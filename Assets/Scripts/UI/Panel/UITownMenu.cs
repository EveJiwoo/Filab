using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITownMenu : MonoBehaviour
{
    [Header("내 골드 표기")]
    public TMP_Text kMyGoldText;

    [Header("년/월/일 표기")]
    public TMP_Text kYMDText;
    [Header("시:분 표기")]
    public TMP_Text kHMText;

    [Header("테스트 금리 표기")]
    public TMP_Text kRateText;

    // Start is called before the first frame update
    void Start()
    {
        MyGoldUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MyGoldUpdate()
    {
        kMyGoldText.text = Mng.data.myInfo.gold.ToColumnString();
    }

    public void SetDateTime(DateTime _time)
    {
        kYMDText.text = $"{_time.Year} / {_time.Month} / {_time.Day}";

        kHMText.text = $"{StringHelper.ConvertInteger_D(_time.Hour, 2)} : {StringHelper.ConvertInteger_D(_time.Minute, 2)}";
    }

    public void TempRate(float _rate)
    {
        kRateText.text = $"{_rate * 100}%";
    }

    public void OnInventoryButtonClick()
    {
        Mng.canvas.kInventory.gameObject.SetActive(true);
    }

    public void OnFinancialsButtonClick()
    {
        Mng.canvas.kFinancialPopup.gameObject.SetActive(true);
    }
}
