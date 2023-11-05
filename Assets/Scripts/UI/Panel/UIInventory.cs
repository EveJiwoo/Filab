using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public GameObject kItemListGo;

    UIItemIcon[] mItemIconList;

    // Start is called before the first frame update
    void Awake()
    {
        mItemIconList = kItemListGo.GetComponentsInChildren<UIItemIcon>();
        for(int i = 0; i < ConstDef.MAX_ITEM_TYPE_COUNT; i++)
        {
            var table = Mng.table.FindItemDataTable_Client(200000000001 + i);
            mItemIconList[i].SetSprite( Mng.canvas.GetSprite(table.AtlasName, table.SpriteName) );
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
