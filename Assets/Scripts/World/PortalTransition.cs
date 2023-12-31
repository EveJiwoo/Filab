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
        var cols = GetComponentsInChildren<BoxCollider2D>();
        foreach (var col in cols)
            col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != ConstDef.LAYER_PLAYER)
            return;

        if (Mng.play.player.isPortalTransit == true)
            return;

        string mapName = "";
        switch (kLoadMap)
        {
            case LoadMap.City1: mapName = "Prefabs/Map/City1"; break;
            case LoadMap.City2: mapName = "Prefabs/Map/City2"; break;
            case LoadMap.City3: mapName = "Prefabs/Map/City3"; break;
            case LoadMap.City4: mapName = "Prefabs/Map/City4"; break;
            case LoadMap.City5: mapName = "Prefabs/Map/City5"; break;
            case LoadMap.City6: mapName = "Prefabs/Map/City6"; break;
            case LoadMap.City7: mapName = "Prefabs/Map/City7"; break;
            case LoadMap.City8: mapName = "Prefabs/Map/City8"; break;
            case LoadMap.City9: mapName = "Prefabs/Map/City9"; break;
            case LoadMap.City10: mapName = "Prefabs/Map/City10"; break;

            case LoadMap.Bank: mapName = "Prefabs/Map/Bank"; break;
            case LoadMap.Shop: mapName = "Prefabs/Map/Shop"; break;
            case LoadMap.Home: mapName = "Prefabs/Map/Home"; break;

            case LoadMap.World: mapName = "Prefabs/Map/World"; break;
        }

        var mapGo = Resources.Load(mapName) as GameObject;
        Map map = Instantiate(mapGo).GetComponent<Map>();

        PortalTransition[] portals = map.transform.GetComponentsInChildren<PortalTransition>();

        foreach (var portal in portals)
        {
            if (portal.kPoint == kPoint){

                if (kLoadMap == LoadMap.Home || kLoadMap == LoadMap.Bank || kLoadMap == LoadMap.Shop){

                    switch (Mng.data.myInfo.local){
                        case CityType.City1: portal.kLoadMap = LoadMap.City1; break;
                        case CityType.City2: portal.kLoadMap = LoadMap.City2; break;
                        case CityType.City3: portal.kLoadMap = LoadMap.City3; break;
                        case CityType.City4: portal.kLoadMap = LoadMap.City4; break;
                        case CityType.City5: portal.kLoadMap = LoadMap.City5; break;
                        case CityType.City6: portal.kLoadMap = LoadMap.City6; break;
                        case CityType.City7: portal.kLoadMap = LoadMap.City7; break;
                        case CityType.City8: portal.kLoadMap = LoadMap.City8; break;
                        case CityType.City9: portal.kLoadMap = LoadMap.City9; break;
                        case CityType.City10: portal.kLoadMap = LoadMap.City10; break;
                    }
                }

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
}
