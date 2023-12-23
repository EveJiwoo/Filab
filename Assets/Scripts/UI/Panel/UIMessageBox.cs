using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageBox
{
    static public void Open(string _msg, Action _actConfirm)
    {
        Mng.canvas.kMessageBox.gameObject.SetActive(true);
        Mng.canvas.kMessageBox.Open(_msg, _actConfirm);
    }

    static public void Open(string _msg, Action _actYes, Action _actNo)
    {
        Mng.canvas.kMessageBox.gameObject.SetActive(true);
        Mng.canvas.kMessageBox.Open(_msg, _actYes, _actNo);
    }
}

public class UIMessageBox : UIBase
{
    public TMP_Text kMessageText;

    public Button kConfirmButton;
    public Button kYesButton;
    public Button kNoButton;

    Action mConfirmButtonAct;
    Action mYesButtonAct;
    Action mNoButtonAct;

    private void Reset()
    {
        kConfirmButton.gameObject.SetActive(false);
        kYesButton.gameObject.SetActive(false);
        kNoButton.gameObject.SetActive(false);

        mConfirmButtonAct = default;
        mYesButtonAct = default;
        mNoButtonAct = default;
    }

    public void Open(string _msg, Action _actConfirm)
    {
        Reset();

        kConfirmButton.gameObject.SetActive(true);        
        
        kMessageText.text = _msg;
        mConfirmButtonAct = _actConfirm;
    }

    public void Open(string _msg, Action _actYes, Action _actNo)
    {
        Reset();

        kYesButton.gameObject.SetActive(true);
        kNoButton.gameObject.SetActive(true);

        kMessageText.text = _msg;
        mYesButtonAct = _actYes;
        mNoButtonAct = _actNo;
    }


    public void OnConfirmButtonClick()
    {
        gameObject.SetActive(false);
        mConfirmButtonAct?.Invoke();
    }

    public void OnYesButtonClick()
    {
        gameObject.SetActive(false);
        mYesButtonAct?.Invoke();
    }

    public void OnNoButtonClick()
    {
        gameObject.SetActive(false);
        mNoButtonAct?.Invoke();
    }
}
