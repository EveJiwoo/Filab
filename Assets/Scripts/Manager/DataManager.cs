using ClassDef;
using EnumDef;
using SheetData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. 상점에서 아이템을 산다.(완)
//2. 상점에서 아이템을 산 후 내 인벤토리에 저장한다.(완)
//3. 내 인벤토리를 복원한다.(완)
//4. 내 인벤토리에서 상점에 물건을 판다.
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

    public DateTime curDateTime { get; set; }

    public MyInfo myInfo { get; set; }

    Dictionary<CityType, ShopInfo> mCityShopList = new Dictionary<CityType, ShopInfo>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        
        myInfo = ES3.Load("MyInfo", Application.dataPath + "/MyInfo.dat", new MyInfo());
        foreach (var item in myInfo.invenItemInfoList)
            item.table = Mng.table.FindItemDataTable(item.uid);

        myInfo.gold = 99999999;

        TimeUpdate();
        CityShopUpdate();
    }

    public void TimeUpdate()
    {
        //저장된 시간 복구
        var time = PlayerPrefs.GetString(ConstDef.GAME_DATE_TIME);
        if (time == "")
            curDateTime = new DateTime(kStartYear, kStartMonth, kStartDay, kStartHour, kStartMin, 0);
        else
            curDateTime = DateTime.Parse(time);
    }

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
        if (mCityShopList.ContainsKey(_city) == false)
            mCityShopList[_city] = new ShopInfo();

        ShopItemInfo info = new ShopItemInfo();
        info.table = _data;
        info.uid = _data.UID;        
        mCityShopList[_city].userBuyList.Add(info);

        ShopPriceAndCountUpdate(_city, info, curDateTime);
    }

    /// <summary> 유저에게 살 아이템 목록 </summary>
    void SetCityShopNotGoddsList(CityType _city, ItemDataTable_Client _data)
    {
        if (mCityShopList.ContainsKey(_city) == false)
            mCityShopList[_city] = new ShopInfo();

        ShopItemInfo info = new ShopItemInfo();
        info.table = _data;
        info.uid = _data.UID;
        mCityShopList[_city].shopBuyList.Add(info);

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
        return mCityShopList[_type];
    }        

    /// <summary> 이번달 대출 금리 </summary>
    public float GetLoanRate(DateTime _dt)
    {
        return Mng.table.GetBaseInterestRate(_dt) + Mng.table.GetLoanInterestRate(_dt);
    }

    /// <summary> 이번달 예금 금리 </summary>
    public float GetDepositRate(DateTime _dt)
    {
        return Mng.table.GetBaseInterestRate(_dt) + Mng.table.GetDepositInterestRate(_dt);
    }

    /// <summary> 모든 상점 가격, 생산 업데이트 </summary>
    public void AllShopItemUpdate()
    {
        foreach (var shop in mCityShopList)
        {
            foreach (var item in shop.Value.userBuyList)
            {
                ShopPriceAndCountUpdate(shop.Key, item, curDateTime);
            }
        }
    }

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

    private void OnApplicationQuit()
    {
        //현재까지의 시간 저장
        PlayerPrefs.SetString(ConstDef.GAME_DATE_TIME, curDateTime.ToString());

        ES3.Save("MyInfo", Mng.data.myInfo, Application.dataPath + "/MyInfo.dat");
    }
}
