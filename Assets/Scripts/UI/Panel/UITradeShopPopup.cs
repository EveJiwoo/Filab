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

    // Start is called before the first frame update
    void Awake()
    {
        mItemIconList = kItemListGo.GetComponentsInChildren<UIItemIcon>();
        foreach (var item in mItemIconList)
            item.onClick += OnItemIconClick;

        SetCitySellItem(CityType.City1);
    }

    protected override void onDisable()
    {
        kScrollRect.verticalNormalizedPosition = 1f;

        kSelectItemImage.sprite = null;
        kSelectItemName.text = "";
        kSelectItemPrice.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCitySellItem(CityType _type)
    {
        mItemDataList = Mng.data.GetSellItemList(_type);

        int fillCount = 0;
        foreach (var item in mItemDataList.sellList) {
            mItemIconList[fillCount].gameObject.SetActive(true);
            mItemIconList[fillCount].SetSprite(Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName));
            mItemIconList[fillCount].SetPrice(item.sellPrice);
            fillCount++;
        }

        for (int i = fillCount; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
        {
            mItemIconList[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        mSelectIndex = ConstDef.NONE;
    }        

    int mSelectIndex = ConstDef.NONE;
    void OnItemIconClick(int _index)
    {
        mSelectIndex = _index;
        var item = mItemDataList.sellList[mSelectIndex];
        kSelectItemImage.sprite = Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName);
        kSelectItemName.text = item.table.Name;
        kSelectItemPrice.text = item.sellPrice.ToString();
    }

    public void OnBuyItem()
    {
        if( mSelectIndex == ConstDef.NONE )
        {
            Debug.Log("구입할 아이템을 선택해 주세요.");
            return;
        }

        int tempBuyCount = 1;

        var item = mItemDataList.sellList[mSelectIndex];
        if( Mng.data.myInfo.gold < item.sellPrice * tempBuyCount)
        {
            Debug.Log("소지금이 부족 합니다.");
            return;
        }

        Mng.data.myInfo.gold -= item.sellPrice * tempBuyCount;

        if ( item.count <= 0 || item.count < tempBuyCount)
        {
            Debug.Log("상품 갯수가 부족 합니다.");
            return;
        }

        //구입
        item.count -= tempBuyCount;
    }
}
