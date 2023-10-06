using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITownMenu : MonoBehaviour
{
    [Header("년/월/일 표기")]
    public TMP_Text kYMDText;
    [Header("시:분 표기")]
    public TMP_Text kHMText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDateTime(DateTime _time)
    {
        kYMDText.text = $"{_time.Year} / {_time.Month} / {_time.Day}";

        kHMText.text = $"{StringHelper.ConvertInteger_D(_time.Hour, 2)} : {StringHelper.ConvertInteger_D(_time.Minute, 2)}";
    }
}
