using EnumDef;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICityBankCDAccountPopup : UIBase
{
    [Header("첫 메뉴")]
    public GameObject kFrontMenuGo;

    [Header("정기 예금 상품 메뉴")]
    public GameObject kOpenCDofferedGo;
    
    [Header("상품 정보 메뉴")]
    public GameObject kSavingAmountGo;

    [Header("상품1")]
    public GameObject kProductGo1;
    public TMP_Text kProductRate1;
    public TMP_Text kProductTerm1;
    public Button kProductSelectButton1;
    [Header("상품2")]
    public GameObject kProductGo2;
    public TMP_Text kProductRate2;
    public TMP_Text kProductTerm2;
    public Button kProductSelectButton2;
    [Header("상품3")]
    public GameObject kProductGo3;
    public TMP_Text kProductRate3;
    public TMP_Text kProductTerm3;
    public Button kProductSelectButton3;

    [Header("예금액")]
    public TMP_InputField kDepositAmount;

    [Header("월 이자금")]
    public TMP_Text kMonthlyInterestGold;
    [Header("총 이자금")]
    public TMP_Text kTotalInterestEarned;
    [Header("만기일")]
    public TMP_Text kMaturityDate;

    int mSelectButtonIndex = ConstDef.NONE;

    // Start is called before the first frame update
    void Start()
    {
        CDProductListUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOpenCDAccountButtonClick()
    {
        kFrontMenuGo.SetActive(false);
        kOpenCDofferedGo.SetActive(true);
        kSavingAmountGo.SetActive(true);
    }

    public void OnCloseButton()
    {
        gameObject.SetActive(false);
    }

    void CDProductListUpdate()
    {
        kProductGo1.gameObject.SetActive(false);
        kProductGo2.gameObject.SetActive(false);
        kProductGo3.gameObject.SetActive(false);

        var bankInfo = Mng.data.GetBankInfo(Mng.data.myInfo.local);
        
        for (int i = 0; i < bankInfo.cdList.Count; i++)
        {
            var rate = (bankInfo.cdList[i].interestRate * 100f).ToFloatN2String() + "%";
            var term = bankInfo.cdList[i].term.ToString() + " year";
            switch (i)
            {
                case 0:
                    {
                        kProductGo1.SetActive(true);
                        kProductRate1.text = rate;
                        kProductTerm1.text = term;
                    } break;
                case 1:
                    {
                        kProductGo2.SetActive(true);
                        kProductRate2.text = rate;
                        kProductTerm2.text = term;
                    } break;
                case 2:
                    {
                        kProductGo1.SetActive(true);
                        kProductRate3.text = rate;
                        kProductTerm3.text = term;
                    } break;

            }
        }
    }

    public void OnSelectButtonClick(Button _button)
    {
        if( _button == kProductSelectButton1 )
        {
            mSelectButtonIndex = 0;            
        }
        else if(_button == kProductSelectButton2)
        {
            mSelectButtonIndex = 1;
        }
        else if(_button == kProductSelectButton3)
        {
            mSelectButtonIndex = 2;
        }

        kDepositAmount.text = "0";
        kMonthlyInterestGold.text = "0 Gold";
        kTotalInterestEarned.text = "0 Gold";

        var bankInfo = Mng.data.GetBankInfo(Mng.data.myInfo.local);
        
        var cd = bankInfo.cdList[mSelectButtonIndex];
        var maturityDate = Mng.data.curDateTime.AddYears(cd.term);
        kMaturityDate.text = $"{maturityDate.Year}.{maturityDate.Month}.{maturityDate.Day}"; 
    }

    public void OnChangeSavingAmount()
    {
        if (mSelectButtonIndex == ConstDef.NONE)
            return;

        var bankInfo = Mng.data.GetBankInfo(Mng.data.myInfo.local);
        var cd = bankInfo.cdList[mSelectButtonIndex];

        long amountGold = long.Parse(kDepositAmount.text);
        if(amountGold <= 0)
        {
            kMonthlyInterestGold.text = "0 Gold";
            kTotalInterestEarned.text = "0 Gold";            
            kMaturityDate.text = "";
        }
        else
        {
            kMonthlyInterestGold.text = $"{ (int)(amountGold * (cd.interestRate / 12f)) } Gold";
            kTotalInterestEarned.text = $"{ (int)(amountGold * (cd.interestRate * cd.term))} Gold";
            var maturityDate = Mng.data.curDateTime.AddYears(cd.term);
            kMaturityDate.text = $"{maturityDate.Year}.{maturityDate.Month}.{maturityDate.Day}";
        }
    }
}
