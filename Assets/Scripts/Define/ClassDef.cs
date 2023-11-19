
using EnumDef;
using SheetData;
using System;
using System.Collections.Generic;

namespace ClassDef
{
    public class ShopInfo  
    {
        public DateTime nextChangePriceDay;
        public List<ItemInfo> sellList = new List<ItemInfo>();
    }

    public class ItemInfo
    {
        public long uid;
        public int count;
        public long inventAvgPrice;
        public long shopSellPrice { get { return table.OrignalSellPrice; } }
        public long shopBuyPrice { get { return table.OrignalBuyPrice; } }

        public ItemDataTable_Client table;

        public void Set(int _count)
        {
            count = _count;
        }

        public void Set(int _count, long _price)
        {
            count = _count;
            inventAvgPrice = _price;
        }

        public void Add(int _count, float _price)
        {
            double totalPrice = ((double)count * (double)inventAvgPrice) + ((double)_count * (double)_price);

            count = count + _count;
            inventAvgPrice = (long)(totalPrice / (double)count);
        }

        public void Remove(int _count)
        {
            count -= _count;
        }
    }
}
