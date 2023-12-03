using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIItemIcon : MonoBehaviour
{
    public Action<long> onClick;
    
    public long mUid;

    public Image kSprite;
    public TMP_Text kCountText;
    public TMP_Text kPriceText;

    private void Awake()
    {
        //kPriceText.text = "";
    }

    public void Set(long _uid, Sprite _sprite, long _price, int _count)
    {
        mUid = _uid;

        kSprite.sprite = _sprite;
        kPriceText.text = "";
        kCountText.text = "";

        //가격이 없으면 감춤
        if (_price == 0)
            kPriceText.text = "";
        else
            kPriceText.text = _price.ToString() + " G";

        kCountText.text = _count.ToString();
    }

    public void OnIconButtonClick()
    {
        onClick?.Invoke(mUid);
    }
}
