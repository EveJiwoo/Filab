using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICityBankDepositPopup : UIBase
{
    [Header("첫 메뉴")]
    public GameObject kFrontMenuGo;

    [Header("입금 메뉴")]
    public GameObject kDepositMenuGo;

    [Header("출금 메뉴")]
    public GameObject kWithdrawMenuGo;
    public TMP_InputField kMyGoldTextInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void onEnable()
    {
        kFrontMenuGo.SetActive(true);
        kDepositMenuGo.SetActive(false);
        kWithdrawMenuGo.SetActive(false);
    }

    public void OnDepositMenuButtonClick()
    {
        kFrontMenuGo.SetActive(false);
        kDepositMenuGo.SetActive(true);
    }

    public void OnDepositMenuCancelButtonClick()
    {
        kFrontMenuGo.SetActive(true);
        kDepositMenuGo.SetActive(false);
    }

    public void OnWithdrawMenuButtonClick()
    {
        kFrontMenuGo.SetActive(false);
        kWithdrawMenuGo.SetActive(true);

        kMyGoldTextInput.text = Mng.data.myInfo.gold.ToColumnString() + " G";
    }

    public void OnWithdrawMenuCancelButtonClick()
    {
        kFrontMenuGo.SetActive(true);
        kWithdrawMenuGo.SetActive(false);        
    }

    public void OnCloseButton()
    {
        gameObject.SetActive(false);
    }
}
