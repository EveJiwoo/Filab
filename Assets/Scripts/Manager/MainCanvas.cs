using EnumDef;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    static public MainCanvas Instance;
    
    [HideInInspector]
    public UITownMenu kTownMenu;

    [HideInInspector]
    public UICityBankDepositPopup kCityBankPopup;
    [HideInInspector]
    public UICityBankCDAccountPopup kCityBankCDAccountPopup;
    [HideInInspector]
    public UICityBankLoanPopup kCityBankLoanPopup;

    private void Awake()
    {
        Instance = this;

        kTownMenu = GetComponentInChildren<UITownMenu>(true);
        kCityBankPopup = GetComponentInChildren<UICityBankDepositPopup>(true);
        kCityBankCDAccountPopup = GetComponentInChildren<UICityBankCDAccountPopup>(true);
        kCityBankLoanPopup = GetComponentInChildren<UICityBankLoanPopup>(true);
    }
}
