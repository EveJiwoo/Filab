using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    [Header("�˾� Ÿ���ΰ�?")]
    public bool kIsPopupType = false;

    // Update is called once per frame
    void OnEnable()
    {
        if( kIsPopupType == true )
        {
            Mng.play.player.isCanMove = false;
            Mng.play.isTimer = false;
        }
    }

    private void OnDisable()
    {
        if (kIsPopupType == true)
        {
            Mng.play.player.isCanMove = true;
            Mng.play.isTimer = true;
        }
    }
}
