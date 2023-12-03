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

    long mSelectUID = ConstDef.NONE;

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

        mSelectUID = ConstDef.NONE;

        SelectItemIconUpdate(mSelectUID);
    }

    // Update is called once per frame
    void Update()
    {

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
            var item = mItemDataList.sellList.Find(_p => _p.uid == _uid);
            kSelectItemImage.sprite = Mng.canvas.GetSprite(item.table.AtlasName, item.table.SpriteName);
            kSelectItemName.text = item.table.Name;
            kSelectItemPrice.text = item.userBuyPrice.ToString();
            kSelectItemCount.text = item.count.ToString();
        }
    }

    int ItemIconUpdate()
    {
        for (int i = 0; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
            mItemIconList[i].gameObject.SetActive(false);

        int iconIndex = 0;
        foreach (var data in mItemDataList.sellList)
        {
            if (data.count == 0)
                continue;    

            mItemIconList[iconIndex].gameObject.SetActive(true);
            var sprite = Mng.canvas.GetSprite(data.table.AtlasName, data.table.SpriteName);

            var icon = mItemIconList[iconIndex];
            icon.Set(data.uid, sprite, data.userBuyPrice, data.count);
            iconIndex++;
        }

        return iconIndex;
    }

    public void SetCitySellItem(CityType _type)
    {
        mItemDataList = Mng.data.GetSellItemList(_type);

        int fillCount = ItemIconUpdate();        

        for (int i = fillCount; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
            mItemIconList[i].gameObject.SetActive(false);
    }
    
    void OnItemIconClick(long _uid)
    {
        mSelectUID = _uid;

        SelectItemIconUpdate(mSelectUID);
    }

    public void OnBuyItemButtonClick()
    {
        if( mSelectUID == ConstDef.NONE )
        {
            Debug.Log("������ �������� ������ �ּ���.");
            return;
        }

        int tempBuyCount = 1;

        var item = mItemDataList.sellList.Find(_p => _p.uid == mSelectUID);
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

        ItemIconUpdate();
        MyGoldUpdate();        
        SelectItemIconUpdate(mSelectUID);
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
