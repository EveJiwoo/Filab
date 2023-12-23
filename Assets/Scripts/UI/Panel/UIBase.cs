using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [Header("팝업 타입인가?")]
    public bool kIsPopupType = false;

    static int mPopupPanelCount = 0;
    // Update is called once per frame
    void OnEnable()
    {
        if( kIsPopupType == true )
        {
            Mng.play.player.isCanMove = false;
            Mng.play.isTimer = false;
            mPopupPanelCount++;
        }

        onEnable();
    }

    protected virtual void onEnable() { }

    private void OnDisable()
    {
        if (kIsPopupType == true)
        {
            mPopupPanelCount--;

            if(mPopupPanelCount <= 0)
            {
                Mng.play.player.isCanMove = true;
                Mng.play.isTimer = true;
            }            
        }

        onDisable();
    }

    protected virtual void onDisable() {}
}
