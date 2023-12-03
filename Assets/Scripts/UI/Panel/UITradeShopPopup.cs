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

    [Header("������ �������� ��")]
    public GameObject kBuyButtonGo;
    [Header("������ �������� ��")]
    public GameObject kSellButtonGo;

    long mSelectUID = ConstDef.NONE;

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
        OnUserBuyItemTapButtonClick();
    }

    void Unselect()
    {
        mSelectUID = ConstDef.NONE;
        SelectItemIconUpdate(mSelectUID);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnUserBuyItemTapButtonClick()
    {
        if (mType == Type.UserBuy)
            return;

        kBuyButtonGo.gameObject.SetActive(true);
        kSellButtonGo.gameObject.SetActive(false);

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
        }
        else
        {
            switch(mType){
                case Type.UserBuy:{
                        var item = mUserBuyItemDataList.Find(_p => _p.uid == _uid);
                        kSelectItemImage.sprite = Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName);
                        kSelectItemName.text = item.table.Name;
                        kSelectItemPrice.text = item.userBuyPrice.ToString();
                        kSelectItemCount.text = item.count.ToString();
                    }break;
                case Type.UserSell:{
                        var item = mUserSellItemDataList.Find(_p => _p.uid == _uid);
                        kSelectItemImage.sprite = Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName);
                        kSelectItemName.text = item.table.Name;
                        kSelectItemPrice.text = item.price.ToString();
                        kSelectItemCount.text = item.count.ToString();
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
                        var userSellItemDataList = Mng.data.GetShopInfo(Mng.data.myInfo.local).userBuyList;
                        data.price = userSellItemDataList.Find(_p => _p.uid == data.uid).userSellPrice;

                        var icon = mItemIconList[iconIndex];
                        icon.Set(data.uid, sprite, data.price, data.count);
                        iconIndex++;
                    }
                }break;
        }        
    }

    void OnItemIconClick(long _uid)
    {
        mSelectUID = _uid;

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

        int tempBuyCount = 1;

        var item = mUserBuyItemDataList.Find(_p => _p.uid == mSelectUID);
        if ( Mng.data.myInfo.gold < item.userBuyPrice * tempBuyCount)
        {
            Debug.Log("�������� ���� �մϴ�.");
            return;
        }        

        if ( item.count <= 0 || item.count < tempBuyCount)
        {
            Debug.Log("��ǰ ������ ���� �մϴ�.");
            return;
        }

        //����
        Mng.data.myInfo.gold -= item.userBuyPrice * tempBuyCount;
        
        item.count -= tempBuyCount;
        
        if (item.count <= 0)
            mSelectUID = ConstDef.NONE;

        Mng.data.myInfo.AddInventory(item.table, tempBuyCount, item.userBuyPrice);

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

        int tempBuyCount = 1;

        var item = mUserSellItemDataList.Find(_p => _p.uid == mSelectUID);                

        //�Ǹ�
        Mng.data.myInfo.gold += item.price * tempBuyCount;

        if (item.count <= tempBuyCount)
            mSelectUID = ConstDef.NONE;

        Mng.data.myInfo.RemoveInventory(item.table, tempBuyCount);

        ItemIconUpdate(mType);
        MyGoldUpdate();
        SelectItemIconUpdate(mSelectUID);
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
