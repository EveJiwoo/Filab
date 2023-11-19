
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
        
        public long shopSellPrice { get { return table.OrignalSellPrice; } }
        public long shopBuyPrice { get { return table.OrignalBuyPrice; } }

        public ItemDataTable_Client table;

        public void Set(int _count)
        {
            count = _count;
        }
    }

    public class ItemInfo
    {
        public long uid;
        public int count;
        public long price;
        
        public ItemDataTable_Client table;

        public void Add(int _count, float _price)
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
