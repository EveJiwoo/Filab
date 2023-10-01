using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using EnumDef;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayManager : MonoBehaviour
{
    static public PlayManager Instance = null;

    Player mPlayer;

    Map mMap;
    public Map map { get { return mMap; } }

    private void Awake()
    {
        Instance = this;

        mPlayer = GameObject.FindObjectOfType<Player>();
        var map = GameObject.FindObjectOfType<Map>();
        mPlayer.transform.position = map.kStartPosition.position;

        LoadMap(map);
    }

    void Start()
    {
        
    }

    public void LoadMap(Map _map)
    {
        
        PlayerCamera.Instance.SetPosition(mPlayer.transform);

        if(mMap != null)
            Destroy(mMap.gameObject);

        mMap = _map;        
    }
}