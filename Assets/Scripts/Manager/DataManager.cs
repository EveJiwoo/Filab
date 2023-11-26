using ClassDef;
using EnumDef;
using SheetData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. �������� �������� ���.
//2. �������� �������� �� �� �� �κ��丮�� �����Ѵ�.
//3. �� �κ��丮�� �����Ѵ�.
//4. �� �κ��丮���� ������ ������ �Ǵ�.
//5. ������ ������ ������ �鸦 �� �����ϰ� ���� ��ġ(����, ���귮)�� ��� �ݿ��Ѵ�.

public class DataManager : MonoBehaviour
{
    static public DataManager Instance = null;

    List<InvenItemInfo> mInvenItemInfoList = new List<InvenItemInfo>();
    public List<InvenItemInfo> invenItemInfoList {
        get { return mInvenItemInfoList; }
        set { mInvenItemInfoList = value; }
    }

    Dictionary<CityType, ShopInfo> mCityShopList = new Dictionary<CityType, ShopInfo>();

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void CityShopUpdate()
    {
        foreach (var data in Mng.table.itemDataTableList)
        {
            if (data.City1Sell == true) SetCityItemSellList(CityType.City1, data);
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

        ShopItemInfo info = new ShopItemInfo();
        info.table = _data;
        info.uid = _data.UID;        
        mCityShopList[_city].sellList.Add(info);

        ShopPriceAndCountUpdate(_city, info, Mng.play.curDateTime);        
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

    public ShopInfo GetSellItemList(CityType _type)
    {
        return mCityShopList[_type];
    }

    public void AddInventory(long _uid, int _count, long _price)
    {
        var item = mInvenItemInfoList.Find(_p => _p.uid == _uid);

        if (item == null) {
            InvenItemInfo newItem = new InvenItemInfo();
            newItem.uid = _uid;
            mInvenItemInfoList.Add(newItem);

            item = newItem;
        }

        item.Add(_count, _price);
    }

    public void RemoveInventory(long _uid, int _count, float _price)
    {
        var item = mInvenItemInfoList.Find(_p => _p.uid == _uid);

        if (item == null) {
            Debug.LogError($"�κ��丮���� {_uid} UID �������� �������� �ʽ��ϴ�.");
            return;
        }

        item.Remove(_count);
    }

    /// <summary> �̹��� ���� �ݸ� </summary>
    public float GetLoanRate(DateTime _dt)
    {
        return Mng.table.GetBaseInterestRate(_dt) + Mng.table.GetLoanInterestRate(_dt);
    }

    /// <summary> �̹��� ���� �ݸ� </summary>
    public float GetDepositRate(DateTime _dt)
    {
        return Mng.table.GetBaseInterestRate(_dt) + Mng.table.GetDepositInterestRate(_dt);
    }

    /// <summary> ��� ���� ����, ���� ������Ʈ </summary>
    public void AllShopItemUpdate()
    {
        foreach (var shop in mCityShopList)
        {
            foreach (var item in shop.Value.sellList)
            {
                ShopPriceAndCountUpdate(shop.Key, item, Mng.play.curDateTime);
            }
        }
    }

    public void ShopPriceAndCountUpdate(CityType _city, ShopItemInfo _item, DateTime _today)
    {
        //���� ���� ���� ����
        _item.marketRate = Mng.table.GetItemPriceRate(_today.Year, _today.Month);
        //���� ���� ���� ����
        _item.shopRate = GetShopRate(_city, _item.table);

        //���� ���� ���� ���� : �Ȱ� ���� ��� ����, �ƴϸ� ����
        if (IsSellItem(_city, _item.table) == true)            
            _item.count = UnityEngine.Random.Range(_item.table.MinProduction, _item.table.MaxProduction + 1);
        else
            _item.count = 0;
    }
}
