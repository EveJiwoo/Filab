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
    public GameObject kItemListGo;

    UIItemIcon[] mItemIconList;
    ShopInfo mItemDataList;
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

    int mSelectIndex = ConstDef.NONE;

    // Start is called before the first frame update
    void Awake()
    {
        mItemIconList = kItemListGo.GetComponentsInChildren<UIItemIcon>();
        foreach (var item in mItemIconList)
            item.onClick += OnItemIconClick;

        SetCitySellItem(CityType.City1);

        MyGoldUpdate();
    }

    protected override void onDisable()
    {
        kScrollRect.verticalNormalizedPosition = 1f;

        kSelectItemImage.sprite = null;
        kSelectItemName.text = "";
        kSelectItemPrice.text = "";

        mSelectIndex = ConstDef.NONE;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MyGoldUpdate()
    {
        kMyGold.text = Mng.data.myInfo.gold.ToColumnString();
    }

    void SelectItemIconUpdate(int _index)
    {
        var item = mItemDataList.sellList[_index];
        kSelectItemImage.sprite = Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName);
        kSelectItemName.text = item.table.Name;
        kSelectItemPrice.text = item.userBuyPrice.ToString();
        kSelectItemCount.text = item.count.ToString();
    }

    int ItemIconUpdate()
    {
        int iconCount = 0;
        foreach (var data in mItemDataList.sellList)
        {
            if (data.count == 0)
                continue;

            mItemIconList[iconCount].gameObject.SetActive(true);
            mItemIconList[iconCount].SetSprite(Mng.canvas.GetSprite(data.table.AtlasName, data.table.SpriteName));
            mItemIconList[iconCount].SetPrice(data.userBuyPrice);
            mItemIconList[iconCount].SetCount(data.count);
            iconCount++;
        }

        return iconCount;
    }

    public void SetCitySellItem(CityType _type)
    {
        mItemDataList = Mng.data.GetSellItemList(_type);

        int fillCount = ItemIconUpdate();        

        for (int i = fillCount; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
            mItemIconList[i].gameObject.SetActive(false);
    }
    
    void OnItemIconClick(int _index)
    {
        mSelectIndex = _index;

        SelectItemIconUpdate(mSelectIndex);
    }

    public void OnBuyItemButtonClick()
    {
        if( mSelectIndex == ConstDef.NONE )
        {
            Debug.Log("구입할 아이템을 선택해 주세요.");
            return;
        }

        int tempBuyCount = 1;

        var item = mItemDataList.sellList[mSelectIndex];
        if( Mng.data.myInfo.gold < item.userBuyPrice * tempBuyCount)
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

        Mng.data.myInfo.AddInventory(item.table, tempBuyCount, item.userBuyPrice);

        SelectItemIconUpdate(mSelectIndex);
        ItemIconUpdate();
        MyGoldUpdate();
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
