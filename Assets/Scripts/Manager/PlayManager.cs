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

    [Header("최초의 게임 시작 시간 : 년도")]
    public int kStartYear;
    [Header("최초의 게임 시작 시간 : 월")]
    public int kStartMonth;
    [Header("최초의 게임 시작 시간 : 일")]
    public int kStartDay;
    [Header("최초의 게임 시작 시간 : 시")]
    public int kStartHour;
    [Header("최초의 게임 시작 시간 : 분")]
    public int kStartMin;

    [Header("게임 시간 가속값")]
    public float kTimeSpeed = 5000f;

    DateTime mCurDateTime;
    int mCurYear = 0;
    int mCurMonth = 0;

    Player mPlayer;
    public Player player { get { return mPlayer; } }

    /// <summary> 시간 흐름 작동 / 멈춤 </summary>
    public bool isTimer { get; set; }

    Map mMap;
    public Map map { get { return mMap; } }

    private void Awake()
    {
        Instance = this;

        mPlayer = GameObject.FindObjectOfType<Player>();
        var map = GameObject.FindObjectOfType<Map>();        

        //최초 맵 로드
        LoadMap(map, map.kResetPosition.position);
        Mng.play.player.isPortalTransit = false;
        Mng.sound.PlayBgm(map.kPlayBGM);

        //저장된 시간 복구
        var time = PlayerPrefs.GetString(ConstDef.GAME_DATE_TIME);
        if (time == "")
            mCurDateTime = new DateTime(kStartYear, kStartMonth, kStartDay, kStartHour, kStartMin, 0);
        else
            mCurDateTime = DateTime.Parse(time);

        //시간 갱신
        TimeUpdate(mCurDateTime);

        isTimer = true;
    }

    void Start()
    {
        
    }

    public void LoadMap(Map _map, Vector3 _pos)
    {
        Mng.play.player.isPortalTransit = true;

        mPlayer.transform.position = _pos;
        PlayerCamera.Instance.SetPosition(mPlayer.transform);        

        if (mMap != null)
            Destroy(mMap.gameObject);

        mMap = _map;
    }

    private void Update()
    {
        if( isTimer == true ){
            mCurDateTime = mCurDateTime.AddSeconds(Time.deltaTime * kTimeSpeed);
            TimeUpdate(mCurDateTime);
        }
    }

    void TimeUpdate(DateTime _time)
    {
        Mng.canvas.kTownMenu.SetDateTime(_time);

        if( mCurMonth != mCurDateTime.Month){
            mCurMonth = mCurDateTime.Month;
            float rate = Mng.table.GetBaseInterestRate(_time);
            Mng.canvas.kTownMenu.TempRate(rate);
        }
    }

    private void OnApplicationQuit()
    {
        //현재까지의 시간 저장
        PlayerPrefs.SetString(ConstDef.GAME_DATE_TIME, mCurDateTime.ToString());
    }
}