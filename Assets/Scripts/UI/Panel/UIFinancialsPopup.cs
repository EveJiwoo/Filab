using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFinancialsPopup : UIBase
{
    public List<TMP_Text> kTextList = new List<TMP_Text>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void onEnable()
    {
        foreach (var tmpt in kTextList)
            tmpt.text = "";

        for(int i = 0; i < Mng.data.myInfo.cdProductList.Count; i++)
        {
            var cdInfo = Mng.data.myInfo.cdProductList[i];
            kTextList[i].text = $"Gold : {cdInfo.depositeGold.ToColumnString()}G Term : {cdInfo.term} " +
                $"IRate : {(cdInfo.interestRate * 100).ToFloatN2String()}% MaturityDate : {cdInfo.maturityDate.Year}-{cdInfo.maturityDate.Month}";
        }
    }

    public void OnConfirmButtonClick()
    {
        gameObject.SetActive(false);
    }
}
