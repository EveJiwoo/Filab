using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICityBankLoanPopup : UIBase
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCloseButton()
    {
        gameObject.SetActive(false);
    }
}
