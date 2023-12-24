using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PortalTransition : MonoBehaviour
{
    [Header("포탈 정보")]
    public PortalScripTable kPortal;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != ConstDef.LAYER_PLAYER)
            return;

        if (Mng.play.player.isPortalTransit == true)
            return;        

        Map map = Instantiate<Map>(kPortal.loadMap);
        Transform portalTf = map.transform.Find(kPortal.portalName);
        
        Mng.data.myInfo.local = map.kCityType;

        Mng.play.LoadMap(map, portalTf.position);

        Mng.sound.PlayBgm(map.kPlayBGM);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != ConstDef.LAYER_PLAYER)
            return;

        Mng.play.player.isPortalTransit = false;
    }
}
