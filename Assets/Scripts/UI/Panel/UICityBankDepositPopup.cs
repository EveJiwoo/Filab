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
    [Header("입금액")]
    public TMP_InputField kDepositTextInput;

    [Header("출금 메뉴")]
    public GameObject kWithdrawMenuGo;
    [Header("출금액")]
    public TMP_InputField kWithdrawTextInput;
    [Header("현재 내 금액")]
    public TMP_InputField kMyGoldTextInput;

    [Header("메세지")]
    public TMP_Text kSpeechMessage;

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

        kSpeechMessage.text =
            $"Welcome to Kabes Bank.\nYou current have {Mng.data.myInfo.bank.depositGold.ToColumnString()} gold in your account.\nHow can I help you ?";
    }

    public void OnDepositMenuButtonClick()
    {
        kFrontMenuGo.SetActive(false);
        kDepositMenuGo.SetActive(true);

        kDepositTextInput.text = "0";
    }

    /// <summary> 예금하려는 금액 숫자를 점검 후 교정 </summary>
    bool DepositRangeCheck()
    {
        long depositGold = long.Parse(kDepositTextInput.text);
        if (depositGold <= 0)
        {
            kDepositTextInput.text = "0";
            return false;
        }

        if (depositGold > Mng.data.myInfo.gold)
        {
            kDepositTextInput.text = Mng.data.myInfo.gold.ToString();
            return false;
        }

        return true;
    }

    public void OnEditDepositInput()
    {
        //DepositRangeCheck();
    }

    public void OnDepositButtonClick()
    {
        if (DepositRangeCheck() == false)
            return;

        long depositGold = long.Parse(kDepositTextInput.text);        

        Mng.data.myInfo.gold -= depositGold;
        Mng.data.myInfo.bank.depositGold += depositGold;
        Mng.canvas.kTownMenu.MyGoldUpdate();

        onEnable();
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

        kMyGoldTextInput.text = Mng.data.myInfo.bank.depositGold.ToColumnString() + " G";
        kWithdrawTextInput.text = "0";
    }

    /// <summary> 출금하려는 금액 숫자를 점검 후 교정 </summary>
    bool WithdrawRangeCheck()
    {
        long withdrawInput = long.Parse(kWithdrawTextInput.text);
        if (withdrawInput < 0)
        {
            kWithdrawTextInput.text = "0";
            return false;
        }

        if (withdrawInput > Mng.data.myInfo.bank.depositGold)
        {
            kWithdrawTextInput.text = Mng.data.myInfo.bank.depositGold.ToString();
            return false;
        }

        return true;
    }
    
    public void OnEditWithdrawAmountInput()
    {
        //WithdrawRangeCheck();
    }

    public void OnWithdrawButtonClick()
    {
        if (WithdrawRangeCheck() == false)
            return;

        long withdrawInput = long.Parse(kWithdrawTextInput.text);

        Mng.data.myInfo.bank.depositGold -= withdrawInput;
        Mng.data.myInfo.gold += withdrawInput;
        Mng.canvas.kTownMenu.MyGoldUpdate();

        onEnable();
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
