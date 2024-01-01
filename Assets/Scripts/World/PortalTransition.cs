using EnumDef;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PortalTransition : MonoBehaviour
{
    public LoadMap kLoadMap = LoadMap.None;
    public MapPoint kPoint = MapPoint.None;

    private void Awake()
    {
        var col = GetComponentInChildren<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != ConstDef.LAYER_PLAYER)
            return;

        if (Mng.play.player.isPortalTransit == true)
            return;
                
        Map map = kLoadMap.LoadResource();

        PortalTransition[] portals = map.transform.GetComponentsInChildren<PortalTransition>();

        foreach (var portal in portals)
        {
            if (portal.kPoint == kPoint){

                if (kLoadMap == LoadMap.Home || kLoadMap == LoadMap.Bank || kLoadMap == LoadMap.Shop)
                    portal.kLoadMap = Mng.data.myInfo.local.ToLoadMap();
                
                if( IsCity(kLoadMap) == true)
                    Mng.data.myInfo.local = map.kCityType;

                Mng.play.LoadMap(map, portal.transform.position);
                break;
            }
        }

        Mng.sound.PlayBgm(map.kPlayBGM);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer != ConstDef.LAYER_PLAYER)
            return;

        Mng.play.player.isPortalTransit = false;
    }

    bool IsCity(LoadMap _map)
    {
        if (LoadMap.City1 <= _map && _map <= LoadMap.City10)
            return true;

        return false;
    }
}
