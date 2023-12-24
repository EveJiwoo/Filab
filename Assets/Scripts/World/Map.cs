using EnumDef;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("맵 초기화 시작점")]
    public Transform kResetPosition;
    [Header("맵 테마 음악 오디오클립")]
    public AudioClip kPlayBGM;
    [Header("맵 소속")]
    public CityType kCityType = CityType.None;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
