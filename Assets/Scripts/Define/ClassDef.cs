
using EnumDef;
using SheetData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClassDef
{
    public class ShopInfo  
    {
        public DateTime nextChangePriceDay;
        public List<ShopItemInfo> userBuyList = new List<ShopItemInfo>();
        public List<ShopItemInfo> shopBuyList = new List<ShopItemInfo>();
    }

    public class ShopItemInfo
    {
        public long uid;
        public int count;

        /// <summary> 물가 반영율 </summary>
        public float marketRate;
        /// <summary> 상점에 존재하면 할인, 존재하지 않으면 프리미엄 반영율 </summary>
        public float shopRate;

        /// <summary> 상점 판매가 (유저 구입가) </summary>
        public long userBuyPrice { get { return (long)(table.Price * marketRate); } }
        /// <summary> 상점 구매가 (유저 판매가) </summary>
        public long userSellPrice { get { return (long)((table.Price * marketRate) * shopRate); } }

        public ItemDataTable_Client table;
    }

    public class InvenItemInfo
    {
        public long uid;
        public int count;
        //평균 구매가
        public long avrPrice;
        //(상점)판매가
        public long sellPrice;

        public ItemDataTable_Client table;

        public void Add(int _count, long _price)
        {
            double totalPrice = ((double)count * (double)avrPrice) + ((double)_count * (double)_price);

            count = count + _count;
            avrPrice = (long)(totalPrice / (double)count);
        }

        public void Remove(int _count)
        {
            count -= _count;
        }
    }

    public class BankInfo
    {
        public List<CDProductInfo> cdList = new List<CDProductInfo>();
        public LoanCondition loan;
    }

    //정기 예금 정보
    public class CDProductInfo
    {
        //대상 은행
        public CityType city = CityType.City1;
        //정기 예금액
        public long depositeGold = 0;
        //정기 예금 금리(년)
        public float interestRate = 0f;
        //정기 예금 기간(년)
        public int term = 1;
        //정기 예금 계약일
        public DateTime openDate;
        //정기 예금 만기일
        public DateTime maturityDate;
    }

    public class LoanCondition
    {
        //대출 은행
        public CityType city = CityType.City1;
        //대출 금액
        public long loanGold = 0;

        //원금 상환액
        //public long principalPayGold;
        //이자 상환액
        public long interestPayGold;

        //대출 금리(년)
        public float interestRate = 0f;
        //갱신된 원금 대비 이자
        public long curInterestGold;

        //대출 기간(년)
        public int term = 1;
        //대출 만기일
        public DateTime maturityDate;
        //대출 계약일
        public DateTime contractDate;
        //다음 상환일
        public DateTime nextPaymentDate = default;
        
        //상환 횟수
        public int interestPayCount = 0;        

        //만기 기준 예상 남은 상환금
        public long curPrincipal{
            get{
                long totalLoanGold = ((long)(loanGold * (1f + interestRate * term)));
                return (totalLoanGold - interestPayGold);
            }
        }

        public void NextPayDateUpdate()
        {
            nextPaymentDate = contractDate.AddMonths(interestPayCount + 1);
        }
    }


    public class MyInfo
    {
        public CityType local = CityType.City1;

        /// <summary>소지금 정보</summary>
        public long gold = 50000;

        /// <summary>신용 점수 정보</summary>
        public float occupation = 500;

        /// <summary>자유 예금 금액</summary>
        public long freeDepositGold = 0;

        /// <summary>한달간의 아이템 구입 대금</summary>
        public long monthPurchasePrice = 0;

        /// <summary>대출 금리 할인율</summary>
        public float extraInterestRate = 0f;

        /// <summary>현재 시간</summary>
        public int curYear = 1300;
        public int curMonth = 1;
        public int curDay = 1;
        public int curHour = 0;
        public int curMin = 0;

        //정기 예금 정보
        List<CDProductInfo> mCdProductList = new List<CDProductInfo>();
        public List<CDProductInfo> cdProductList {
            get { return mCdProductList; }
            set { mCdProductList = value; }
        }

        //대출 상품 정보
        List<LoanCondition> mLoanConditionList = new List<LoanCondition>();
        public List<LoanCondition> loanCondtionList
        {
            get { return mLoanConditionList; }
            set { mLoanConditionList = value; }
        }

        //아이템 정보
        List<InvenItemInfo> mInvenItemInfoList = new List<InvenItemInfo>();

        public List<InvenItemInfo> invenItemInfoList{
            get { return mInvenItemInfoList; }
            set { mInvenItemInfoList = value; }
        }

        public void AddInventory(ItemDataTable_Client _table, int _count, long _price)
        {
            var item = mInvenItemInfoList.Find(_p => _p.uid == _table.UID);

            if (item == null)
            {
                InvenItemInfo newItem = new InvenItemInfo();
                newItem.uid = _table.UID;
                newItem.table = _table;
                mInvenItemInfoList.Add(newItem);

                item = newItem;
            }

            item.Add(_count, _price);
        }

        public void RemoveInventory(ItemDataTable_Client _table, int _count)
        {
            var item = mInvenItemInfoList.Find(_p => _p.uid == _table.UID);

            if (item == null)
            {
                Debug.LogError($"인벤토리에는 {_table.UID} UID 아이템이 존재하지 않습니다.");
                return;
            }

            item.Remove(_count);

            if (item.count == 0)
                mInvenItemInfoList.Remove(item);
        }
    }
}
