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
    public TMP_Text kSellPriceText;

    public void SetSprite(Sprite _sprite)
    {
        kSprite.sprite = _sprite;
    }

    public void OnIconButtonClick()
    {
        onClick?.Invoke(transform.GetSiblingIndex());
    }
}
