using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalTransition : MonoBehaviour
{   
    [Header("���� ����")]
    public BoxCollider2D kEnterArea;

    [Header("���� ��")]
    public Map kEnterMap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Map map = Instantiate<Map>(kEnterMap);
        Mng.play.LoadMap(map);
    }
}
