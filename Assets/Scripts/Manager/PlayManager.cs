using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PlayManager : MonoBehaviour
{
    static public PlayManager Instance = null;    

    [Header("게임 시간 가속값")]
    public float kTimeSpeed = 5000f;    

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

        //시간 갱신
        TimeUpdate(Mng.data.curDateTime);

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
            Mng.data.curDateTime = Mng.data.curDateTime.AddSeconds(Time.deltaTime * kTimeSpeed);
            TimeUpdate(Mng.data.curDateTime);
        }
    }

    void TimeUpdate(DateTime _time)
    {
        Mng.canvas.kTownMenu.SetDateTime(_time);

        if( mCurMonth != Mng.data.curDateTime.Month){
            mCurMonth = Mng.data.curDateTime.Month;
            float rate = Mng.table.GetBaseInterestRate(_time);
            Mng.canvas.kTownMenu.TempRate(rate);
        }
    }
}