using EnumDef;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [Header("�� �ʱ�ȭ ������")]
    public Transform kResetPosition;
    [Header("�� �׸� ���� �����Ŭ��")]
    public AudioClip kPlayBGM;
    [Header("�� �Ҽ�")]
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
