using ClassDef;
using EnumDef;
using SheetData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager Instance = null;

    List<ItemInfo> mInvenItemInfoList = new List<ItemInfo>();
    public List<ItemInfo> invenItemInfoList { 
        get { return mInvenItemInfoList; }
        set { mInvenItemInfoList = value; } 
    }

    Dictionary<CityType, ShopInfo> mCityShopList = new Dictionary<CityType, ShopInfo>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        foreach (var data in Mng.table.itemDataTableList)
        {
            if( data.City1Sell == true) SetCityItemSellList(CityType.City1, data);
            if (data.City2Sell == true) SetCityItemSellList(CityType.City2, data);
            if (data.City3Sell == true) SetCityItemSellList(CityType.City3, data);
            if (data.City4Sell == true) SetCityItemSellList(CityType.City4, data);
            if (data.City5Sell == true) SetCityItemSellList(CityType.City5, data);
            if (data.City6Sell == true) SetCityItemSellList(CityType.City6, data);
            if (data.City7Sell == true) SetCityItemSellList(CityType.City7, data);
            if (data.City8Sell == true) SetCityItemSellList(CityType.City8, data);
            if (data.City9Sell == true) SetCityItemSellList(CityType.City9, data);
            if (data.City10Sell == true) SetCityItemSellList(CityType.City10, data);
            if (data.City11Sell == true) SetCityItemSellList(CityType.City11, data);
            if (data.City12Sell == true) SetCityItemSellList(CityType.City12, data);
            if (data.City13Sell == true) SetCityItemSellList(CityType.City13, data);
            if (data.City14Sell == true) SetCityItemSellList(CityType.City14, data);
            if (data.City15Sell == true) SetCityItemSellList(CityType.City15, data);
            if (data.City16Sell == true) SetCityItemSellList(CityType.City16, data);
            if (data.City17Sell == true) SetCityItemSellList(CityType.City17, data);
            if (data.City18Sell == true) SetCityItemSellList(CityType.City18, data);
            if (data.City19Sell == true) SetCityItemSellList(CityType.City19, data);
            if (data.City20Sell == true) SetCityItemSellList(CityType.City20, data);
        }
    }

    void SetCityItemSellList(CityType _city, ItemDataTable_Client _data)
    {
        if (mCityShopList.ContainsKey(_city) == false)
            mCityShopList[_city] = new ShopInfo();

        ItemInfo info = new ItemInfo();
        info.table = _data;
        info.Set(99);

        mCityShopList[_city].sellList.Add(info);
    }

    public ShopInfo GetSellItemList(CityType _type)
    {
        return mCityShopList[_type];
    }

    public void AddInventory(long _uid, int _count, float _price)
    {
        var item = mInvenItemInfoList.Find(_p => _p.uid == _uid);
        
        if( item == null){
            ItemInfo newItem = new ItemInfo();
            newItem.uid = _uid;
            mInvenItemInfoList.Add(newItem);

            item = newItem;
        }

        item.Add(_count, _price);
    }

    public void RemoveInventory(long _uid, int _count, float _price)
    {
        var item = mInvenItemInfoList.Find(_p => _p.uid == _uid);

        if (item == null){
            Debug.LogError($"인벤토리에는 {_uid} UID 아이템이 존재하지 않습니다.");
            return;
        }

        item.Remove(_count);
    }

    public int GetShopRealSellPrice(ItemDataTable_Client _data)
    {
        int year = Mng.play.curDateTime.Year;
        int month = Mng.play.curDateTime.Month;
        float rate = Mng.table.GetItemPriceRate(year, month);

        return (int)((float)_data.OrignalSellPrice * (1f + rate * 0.01f));
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
}
