
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
    }

    //정기 예금 정보
    public class CDProductInfo
    {
        //정기 예금액
        public long depositeGold = 0;
        //정기 예금 기간(년)
        public float interestRate = 0f;
        //정기 예금 기간(년)
        public int term = 1;
        //정기 예금 만기일
        public DateTime maturityDate;
    }

    public class LoanCondition
    {

    }


    public class MyInfo
    {
        public CityType local = CityType.City1;

        //소지금 정보
        public long gold { get; set; }

        //자유 예금 정보
        public long freeDepositGold { get; set; }

        //정기 예금 정보
        List<CDProductInfo> mCdProductList = new List<CDProductInfo>();
        public List<CDProductInfo> cdProductList {
            get { return mCdProductList; }
            set { mCdProductList = value; }
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
