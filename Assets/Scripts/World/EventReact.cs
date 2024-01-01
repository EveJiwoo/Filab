using EnumDef;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EventReact : MonoBehaviour
{
    public EventReactType kType = EventReactType.None;

    // Start is called before the first frame update
    void Awake()
    {
        var col = GetComponentInChildren<BoxCollider2D>();
        col.isTrigger = true;
    }

    // Update is called once per frameev
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != ConstDef.LAYER_PLAYER)
            return;

        if (kType == EventReactType.None)
            return;


        switch(kType)
        {
            case EventReactType.BankDepositPopup:
                Mng.canvas.kCityBankPopup.gameObject.SetActive(true);
                break;
            case EventReactType.BankCDAccountPopup:
                Mng.canvas.kCityBankCDAccountPopup.gameObject.SetActive(true);
                break;
            case EventReactType.BankLoanPopup:
                Mng.canvas.kCityBankLoanPopup.gameObject.SetActive(true);
                break;
            case EventReactType.ShopPopup:
                Mng.canvas.kTradeShopPopup.gameObject.SetActive(true);
                break;
        }
    }
}
