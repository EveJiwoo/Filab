using ClassDef;
using EnumDef;
using SheetData;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//1. 상점에서 아이템을 산다.(완)
//2. 상점에서 아이템을 산 후 내 인벤토리에 저장한다.(완)
//3. 내 인벤토리를 복원한다.(완)
//4. 내 인벤토리에서 상점에 물건을 판다.(완)
//5. 상점의 물건이 상점에 들를 때 적절하게 변동 수치(물가, 생산량)를 모두 반영한다.
//6. 마을 20개 추가

public class DataManager : MonoBehaviour
{
    static public DataManager Instance = null;
    
    [Header("최초의 게임 시작 시간 : 년도")]
    public int kStartYear;
    [Header("최초의 게임 시작 시간 : 월")]
    public int kStartMonth;
    [Header("최초의 게임 시작 시간 : 일")]
    public int kStartDay;
    [Header("최초의 게임 시작 시간 : 시")]
    public int kStartHour;
    [Header("최초의 게임 시작 시간 : 분")]
    public int kStartMin;

    [PropertySpace]
    [Button("시작날짜 초기화")]
    void ResetGameDateTime()
    {
        PlayerPrefs.SetString(ConstDef.GAME_DATE_TIME, "");
    }

    [PropertySpace]
    [Button("데이터 초기화")]
    void ResetInventory()
    {
        PlayerPrefs.DeleteKey(ConstDef.GAME_DATE_TIME);
        ES3.DeleteFile(Application.dataPath + "/MyInventory.dat");
    }

    int mCurMonth;

    public DateTime curDateTime { get; set; }

    public MyInfo myInfo { get; set; }

    //도시별 상점 아이템 판매 구매 정보
    Dictionary<CityType, ShopInfo> mCityShopInfoList = new Dictionary<CityType, ShopInfo>();
    //도시별 은행 정기 예금, 대출 상품 정보
    Dictionary<CityType, BankInfo> mCityBankInfoList = new Dictionary<CityType, BankInfo>();

    public int maxCdCount = 0;
    public int maxLoanCount = 0;

    public float minLoanInterestRate = 0f;
    public float maxLoanInterestRate = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
        myInfo = ES3.Load("MyInfo", Application.dataPath + "/MyInfo.dat", new MyInfo());
        foreach (var item in myInfo.invenItemInfoList)
            item.table = Mng.table.FindItemDataTable(item.uid);

        maxCdCount = (int)Mng.table.GetGameDataTable("CDMaxCount").GameDataValue;
        maxLoanCount = (int)Mng.table.GetGameDataTable("LoanMaxCount").GameDataValue;
        minLoanInterestRate = Mng.table.GetGameDataTable("LoanMinInterestRate").GameDataRatio;
        maxLoanInterestRate = Mng.table.GetGameDataTable("LoanMaxInterestRate").GameDataRatio;

        TimeUpdate();
        CityShopUpdate();
        CityBankCdAndLoanUpdate();
        MyExtralInterestRateUpdate();
    }

    public void TimeUpdate()
    {
        //저장된 시간 복구
        var time = PlayerPrefs.GetString(ConstDef.GAME_DATE_TIME);
        if (time == "")
            curDateTime = new DateTime(kStartYear, kStartMonth, kStartDay, kStartHour, kStartMin, 0);
        else
            curDateTime = DateTime.Parse(time);

        mCurMonth = curDateTime.Month;
    }
    
    private void FixedUpdate()
    {
        if (curDateTime.Month != mCurMonth)
        {
            mCurMonth = curDateTime.Month;
            AllShopItemUpdate();
            CityBankCdAndLoanUpdate();
            MyOccupationScoreUpdate();
            MyExtralInterestRateUpdate();
        }

        MyLoanUpdate();
    }

    //////////////////////////////////////////////////////////////////////////////////////////
    //상점 상품 정보

    public void CityShopUpdate()
    {
        foreach (var data in Mng.table.itemDataTableList)
        {
            bool[] shopGoods = new bool[20] { 
                data.City1Sell, data.City2Sell, data.City3Sell, data.City4Sell, data.City5Sell, data.City6Sell, data.City7Sell, data.City8Sell, data.City9Sell, data.City10Sell,
                data.City11Sell,data.City12Sell,data.City13Sell,data.City14Sell,data.City15Sell,data.City16Sell,data.City17Sell,data.City18Sell,data.City9Sell, data.City20Sell};

            for(int i = 0; i < shopGoods.Length; i++)
            {
                var isGoods = shopGoods[i];

                if (isGoods == true)
                    SetCityShopGoodsList((CityType)i, data);
                else
                    SetCityShopNotGoddsList((CityType)i, data);
            }
        }
    }

    /// <summary> 유저에게 팔 아이템 목록 </summary>
    void SetCityShopGoodsList(CityType _city, ItemDataTable_Client _data)
    {
        if (mCityShopInfoList.ContainsKey(_city) == false)
            mCityShopInfoList[_city] = new ShopInfo();

        ShopItemInfo info = new ShopItemInfo();
        info.table = _data;
        info.uid = _data.UID;        
        mCityShopInfoList[_city].userBuyList.Add(info);

        ShopPriceAndCountUpdate(_city, info, curDateTime);
    }

    /// <summary> 유저에게 살 아이템 목록 </summary>
    void SetCityShopNotGoddsList(CityType _city, ItemDataTable_Client _data)
    {
        if (mCityShopInfoList.ContainsKey(_city) == false)
            mCityShopInfoList[_city] = new ShopInfo();

        ShopItemInfo info = new ShopItemInfo();
        info.table = _data;
        info.uid = _data.UID;
        mCityShopInfoList[_city].shopBuyList.Add(info);

        ShopPriceAndCountUpdate(_city, info, curDateTime);
    }

    float GetShopRate(CityType _city, ItemDataTable_Client _data)
    {
        float rate = 0f;
        if (IsSellItem(_city, _data) == true)
            rate = UnityEngine.Random.Range(_data.MinDiscountRate, _data.MaxDiscountRate);
        else
            rate = UnityEngine.Random.Range(_data.MinPremiumRate, _data.MaxPremiumRate);

        return rate;
    }

    bool IsSellItem(CityType _city, ItemDataTable_Client _data)
    {
        switch (_city)
        {
            case CityType.City1: return _data.City1Sell;
            case CityType.City2: return _data.City2Sell;
            case CityType.City3: return _data.City3Sell;
            case CityType.City4: return _data.City4Sell;
            case CityType.City5: return _data.City5Sell;
            case CityType.City6: return _data.City6Sell;
            case CityType.City7: return _data.City7Sell;
            case CityType.City8: return _data.City8Sell;
            case CityType.City9: return _data.City9Sell;
            case CityType.City10: return _data.City10Sell;
            case CityType.City11: return _data.City11Sell;
            case CityType.City12: return _data.City12Sell;
            case CityType.City13: return _data.City13Sell;
            case CityType.City14: return _data.City14Sell;
            case CityType.City15: return _data.City15Sell;
            case CityType.City16: return _data.City16Sell;
            case CityType.City17: return _data.City17Sell;
            case CityType.City18: return _data.City18Sell;
            case CityType.City19: return _data.City19Sell;
            case CityType.City20: return _data.City20Sell;
            default:
                return false;
        }
    }

    public ShopInfo GetShopInfo(CityType _type)
    {
        return mCityShopInfoList[_type];
    }        
       

    /// <summary> 모든 상점 가격, 생산 업데이트 </summary>
    public void AllShopItemUpdate()
    {
        foreach (var shop in mCityShopInfoList)
        {
            foreach (var item in shop.Value.userBuyList)
                ShopPriceAndCountUpdate(shop.Key, item, curDateTime);

            foreach (var item in shop.Value.shopBuyList)
                ShopPriceAndCountUpdate(shop.Key, item, curDateTime);
        }
    }

    /// <summary> 상점 가격 재설정 </summary>
    public void ShopPriceAndCountUpdate(CityType _city, ShopItemInfo _item, DateTime _today)
    {
        //시장 물가 변동 적용
        _item.marketRate = Mng.table.GetItemPriceRate(_today.Year, _today.Month);
        //상점 가격 변동 적용
        _item.shopRate = GetShopRate(_city, _item.table) * 0.01f;

        //상점 생산 갯수 적용 : 팔고 있을 경우 생산, 아니면 없음
        if (IsSellItem(_city, _item.table) == true)            
            _item.count = UnityEngine.Random.Range(_item.table.MinProduction, _item.table.MaxProduction + 1);
        else
            _item.count = 0;
    }

    //////////////////////////////////////////////////////////////////////////////////////////
    //은행 상품(정기 예금, 대출) 갱신
    
    /// <summary> 새 예금 상품 생성</summary>
    public void CityBankCdAndLoanUpdate()
    {
        mCityBankInfoList.Clear();

        float baseInterestRate = Mng.table.GetBaseInterestRate(curDateTime);

        for (int i = 0; i < (int)CityType.Max; i++)
        {
            var bankInfo = new BankInfo();
            
            //////////////////////////////////////////////////////////
            //정기 예금 상품 갱신
            //최소 상품 갯수 1개 ~ 최대 상품 갯수3개
            int productionCount = UnityEngine.Random.Range(1, 4);

            //1년 ~ 5년 상품 
            List<int> termList = new List<int>() { 1, 2, 3, 4, 5};
            
            for(int n = 0; n < productionCount; n++)
            {
                CDProductInfo cd = new CDProductInfo();
                
                int selectIndex = UnityEngine.Random.Range(0, termList.Count);
                //만기 기간
                cd.term = termList[selectIndex];
                float addInterstRate = 0f;
                switch(cd.term)
                {
                    //1년 만기 정기 예금 추가 금리(1~2%)
                    case 1: addInterstRate = UnityEngine.Random.Range(0.01f, 0.02f); break;
                    //2년 만기 정기 예금 추가 금리(2~4%)
                    case 2: addInterstRate = UnityEngine.Random.Range(0.02f, 0.04f); break;
                    //3년 만기 정기 예금 추가 금리(3~5%)
                    case 3: addInterstRate = UnityEngine.Random.Range(0.03f, 0.05f); break;
                    //4년 만기 정기 예금 추가 금리(4~7%)
                    case 4: addInterstRate = UnityEngine.Random.Range(0.04f, 0.07f); break;
                    //5년 만기 정기 예금 추가 금리(5~10%)
                    case 5: addInterstRate = UnityEngine.Random.Range(0.05f, 0.1f); break;
                    default:{
                            Debug.Log("해당하는 연차의 만기 정기 예금 추가 금리가 없습니다.");
                            return;
                        }
                }

                //예금 금리
                cd.interestRate = baseInterestRate/*기준금리*/ + addInterstRate/*추가금리*/;
                //만기 일자
                cd.maturityDate = curDateTime.AddYears(cd.term);

                termList.RemoveAt(selectIndex);

                bankInfo.cdList.Add(cd);
            }

            //////////////////////////////////////////////////////////
            //대출 상품 갱신
            LoanCondition loan = new LoanCondition();

            //대출 금리
            float addLoanInterstRate = UnityEngine.Random.Range(minLoanInterestRate, maxLoanInterestRate);
            loan.interestRate = baseInterestRate/*기준금리*/ + addLoanInterstRate/*추가금리*/;

            //1년 ~ 5년 상품
            loan.term = UnityEngine.Random.Range(1, 6);

            //만기 일자
            loan.maturityDate = curDateTime.AddYears(loan.term);

            bankInfo.loan = loan;

            mCityBankInfoList[(CityType)i] = bankInfo;
        }

        Debug.Log("정기 예금 상품이 모두 갱신되었습니다.");
    }

    /// <summary> 각 은행들의 상품(정기예금, 대출상품) 정보</summary>
    public BankInfo GetBankInfo(CityType _type)
    {
        return mCityBankInfoList[_type];
    }

    /// <summary> 만기가 도래한 정기 예금 상품 목록 획득 </summary>    
    public List<CDProductInfo> GetMyCdMaturityList()
    {        
        var list = myInfo.cdProductList.Where(_p => _p.maturityDate.CompareTo(curDateTime) == -1).ToList();

        return list;
    }

    //////////////////////////////////////////////////////////////////////////////////////////
    //신용 점수 결산

    /// <summary> 신용 점수 갱신 </summary>
    void MyOccupationScoreUpdate()
    {
        //신용 점수 계산
        var table = Mng.table.GetOccupationDataTable(myInfo.occupation);

        double addOccupation = (myInfo.monthPurchasePrice / 50f) * table.CreditIncreaseVariable;
        addOccupation = Mathf.Min((float)addOccupation, (float)table.MaxMonthlyCreditIncrease);
        myInfo.occupation += (float)addOccupation;

        myInfo.monthPurchasePrice = 0;
    }

    /// <summary> 신용 점수에 따른 대출 금리 할인율 반영 </summary>
    void MyExtralInterestRateUpdate()
    {
        //신용 점수에 따른 대출 금리 할인율 계산
        var table = Mng.table.GetOccupationDataTable(myInfo.occupation);
        myInfo.extraInterestRate = UnityEngine.Random.Range(table.ExtraInterestMin * 0.01f, table.ExtraInterestMax * 0.01f);
    }

    //////////////////////////////////////////////////////////////////////////////////////////
    //대출 상환

    /// <summary> 대출 이자 및 원금 상환 </summary>
    public void MyLoanUpdate()
    {
        var payLoanList = myInfo.loanCondtionList.Where(_p => _p.nextPaymentDate <= curDateTime).ToList();
        if (payLoanList.Count == 0)
            return;

        long totalPayGold = 0;
        foreach(var payLoan in payLoanList )
        {
            //이자 상환
            long interestPayGold = (long)((float)payLoan.loanGold * (payLoan.interestRate / 12f));
            payLoan.interestPayGold += interestPayGold;
            //원금 상환
            //long principalPayGold = (long)((float)payLoan.loanGold / (12f * payLoan.term));
            //payLoan.principalPayGold += principalPayGold;

            //totalPayGold += interestPayGold + principalPayGold;            
            totalPayGold += interestPayGold;

            payLoan.interestPayCount++;

            //대출 만기 종료
            if (payLoan.maturityDate <= curDateTime)
            {
                totalPayGold += payLoan.loanGold;
                //*대출 목록에서 삭제                
                myInfo.loanCondtionList.Remove(payLoan);
            }
            else
            //다음 상환날 업데이트
            {
                payLoan.NextPayDateUpdate();
            }
        }

        if (totalPayGold > 0){
            MessageBox.Open($"{totalPayGold} Gold will be redeemed.", 
                () => {
                    myInfo.gold -= totalPayGold;
                    Mng.canvas.kTopMenu.MyGoldUpdate();
                });
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////
    //자유 예금 상품

    /// <summary> 자유 예금 추가 </summary>
    public void SetDesipot(long _gold)
    {
        myInfo.gold -= _gold;
        myInfo.freeDepositGold = _gold;
        //myInfo.bank.depositTime = curDateTime;
    }

    private void OnApplicationQuit()
    {
        //현재까지의 시간 저장
        PlayerPrefs.SetString(ConstDef.GAME_DATE_TIME, curDateTime.ToString());

        ES3.Save("MyInfo", Mng.data.myInfo, Application.dataPath + "/MyInfo.dat");
    }
}
