using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIItemIcon : MonoBehaviour
{
    public Action<int> onClick;

    public Image kSprite;
    public TMP_Text kCountText;
    public TMP_Text kPriceText;

    private void Awake()
    {
        //kPriceText.text = "";
    }

    public void SetSprite(Sprite _sprite)
    {
        kSprite.sprite = _sprite;
        kPriceText.text = "";
        kCountText.text = "";
    }

    public void SetPrice(int _price)
    {
        kPriceText.text = _price.ToString() + " G";
    }

    public void SetCount(int _count)
    {
        kCountText.text = _count.ToString();
    }

    public void OnIconButtonClick()
    {
        onClick?.Invoke(transform.GetSiblingIndex());
    }
}
