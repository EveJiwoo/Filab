using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox
{
    static public void Open(string _msg, Action _act)
    {
        Mng.canvas.kMessageBox.gameObject.SetActive(true);
        Mng.canvas.kMessageBox.Open(_msg, _act);
    }
}

public class UIMessageBox : MonoBehaviour
{
    public TMP_Text kMessageText;

    public Button kConfirmButton;

    Action mConfirmButtonAct;


    public void Open(string _msg, Action _act)
    {        
        kMessageText.text = _msg;
        mConfirmButtonAct = _act;
    }

    public void OnConfirmButtonClick()
    {
        gameObject.SetActive(false);
        mConfirmButtonAct?.Invoke();
    }
}
