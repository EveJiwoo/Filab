using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UIItemIcon : MonoBehaviour
{
    public Image kSprite;
    public TMP_Text kCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(Sprite _sprite)
    {
        kSprite.sprite = _sprite;
    }
}
