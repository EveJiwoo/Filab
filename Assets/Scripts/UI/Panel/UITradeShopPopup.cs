using ClassDef;
using EnumDef;
using SheetData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITradeShopPopup : UIBase
{
    public enum Type
    {
        None,
        UserBuy,
        UserSell,
    }

    public GameObject kItemListGo;

    UIItemIcon[] mItemIconList;
    //������ ������ ������ ������ ���
    List<ShopItemInfo> mUserBuyItemDataList;
    //������ �� ������ ������ ���(�κ��丮)
    List<InvenItemInfo> mUserSellItemDataList;

    [Header("������ ����Ʈ ��ũ��")]
    public ScrollRect kScrollRect;
    [Header("���õ� ������ �̹���")]
    public Image kSelectItemImage;
    [Header("���õ� ������ �̸�")]
    public TMP_Text kSelectItemName;
    [Header("���õ� ������ ����")]
    public TMP_Text kSelectItemPrice;
    [Header("���õ� ������ ����")]
    public TMP_Text kSelectItemCount;

    [Header("���� ���� ���")]
    public TMP_Text kMyGold;

    [Header("���� ������ ���� ��ư")]
    public GameObject kBuyButtonGo;
    [Header("���� ������ �Ǹ� ��ư")]
    public GameObject kSellButtonGo;

    [Header("���� - ���� ������ ���� ������")]
    public GameObject kBuyPageGo;
    [Header("���� - ���� ������ ���� ����")]
    public TMP_Text kBuyItemPrice;
    [Header("���� - ���� ������ ���� ����")]
    public TMP_InputField kBuyNumberInput;
    [Header("���� - ���� ������ ���� �� ����")]
    public TMP_Text kBuyItemTotalPrice;

    [Header("�Ǹ� - ���� ������ �Ǹ� ������")]
    public GameObject kSellPageGo;
    [Header("�Ǹ� - ���� ������ ��� ���� ����")]
    public TMP_Text kAvrPurchasePrice;
    [Header("�Ǹ� - ���� ������ �ǸŰ�")]
    public TMP_Text kSellPrice;
    [Header("�Ǹ� - ���� ������ �Ǹ� ����")]
    public TMP_InputField kSellNumberInput;
    [Header("�Ǹ� - ���� ������ ���� ���Ͱ�")]
    public TMP_Text kProfitLossPrice;
    [Header("�Ǹ� - ���� ������ �� ���Ͱ�")]
    public TMP_Text kProfitLossTotalPrice;
    [Header("�Ǹ� - ���� ������ ���� ���ͷ�")]
    public TMP_Text kSellItemYield;
    [Header("�Ǹ� - ���� ������ �� �ǸŰ�")]
    public TMP_Text kSellItemTotalPrice;

    long mSelectUID = ConstDef.NONE;
    ShopItemInfo mSelectShopItemInfo;
    InvenItemInfo mSelectInvenItemInfo;

    Type mType = Type.None;

    // Start is called before the first frame update
    void Awake()
    {
        mItemIconList = kItemListGo.GetComponentsInChildren<UIItemIcon>();
        foreach (var item in mItemIconList)
            item.onClick += OnItemIconClick;

        MyGoldUpdate();

        OnUserBuyItemTapButtonClick();
    }

    protected override void onDisable()
    {
        kScrollRect.verticalNormalizedPosition = 1f;        
        Unselect();
    }

    protected override void onEnable()
    {
        mType = Type.None;
        OnUserBuyItemTapButtonClick();
    }

    void Unselect()
    {
        mSelectUID = ConstDef.NONE;
        SelectItemIconUpdate(mSelectUID);
        BuyItemPriceUpdate(0, 0);
        SellItemPriceUpdate(null, 0);
        kBuyNumberInput.text = "0";
        kSellNumberInput.text = "0";
        mSelectInvenItemInfo = null;
        mSelectShopItemInfo = null;
    }

    public void OnUserBuyItemTapButtonClick()
    {
        if (mType == Type.UserBuy)
            return;

        kBuyButtonGo.gameObject.SetActive(true);
        kSellButtonGo.gameObject.SetActive(false);

        kBuyPageGo.SetActive(true);
        kSellPageGo.SetActive(false);

        mType = Type.UserBuy;

        Unselect();

        mUserBuyItemDataList = Mng.data.GetShopInfo(Mng.data.myInfo.local).userBuyList;

        ItemIconUpdate(mType);
    }

    public void OnUserSellItemTapButtonClick()
    {
        if (mType == Type.UserSell)
            return;

        kBuyButtonGo.gameObject.SetActive(false);
        kSellButtonGo.gameObject.SetActive(true);

        kBuyPageGo.SetActive(false);
        kSellPageGo.SetActive(true);

        mType = Type.UserSell;

        Unselect();

        mUserSellItemDataList = Mng.data.myInfo.invenItemInfoList;

        ItemIconUpdate(mType);
    }

    void MyGoldUpdate()
    {
        kMyGold.text = Mng.data.myInfo.gold.ToColumnString();
    }

    void SelectItemIconUpdate(long _uid)
    {
        if(_uid == ConstDef.NONE)
        {
            kSelectItemImage.sprite = null;
            kSelectItemName.text = "";
            kSelectItemPrice.text = "";
            kSelectItemCount.text = "";
            mSelectShopItemInfo = null;
            mSelectInvenItemInfo = null;
            BuyItemPriceUpdate(0, 0);
            SellItemPriceUpdate(null, 0);
        }
        else
        {
            switch(mType){
                case Type.UserBuy:{
                        var item = mUserBuyItemDataList.Find(_p => _p.uid == _uid);
                        kSelectItemImage.sprite = Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName);
                        kSelectItemName.text = item.table.Name;
                        kSelectItemPrice.text = item.userBuyPrice.ToColumnString();
                        kSelectItemCount.text = item.count.ToColumnString();
                    }break;
                case Type.UserSell:{
                        var item = mUserSellItemDataList.Find(_p => _p.uid == _uid);
                        kSelectItemImage.sprite = Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName);
                        kSelectItemName.text = item.table.Name;
                        kSelectItemPrice.text = item.avrPrice.ToColumnString();
                        kSelectItemCount.text = item.count.ToColumnString();
                    }break;
            }            
        }
    }

    void ItemIconUpdate(Type _type)
    {
        for (int i = 0; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
            mItemIconList[i].gameObject.SetActive(false);

        int iconIndex = 0;
        switch(_type)
        {
            case Type.UserBuy:{
                    foreach (var data in mUserBuyItemDataList){
                        if (data.count == 0)
                            continue;

                        mItemIconList[iconIndex].gameObject.SetActive(true);
                        var sprite = Mng.canvas.GetSprite(data.table.AtlasName, data.table.SpriteName);

                        var icon = mItemIconList[iconIndex];
                        icon.Set(data.uid, sprite, data.userBuyPrice, data.count);
                        iconIndex++;
                    }
                }break;
            case Type.UserSell:{

                    foreach (var data in mUserSellItemDataList){
                        if (data.count == 0)
                            continue;

                        mItemIconList[iconIndex].gameObject.SetActive(true);
                        var sprite = Mng.canvas.GetSprite(data.table.AtlasName, data.table.SpriteName);

                        //*���� �ǸŰ� ����
                        var shopInfo = Mng.data.GetShopInfo(Mng.data.myInfo.local);
                        var userBuyItemDataList = shopInfo.userBuyList;
                        var shopBuyItemDataList = shopInfo.shopBuyList;
                        
                        //�������� �Ĵ� ���� ���
                        var item = userBuyItemDataList.Find(_p => _p.uid == data.uid);
                        //�������� ���� �ʴ� ���� ���
                        if( item == null )
                            item = shopBuyItemDataList.Find(_p => _p.uid == data.uid);

                        data.sellPrice = item.userSellPrice;

                        var icon = mItemIconList[iconIndex];
                        icon.Set(data.uid, sprite, data.sellPrice, data.count);
                        iconIndex++;
                    }
                }break;
        }        
    }

    void OnItemIconClick(long _uid)
    {
        mSelectUID = _uid;

        switch(mType){
            case Type.UserBuy:
                mSelectShopItemInfo = mUserBuyItemDataList.Find(_p => _p.uid == mSelectUID);
                BuyItemPriceUpdate(0, 0);
                mSelectInvenItemInfo = null;
                break;
            case Type.UserSell:
                mSelectInvenItemInfo = mUserSellItemDataList.Find(_p => _p.uid == mSelectUID);
                mSelectShopItemInfo = null;
                SellItemPriceUpdate(null, 0);
                break;
        }        
        
        SelectItemIconUpdate(mSelectUID);
    }

    /// <summary> ������ �������� �������� ���� </summary>
    public void OnUserBuyItemButtonClick()
    {
        if( mSelectUID == ConstDef.NONE )
        {
            Debug.Log("������ �������� ������ �ּ���.");
            return;
        }

        int buyCount = int.Parse(kBuyNumberInput.text);

        var item = mUserBuyItemDataList.Find(_p => _p.uid == mSelectUID);
        if ( Mng.data.myInfo.gold < item.userBuyPrice * buyCount)
        {
            Debug.Log("�������� ���� �մϴ�.");
            return;
        }        

        if ( item.count <= 0 || item.count < buyCount)
        {
            Debug.Log("��ǰ ������ ���� �մϴ�.");
            return;
        }

        //����
        Mng.data.myInfo.gold -= item.userBuyPrice * buyCount;
        
        item.count -= buyCount;
        
        if (item.count <= 0)
            mSelectUID = ConstDef.NONE;

        Mng.data.myInfo.AddInventory(item.table, buyCount, item.userBuyPrice);

        ItemIconUpdate(mType);
        MyGoldUpdate();        
        SelectItemIconUpdate(mSelectUID);
    }

    /// <summary> ������ ������ �������� �Ǹ�</summary>
    public void OnUserSellItemButtonClick()
    {
        if (mSelectUID == ConstDef.NONE){
            Debug.Log("������ �������� ������ �ּ���.");
            return;
        }

        int sellCount = int.Parse(kSellNumberInput.text);

        var item = mUserSellItemDataList.Find(_p => _p.uid == mSelectUID);                

        //�Ǹ�
        Mng.data.myInfo.gold += item.sellPrice * sellCount;

        if (item.count <= sellCount)
            mSelectUID = ConstDef.NONE;

        Mng.data.myInfo.RemoveInventory(item.table, sellCount);

        ItemIconUpdate(mType);
        MyGoldUpdate();
        SelectItemIconUpdate(mSelectUID);
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////
    //���� ���� Ȱ��
    public void OnMinusBuyItemCountButtonClick()
    {
        if (mSelectShopItemInfo == null)
            return;

        int count = 0;
        if( kBuyNumberInput.text != "")
            count = int.Parse(kBuyNumberInput.text) - 1;

        count = Mathf.Max(0, count);
        
        BuyItemPriceUpdate(mSelectShopItemInfo.userBuyPrice, count);
    }

    public void OnPlusBuyItemCountButtonClick()
    {
        if (mSelectShopItemInfo == null)
            return;

        int count = 0;
        if ( kBuyNumberInput.text != "" )
            count = int.Parse(kBuyNumberInput.text) + 1;

        int maxBuyCount = (int)(Mng.data.myInfo.gold / mSelectShopItemInfo.userBuyPrice);
        count = Mathf.Min(maxBuyCount, mSelectShopItemInfo.count, count);
        
        BuyItemPriceUpdate(mSelectShopItemInfo.userBuyPrice, count);
    }

    public void OnMaxBuyItemCountButtonClick()
    {
        if (mSelectShopItemInfo == null)
            return;
        
        int maxBuyCount = (int)(Mng.data.myInfo.gold / mSelectShopItemInfo.userBuyPrice);
        int value = Mathf.Min(maxBuyCount, mSelectShopItemInfo.count);

        BuyItemPriceUpdate(mSelectShopItemInfo.userBuyPrice, value);
    }

    public void OnEditBuyItemCount()
    {
        if (mSelectShopItemInfo == null){
            kBuyNumberInput.text = "0";
            return;
        }

        int count = int.Parse(kBuyNumberInput.text);
        if(count < 0)
            count = 0;
        
        int maxCount = Mathf.Min((int)(Mng.data.myInfo.gold / mSelectShopItemInfo.userBuyPrice), mSelectShopItemInfo.count);
        if (count > maxCount)
            count = maxCount;

        BuyItemPriceUpdate(mSelectShopItemInfo.userBuyPrice, count);
    }

    public void BuyItemPriceUpdate(long _price, int _count)
    {
        kBuyNumberInput.text = _count.ToString();
        kBuyItemPrice.text = _price.ToColumnString() + " G";
        kBuyItemTotalPrice.text = (_price * _count).ToColumnString() + " G";
    }

    ////////////////////////////////////////////////////////////////////////////////////
    //���� �Ǹ� Ȱ��
    public void OnMinusSellItemCountButtonClick()
    {
        if (mSelectInvenItemInfo == null)
            return;

        int count = 0;
        if (kSellNumberInput.text != "")
            count = int.Parse(kSellNumberInput.text) - 1;

        count = Mathf.Max(0, count);

        SellItemPriceUpdate(mSelectInvenItemInfo, count);
    }

    public void OnPlusSellItemCountButtonClick()
    {
        if (mSelectInvenItemInfo == null)
            return;

        int count = 0;
        if (kSellNumberInput.text != "")
            count = int.Parse(kSellNumberInput.text) + 1;
                
        count = Mathf.Min(mSelectInvenItemInfo.count, count);

        SellItemPriceUpdate(mSelectInvenItemInfo, count);
    }

    public void OnMaxSellItemCountButtonClick()
    {
        if (mSelectInvenItemInfo == null)
            return;

        int count = mSelectInvenItemInfo.count;

        SellItemPriceUpdate(mSelectInvenItemInfo, count);
    }

    public void OnEditSellItemCount()
    {
        if (mSelectInvenItemInfo == null){
            kSellNumberInput.text = "0";
            return;
        }

        int count = int.Parse(kSellNumberInput.text);
        if (count < 0)
            count = 0;
        
        if (count > mSelectInvenItemInfo.count)
            count = mSelectInvenItemInfo.count;

        SellItemPriceUpdate(mSelectInvenItemInfo, count);
    }

    public void SellItemPriceUpdate(InvenItemInfo _info, int _count)
    {
        kSellNumberInput.text = _count.ToString();

        if (_info == null)
        {
            kAvrPurchasePrice.text = "0 G";
            kSellPrice.text = "0 G";
            kProfitLossPrice.text = "0 G";
            kProfitLossTotalPrice.text = "0 G";

            kSellItemYield.text = "0 %";

            kSellItemTotalPrice.text = "0 G";
        }
        else
        {
            kAvrPurchasePrice.text = _info.avrPrice.ToColumnString() + " G";
            kSellPrice.text = _info.sellPrice.ToColumnString() + " G";

            long profit = _info.sellPrice - _info.avrPrice;
            kProfitLossPrice.text = profit.ToColumnString() + " G";
            kProfitLossTotalPrice.text = (profit * _count).ToColumnString() + " G";

            kSellItemYield.text = (((float)profit / (float)_info.avrPrice)*100f).ToFloatN2String() + " %";

            kSellItemTotalPrice.text = (_info.sellPrice * _count).ToColumnString() + " G";
        }
    }
}
