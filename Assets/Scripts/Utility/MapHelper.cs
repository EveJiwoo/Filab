using EnumDef;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class MapHelper
{
    static public string ToResourcePath(this LoadMap _value)
    {        
        switch (_value)
        {
            case LoadMap.City1: return "Prefabs/Map/City1";
            case LoadMap.City2: return "Prefabs/Map/City2";
            case LoadMap.City3: return "Prefabs/Map/City3";
            case LoadMap.City4: return "Prefabs/Map/City4";
            case LoadMap.City5: return "Prefabs/Map/City5";
            case LoadMap.City6: return "Prefabs/Map/City6";
            case LoadMap.City7: return "Prefabs/Map/City7";
            case LoadMap.City8: return "Prefabs/Map/City8";
            case LoadMap.City9: return "Prefabs/Map/City9";
            case LoadMap.City10: return "Prefabs/Map/City10";

            case LoadMap.Bank: return "Prefabs/Map/Bank";
            case LoadMap.Shop: return "Prefabs/Map/Shop";
            case LoadMap.Home: return "Prefabs/Map/Home";

            case LoadMap.World: return "Prefabs/Map/World";
        }

        return "";
    }

    static public Map LoadResource(this LoadMap _value)
    {
        string mapName = _value.ToResourcePath();
        var mapGo = Resources.Load(mapName) as GameObject;
        Map map = GameObject.Instantiate(mapGo).GetComponent<Map>();

        return map;
    }

    static public LoadMap ToLoadMap(this CityType _value)
    {
        LoadMap map = LoadMap.None;
        switch (_value)
        {
            case CityType.City1: map = LoadMap.City1; break;
            case CityType.City2: map = LoadMap.City2; break;
            case CityType.City3: map = LoadMap.City3; break;
            case CityType.City4: map = LoadMap.City4; break;
            case CityType.City5: map = LoadMap.City5; break;
            case CityType.City6: map = LoadMap.City6; break;
            case CityType.City7: map = LoadMap.City7; break;
            case CityType.City8: map = LoadMap.City8; break;
            case CityType.City9: map = LoadMap.City9; break;
            case CityType.City10: map = LoadMap.City10; break;
        }

        return map;
    }


}
