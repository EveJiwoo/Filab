
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
        public List<ShopItemInfo> sellList = new List<ShopItemInfo>();
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
        public long price;
        
        public ItemDataTable_Client table;

        public void Add(int _count, long _price)
        {
            double totalPrice = ((double)count * (double)price) + ((double)_count * (double)_price);

            count = count + _count;
            price = (long)(totalPrice / (double)count);
        }

        public void Remove(int _count)
        {
            count -= _count;
        }
    }

    public class MyInfo
    {
        public long gold { get; set; }

        List<InvenItemInfo> mInvenItemInfoList = new List<InvenItemInfo>();

        public List<InvenItemInfo> invenItemInfoList
        {
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

        public void RemoveInventory(ItemDataTable_Client _table, int _count, float _price)
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
