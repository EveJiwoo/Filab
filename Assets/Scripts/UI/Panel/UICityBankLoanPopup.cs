using ClassDef;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICityBankLoanPopup : UIBase
{
    [Header("첫 메뉴")]
    public GameObject kFrontMenuGo;

    [Header("대출 상품 조건 설정")]
    public GameObject kLoanProductGo;
    //왼쪽 메뉴
    public TMP_Text kLoanAmountTxt;
    public Slider kLoanAmountSlider;
    public TMP_Text kInterestRateTxt;
    public TMP_Text kProductTermTxt;
    public Slider kTermSlider;
    //오른쪽 메뉴
    public TMP_Text kTotalScheduledPaymentTxt;
    public TMP_Text kYearlyPaymentTxt;
    public TMP_Text kMonthlyPaymentTxt;
    public TMP_Text kPaymentDatesTxt;

    [Header("대출 상품 상태 보기")]
    public GameObject kLoanConditionGo;
    //왼쪽 메뉴
    public TMP_Text kConditionLoanAmountTxt;
    public TMP_Text kConditionInterestRateTxt;
    public TMP_Text kConditionTermTxt;
    public TMP_Text kCurrentPrincipalTxt;
    //오른쪽 메뉴
    public TMP_Text kLoanDateTxt;
    public TMP_Text kTotalPrincipalPaymentTxt;
    public TMP_Text kTotalInterestPaymentTxt;
    public TMP_Text kTotalPaidTxt;
    public Button kMakePaymnetButton;

    LoanCondition mBankLoan;
    LoanCondition mMyLoan;
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
        ResetMenu();

        var bankInfo = Mng.data.GetBankInfo(Mng.data.myInfo.local);
        mBankLoan = bankInfo.loan;

        kPaymentDatesTxt.text = $"Every {Mng.data.curDateTime.Day}th of the month";

        mMyLoan = Mng.data.myInfo.loanCondtionList.FirstOrDefault(_p => _p.city == Mng.data.myInfo.local);
    }

    public void OnApplayForLoanButtonClick()
    {
        if( Mng.data.myInfo.loanCondtionList.Count >= ConstDef.MAX_LOAN_PRODUCT_SERVICE_COUNT )
        {
            //메세지 처리
            return;
        }

        //해당 은행에서는 받은적 없음 : 은행 대출 상품 정보 출력
        if (mMyLoan == default)
        {
            kFrontMenuGo.SetActive(false);
            kLoanProductGo.SetActive(true);

            kLoanAmountSlider.value = 0;
            kTermSlider.value = 0;

            kInterestRateTxt.text = (mBankLoan.interestRate * 100f).ToFloatN2String() + " %";

            LoanProductInfoUpdate();
        }
        //이미 해당 은행에서 대출 받음 : 대출 받은 상품 정보 출력
        else
        {
            kFrontMenuGo.SetActive(false);
            kLoanConditionGo.SetActive(true);

            //상환 버튼 활성화/비활성화
            if (mMyLoan.curPrincipal <= Mng.data.myInfo.gold)
                kMakePaymnetButton.interactable = true;
            else
                kMakePaymnetButton.interactable = false;

            LoanConditionInfoUpdate();
        }
    }

    void LoanConditionInfoUpdate()
    {
        //대출 원금
        kConditionLoanAmountTxt.text = mMyLoan.loanGold.ToColumnString();
        //대출 이자
        kConditionInterestRateTxt.text = $"{(mMyLoan.interestRate*100f).ToFloatN2String()} %";
        //기간
        kConditionTermTxt.text = $"{mMyLoan.term} Year";
        //남은 상환금        
        kCurrentPrincipalTxt.text = $"{(mMyLoan.curPrincipal).ToColumnString()} Gold";
    
        kLoanDateTxt.text = $"{mMyLoan.maturityDate.Year}/{mMyLoan.maturityDate.Month}/{mMyLoan.maturityDate.Day}";
        kTotalPrincipalPaymentTxt.text = $"{mMyLoan.principalPayGold.ToColumnString()} Gold";
        kTotalInterestPaymentTxt.text = $"{mMyLoan.interestPayGold.ToColumnString()} Gold"; ;
        kTotalPaidTxt.text = $"{(mMyLoan.principalPayGold + mMyLoan.interestPayGold).ToColumnString()} Gold";
    }

    public void OnLoanAmoutSlider()
    {
        kLoanAmountTxt.text = $"{(int)kLoanAmountSlider.value * 1000} Gold";

        LoanProductInfoUpdate();
    }

    public void OnTermSlider()
    {
        kProductTermTxt.text = $"{(int)kTermSlider.value} Year";

        LoanProductInfoUpdate();
    }

    /// <summary> 상품 정보 갱신 </summary>
    void LoanProductInfoUpdate()
    {   
        
        if(kTermSlider.value == 0f || kLoanAmountSlider.value == 0f)
        {
            kTotalScheduledPaymentTxt.text = "0 Gold";
            kYearlyPaymentTxt.text = "0 Gold";
            kMonthlyPaymentTxt.text = "0 Gold";
        }
        else
        {
            int loanGold = (int)kLoanAmountSlider.value * 1000;
            float interestRate = mBankLoan.interestRate;
            int loanTerm = (int)kTermSlider.value;

            int totalPayGold = ((int)(loanGold * (1f + interestRate * loanTerm)));
            kTotalScheduledPaymentTxt.text = totalPayGold.ToString() + " Gold";

            kYearlyPaymentTxt.text = ((int)(totalPayGold / loanTerm)).ToString() + " Gold";

            kMonthlyPaymentTxt.text = ((int)(totalPayGold / (loanTerm * 12f))).ToString() + " Gold";
        }
    }

    public void OnLoanApplyButtonClick()
    {
        LoanCondition loan = new LoanCondition();
        loan.city = Mng.data.myInfo.local;
        loan.loanGold = (int)kLoanAmountSlider.value * 1000;
        loan.interestRate = mBankLoan.interestRate;
        loan.term = (int)kTermSlider.value;
        loan.maturityDate = Mng.data.curDateTime.AddYears(loan.term).AddMonths(1);
        loan.contractDate = Mng.data.curDateTime;
        loan.NextPayDateUpdate();

        Mng.data.myInfo.loanCondtionList.Add(loan);
        
        mMyLoan = loan;

        ResetMenu();
    }

    public void OnLoanCancelButtonClick()
    {
        ResetMenu();
    }

    void ResetMenu()
    {
        kFrontMenuGo.SetActive(true);
        kLoanConditionGo.SetActive(false);
        kLoanProductGo.SetActive(false);
    }

    public void OnMakePaymentButtonClick()
    {
        //남은 상환 원금 표시
        MessageBox.Open($"The remaining loan is {(mMyLoan.loanGold - mMyLoan.principalPayGold).ToColumnString()} Gold.\nDo you want to repay them all?",
            () => {
                Mng.data.myInfo.gold -= mMyLoan.loanGold - mMyLoan.principalPayGold;
                Mng.canvas.kTownMenu.MyGoldUpdate();
                Mng.data.myInfo.loanCondtionList.Remove(mMyLoan);
                mMyLoan = default;
                ResetMenu();
            },
            () => { }
        );
    }

    public void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}
