using EnumDef;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class MainCanvas : MonoBehaviour
{
    static public MainCanvas Instance;
    
    [HideInInspector]
    public UITopMenu kTopMenu;

    [HideInInspector]
    public UICityBankDepositPopup kCityBankPopup;
    [HideInInspector]
    public UICityBankCDAccountPopup kCityBankCDAccountPopup;
    [HideInInspector]
    public UICityBankLoanPopup kCityBankLoanPopup;
    [HideInInspector]
    public UITradeShopPopup kTradeShopPopup;
    [HideInInspector]
    public UIInventory kInventory;
    [HideInInspector]
    public UIFinancialsPopup kFinancialPopup;

    [HideInInspector]
    public UIMessageBox kMessageBox;

    Dictionary<string, SpriteAtlas> mSpriteAtlasList = new Dictionary<string, SpriteAtlas>();

    private void Awake()
    {
        Instance = this;

        kTopMenu = GetComponentInChildren<UITopMenu>(true);
        kCityBankPopup = GetComponentInChildren<UICityBankDepositPopup>(true);
        kCityBankCDAccountPopup = GetComponentInChildren<UICityBankCDAccountPopup>(true);
        kCityBankLoanPopup = GetComponentInChildren<UICityBankLoanPopup>(true);
        kTradeShopPopup = GetComponentInChildren<UITradeShopPopup>(true);
        kInventory = GetComponentInChildren<UIInventory>(true);
        kFinancialPopup = GetComponentInChildren<UIFinancialsPopup>(true);
        kMessageBox = GetComponentInChildren<UIMessageBox>(true);
    }

    public Sprite GetSprite(string _atlasName, string _spriteName)
    {
        if (mSpriteAtlasList.ContainsKey(_atlasName) == false)
        {
            var atlas = Resources.Load<SpriteAtlas>("Atlas/" + _atlasName);
            mSpriteAtlasList[_atlasName] = atlas;
        }

        return mSpriteAtlasList[_atlasName].GetSprite(_spriteName);
    }
}
