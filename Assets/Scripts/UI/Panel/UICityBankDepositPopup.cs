using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICityBankDepositPopup : UIBase
{
    [Header("ù �޴�")]
    public GameObject kFrontMenuGo;

    [Header("�Ա� �޴�")]
    public GameObject kDepositMenuGo;

    [Header("��� �޴�")]
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
