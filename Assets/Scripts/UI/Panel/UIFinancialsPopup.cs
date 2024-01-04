using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFinancialsPopup : UIBase
{
    [Header("대출 목록")]
    public GameObject kLoanMenuGo;
    public List<Button> kLoanButtonList = new List<Button>();
    public List<TMP_Text> kLoanBankNameList = new List<TMP_Text>();
    public List<TMP_Text> kLoanGoldList = new List<TMP_Text>();
    public List<TMP_Text> kLoanRateList = new List<TMP_Text>();
    public TMP_Text kSelectLoanAmountGold;
    public TMP_Text kSelectLoanMonthlyPaymnetGold;
    public TMP_Text kSelectLoanInterestRate;
    public TMP_Text kSelectLoanTerm;
    public TMP_Text kSelectLoanDate;
    public TMP_Text kSelectLoanMaturityDate;
    public TMP_Text kSelectLoanInterestPaidGold;

    [Header("정기 예금 목록")]
    public GameObject kSaveMenuGo;
    public List<Button> kSaveButtonList = new List<Button>();
    public List<TMP_Text> kSaveBankNameList = new List<TMP_Text>();
    public List<TMP_Text> kSaveGoldList = new List<TMP_Text>();
    public List<TMP_Text> kSaveRateList = new List<TMP_Text>();
    public TMP_Text kSelectSavingAmountGold;
    public TMP_Text kSelectSavingMonthlyInterestGold;
    public TMP_Text kSelectSavingInterestRate;
    public TMP_Text kSelectSavingTerm;
    public TMP_Text kSelectSavingOpenDate;
    public TMP_Text kSelectSavingMaturityDate;

    [Header("정기 예금 목록")]
    public TMP_Text kFreeDepositGold;
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    protected override void onEnable()
    {
        kFreeDepositGold.text = Mng.data.myInfo.freeDepositGold.ToString() + " G";

        OnLoanTapButtonClick();
    }


    public void OnLoanTapButtonClick()
    {
        kLoanMenuGo.SetActive(true);
        kSaveMenuGo.SetActive(false);

        OnSelectLoanInfo(-1);
        LoanListUpdate();
    }

    void LoanListUpdate()
    {
        foreach (var button in kLoanButtonList)
            button.gameObject.SetActive(false);

        for (int i = 0; i < Mng.data.myInfo.loanCondtionList.Count; i++)
        {
            var loanInfo = Mng.data.myInfo.loanCondtionList[i];

            kLoanButtonList[i].gameObject.SetActive(true);
            kLoanBankNameList[i].text = loanInfo.city.ToString();
            kLoanGoldList[i].text = loanInfo.loanGold.ToString() + " G";
            kLoanRateList[i].text = (loanInfo.interestRate * 100f).ToFloatN2String() + "%";
        }
    }

    public void OnSelectLoanListButtonClick(int _selectIndex)
    {
        OnSelectLoanInfo(_selectIndex);
    }

    void OnSelectLoanInfo(int _selectIndex = -1)
    {
        kSelectLoanAmountGold.text = "0 G";
        kSelectLoanMonthlyPaymnetGold.text = "0 G";
        kSelectLoanInterestRate.text = "0%";
        kSelectLoanTerm.text = "- years";
        kSelectLoanDate.text = "--/--/----";
        kSelectLoanMaturityDate.text = "--/--/----";
        kSelectLoanInterestPaidGold.text = "0 G";

        if (_selectIndex == -1)
            return;

        var loanInfo = Mng.data.myInfo.loanCondtionList[_selectIndex];
        
        kSelectLoanAmountGold.text = loanInfo.loanGold.ToString() + " G";
        kSelectLoanMonthlyPaymnetGold.text = ((int)(loanInfo.loanGold * (loanInfo.interestRate / 12f))).ToString() + " G";
        kSelectLoanInterestRate.text = (loanInfo.interestRate * 100f).ToFloatN2String() + "%";
        kSelectLoanTerm.text = loanInfo.term.ToString() + " years";
        kSelectLoanDate.text = $"{loanInfo.contractDate.Day}/{loanInfo.contractDate.Month}/{loanInfo.contractDate.Year}";
        kSelectLoanMaturityDate.text = $"{loanInfo.maturityDate.Day}/{loanInfo.maturityDate.Month}/{loanInfo.maturityDate.Year}"; ;
        kSelectLoanInterestPaidGold.text = loanInfo.interestPayGold.ToString() + " G";
    }

    public void OnSaveTapButtonClick()
    {
        kLoanMenuGo.SetActive(false);
        kSaveMenuGo.SetActive(true);

        OnSelectSaveInfo(-1);
        SaveListUpdate();
    }

    void SaveListUpdate()
    {
        foreach (var button in kSaveButtonList)
            button.gameObject.SetActive(false);

        for (int i = 0; i < Mng.data.myInfo.cdProductList.Count; i++)
        {
            var saveInfo = Mng.data.myInfo.cdProductList[i];

            kSaveButtonList[i].gameObject.SetActive(true);
            kSaveBankNameList[i].text = saveInfo.city.ToString();
            kSaveGoldList[i].text = saveInfo.depositeGold.ToString() + " G";
            kSaveRateList[i].text = (saveInfo.interestRate * 100f).ToFloatN2String() + "%";
        }
    }

    public void OnSelectSaveListButtonClick(int _selectIndex)
    {
        OnSelectSaveInfo(_selectIndex);
    }

    void OnSelectSaveInfo(int _selectIndex = -1)
    {
        kSelectSavingAmountGold.text = "0 G";
        kSelectSavingMonthlyInterestGold.text = "0 G";
        kSelectSavingInterestRate.text = "0%";
        kSelectSavingTerm.text = "- years";
        kSelectSavingOpenDate.text = "--/--/----";
        kSelectSavingMaturityDate.text = "--/--/----"; ;

        if (_selectIndex == -1)
            return;

        var saveInfo = Mng.data.myInfo.cdProductList[_selectIndex];

        kSelectSavingAmountGold.text = saveInfo.depositeGold.ToString() + " G";
        kSelectSavingMonthlyInterestGold.text = ((int)(saveInfo.depositeGold * (saveInfo.interestRate / 12f))).ToString() + " G";
        kSelectSavingInterestRate.text = (saveInfo.interestRate * 100f).ToFloatN2String() + "%";
        kSelectSavingTerm.text = saveInfo.term.ToString() + " years";
        kSelectSavingOpenDate.text = $"{saveInfo.openDate.Day}/{saveInfo.openDate.Month}/{saveInfo.openDate.Year}";
        kSelectSavingMaturityDate.text = $"{saveInfo.maturityDate.Day}/{saveInfo.maturityDate.Month}/{saveInfo.maturityDate.Year}"; ;        
    }

    public void OnConfirmButtonClick()
    {
        gameObject.SetActive(false);
    }
}
