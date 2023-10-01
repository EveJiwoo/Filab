using EnumDef;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    static public MainCanvas Instance;
/*
    public UIIngamePanel kIngame;
    public UIResultPanel kResult;
    public UITitlePanel kTitle;
*/
    private void Awake()
    {
        Instance = this;
/*
        kIngame = GetComponentInChildren<UIIngamePanel>(true);
        kResult = GetComponentInChildren<UIResultPanel>(true);
        kTitle = GetComponentInChildren<UITitlePanel>(true);
*/
    }
}
