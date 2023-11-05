using EnumDef;
using SheetData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static public DataManager Instance = null;

    Dictionary<CityType, List<ItemDataTable_Client>> mCityItemSellList = new Dictionary<CityType, List<ItemDataTable_Client>>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        foreach(var data in Mng.table.itemDataTableList)
        {
            if( data.City1Sell == true) AddCityItemSellList(CityType.City1, data);
            if (data.City2Sell == true) AddCityItemSellList(CityType.City2, data);
            if (data.City3Sell == true) AddCityItemSellList(CityType.City3, data);
            if (data.City4Sell == true) AddCityItemSellList(CityType.City4, data);
            if (data.City5Sell == true) AddCityItemSellList(CityType.City5, data);
            if (data.City6Sell == true) AddCityItemSellList(CityType.City6, data);
            if (data.City7Sell == true) AddCityItemSellList(CityType.City7, data);
            if (data.City8Sell == true) AddCityItemSellList(CityType.City8, data);
            if (data.City9Sell == true) AddCityItemSellList(CityType.City9, data);
            if (data.City10Sell == true) AddCityItemSellList(CityType.City10, data);
            if (data.City11Sell == true) AddCityItemSellList(CityType.City11, data);
            if (data.City12Sell == true) AddCityItemSellList(CityType.City12, data);
            if (data.City13Sell == true) AddCityItemSellList(CityType.City13, data);
            if (data.City14Sell == true) AddCityItemSellList(CityType.City14, data);
            if (data.City15Sell == true) AddCityItemSellList(CityType.City15, data);
            if (data.City16Sell == true) AddCityItemSellList(CityType.City16, data);
            if (data.City17Sell == true) AddCityItemSellList(CityType.City17, data);
            if (data.City18Sell == true) AddCityItemSellList(CityType.City18, data);
            if (data.City19Sell == true) AddCityItemSellList(CityType.City19, data);
            if (data.City20Sell == true) AddCityItemSellList(CityType.City20, data);
        }
    }

    void AddCityItemSellList(CityType _city, ItemDataTable_Client _data)
    {
        if (mCityItemSellList.ContainsKey(_city) == false)
            mCityItemSellList[_city] = new List<ItemDataTable_Client>();

        mCityItemSellList[_city].Add(_data);
    }

    public List<ItemDataTable_Client> GetSellItemList(CityType _type)
    {
        return mCityItemSellList[_type];
    }

    public int GetShopRealSellPrice(ItemDataTable_Client _data)
    {
        int year = Mng.play.curDateTime.Year;
        int month = Mng.play.curDateTime.Month;
        float rate = Mng.table.GetItemPriceRate(year, month);

        return (int)((float)_data.OrignalSellPrice * (1f + rate * 0.01f));
    }
}
