using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : UIBase
{
    public GameObject kItemListGo;

    UIItemIcon[] mItemIconList;

    // Start is called before the first frame update
    void Awake()
    {
        mItemIconList = kItemListGo.GetComponentsInChildren<UIItemIcon>();
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //인벤토리에 가격 제거
    protected override void onEnable()
    {
        for (int i = 0; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
        {
            if( i < Mng.data.myInfo.invenItemInfoList.Count)
            {
                mItemIconList[i].gameObject.SetActive(true);

                var itemInfo = Mng.data.myInfo.invenItemInfoList[i];
                
                if (itemInfo.table == null)
                    itemInfo.table = Mng.table.FindItemDataTable(itemInfo.uid);

                var sprite = Mng.canvas.GetSprite(itemInfo.table.AtlasName, itemInfo.table.SpriteName);
                mItemIconList[i].Set(itemInfo.table.UID, sprite, 0, itemInfo.count);
            }
            else
            {
                mItemIconList[i].gameObject.SetActive(false);
            }            
        }
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
