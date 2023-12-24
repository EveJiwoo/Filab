using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static public Manager Instance;

    //[Header("풀 매니저")]
    //public PoolManager kPoolManager;
    [Header("테이블 매니저")]
    public TableManager kTableManager;
    [Header("데이터 매니저")]
    public DataManager kDataManager;    
    [Header("플레이 매니저")]
    public PlayManager kPlayManager;
    [Header("사운드 매니저")]
    public SoundManager kSoundManager;
    [Header("캔버스 매니저")]
    public MainCanvas kCanvasManager;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        ///////////////////////////////////////////////////////////////////////////////////////
        //매니저 초기화

        GameObject go = Instantiate(kTableManager.gameObject);
        go.transform.parent = transform;
        go.name = "TableManager";

        while (TableManager.Instance == null)
            yield return null;

        go = Instantiate(kDataManager.gameObject);
        go.transform.parent = transform;
        go.name = "DataManager";

        while (DataManager.Instance == null)
            yield return null;

        go = Instantiate(kSoundManager.gameObject);
        go.transform.parent = transform;
        go.name = "SoundManager";

        while (SoundManager.Instance == null)
            yield return null;
/*
        go = Instantiate(kPoolManager.gameObject);
        go.transform.parent = transform;
        go.name = "PoolManager";

        while (PoolManager.Instance == null)
            yield return null;
*/
        kCanvasManager.kTownMenu.gameObject.SetActive(true);
  
        go = Instantiate(kPlayManager.gameObject);
        go.transform.parent = transform;
        go.name = "PlayManager";

        while (PlayManager.Instance == null)
            yield return null;

        ///////////////////////////////////////////////////////////////////////////////////////
        //게임 시작

    }
}

//게임 종료 후 정기 예금 돌려 받나?
