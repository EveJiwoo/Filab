using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PortalScripTable : ScriptableObject {
    [Header("읽어드릴 맵 오브젝트")]
    public Map loadMap;
    [Header("맵의 도착 위치 경로")]
    public string portalName;    
}

