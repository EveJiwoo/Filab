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
    public UITownMenu kTownMenu;

    [HideInInspector]
    public UICityBankDepositPopup kCityBankPopup;
    [HideInInspector]
    public UICityBankCDAccountPopup kCityBankCDAccountPopup;
    [HideInInspector]
    public UICityBankLoanPopup kCityBankLoanPopup;


    Dictionary<string, SpriteAtlas> mSpriteAtlasList = new Dictionary<string, SpriteAtlas>();

    private void Awake()
    {
        Instance = this;

        kTownMenu = GetComponentInChildren<UITownMenu>(true);
        kCityBankPopup = GetComponentInChildren<UICityBankDepositPopup>(true);
        kCityBankCDAccountPopup = GetComponentInChildren<UICityBankCDAccountPopup>(true);
        kCityBankLoanPopup = GetComponentInChildren<UICityBankLoanPopup>(true);
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
