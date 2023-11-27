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

    protected override void onEnable()
    {
        for (int i = 0; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
        {
            if( i < Mng.data.myInfo.invenItemInfoList.Count)
            {
                mItemIconList[i].gameObject.SetActive(true);

                var itemInfo = Mng.data.myInfo.invenItemInfoList[i];

                var table = Mng.table.FindItemDataTable_Client(itemInfo.uid);
                mItemIconList[i].SetSprite(Mng.canvas.GetSprite(table.AtlasName, table.SpriteName));
                mItemIconList[i].SetCount(itemInfo.count);
            }
            else
            {
                mItemIconList[i].gameObject.SetActive(false);
            }            
        }
    }
}
