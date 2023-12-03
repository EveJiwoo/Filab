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
    //유저가 구입할 아이템 데이터 목록
    List<ShopItemInfo> mUserBuyItemDataList;
    //유저가 팔 아이템 데이터 목록(인벤토리)
    List<InvenItemInfo> mUserSellItemDataList;

    [Header("아이템 리스트 스크롤")]
    public ScrollRect kScrollRect;
    [Header("선택된 아이템 이미지")]
    public Image kSelectItemImage;
    [Header("선택된 아이템 이름")]
    public TMP_Text kSelectItemName;
    [Header("선택된 아이템 가격")]
    public TMP_Text kSelectItemPrice;
    [Header("선택된 아이템 갯수")]
    public TMP_Text kSelectItemCount;

    [Header("내가 가진 골드")]
    public TMP_Text kMyGold;

    [Header("유저가 아이템을 삼")]
    public GameObject kBuyButtonGo;
    [Header("유저가 아이템을 팜")]
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
                        
                        //*유저 판매가 결정
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

    /// <summary> 유저가 상점에서 아이템을 구입 </summary>
    public void OnUserBuyItemButtonClick()
    {
        if( mSelectUID == ConstDef.NONE )
        {
            Debug.Log("구입할 아이템을 선택해 주세요.");
            return;
        }

        int tempBuyCount = 1;

        var item = mUserBuyItemDataList.Find(_p => _p.uid == mSelectUID);
        if ( Mng.data.myInfo.gold < item.userBuyPrice * tempBuyCount)
        {
            Debug.Log("소지금이 부족 합니다.");
            return;
        }        

        if ( item.count <= 0 || item.count < tempBuyCount)
        {
            Debug.Log("상품 갯수가 부족 합니다.");
            return;
        }

        //구입
        Mng.data.myInfo.gold -= item.userBuyPrice * tempBuyCount;
        
        item.count -= tempBuyCount;
        
        if (item.count <= 0)
            mSelectUID = ConstDef.NONE;

        Mng.data.myInfo.AddInventory(item.table, tempBuyCount, item.userBuyPrice);

        ItemIconUpdate(mType);
        MyGoldUpdate();        
        SelectItemIconUpdate(mSelectUID);
    }

    /// <summary> 유저가 상점에 아이템을 판매</summary>
    public void OnUserSellItemButtonClick()
    {
        if (mSelectUID == ConstDef.NONE){
            Debug.Log("구입할 아이템을 선택해 주세요.");
            return;
        }

        int tempBuyCount = 1;

        var item = mUserSellItemDataList.Find(_p => _p.uid == mSelectUID);                

        //판매
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
