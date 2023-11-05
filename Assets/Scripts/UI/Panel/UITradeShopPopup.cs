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
    List<ItemDataTable_Client> mItemDataList;
    [Header("������ ����Ʈ ��ũ��")]
    public ScrollRect kScrollRect;
    [Header("���õ� ������ �̹���")]
    public Image kSelectItemImage;
    [Header("���õ� ������ �̸�")]
    public TMP_Text kSelectItemName;
    [Header("���õ� ������ ����")]
    public TMP_Text kSelectItemPrice;

    // Start is called before the first frame update
    void Awake()
    {
        mItemIconList = kItemListGo.GetComponentsInChildren<UIItemIcon>();
        foreach (var item in mItemIconList)
            item.onClick += OnItemIconClick;

        SetItemData(CityType.City1);
    }

    private void OnDisable()
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

    public void SetItemData(CityType _type)
    {
        mItemDataList = Mng.data.GetSellItemList(_type);
        
        int fillCount = 0;
        foreach(var tableData in mItemDataList){
            mItemIconList[fillCount].gameObject.SetActive(true);
            mItemIconList[fillCount].SetSprite(Mng.canvas.GetSprite(tableData.AtlasName, tableData.SpriteName));
            fillCount++;
        }        

        for(int i = fillCount;i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
        {
            mItemIconList[i].gameObject.SetActive(false);
        }
    }

    void OnItemIconClick(int _index)
    {
        var data = mItemDataList[_index];
        kSelectItemImage.sprite = Mng.canvas.GetSprite(data.AtlasName, data.SpriteName);
        kSelectItemName.text = data.Name;
        kSelectItemPrice.text = data.OrignalBuyPrice.ToString();
    }
}
