﻿
using EnumDef;
using SheetData;
using System;
using System.Collections.Generic;

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

        /// <summary> 상점 판매가 </summary>
        public long sellPrice { get { return (long)(table.Price * marketRate); } }
        /// <summary> 상점 구매가 </summary>
        public long buyPrice { get { return (long)((table.Price * marketRate) * shopRate); } }

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
}
