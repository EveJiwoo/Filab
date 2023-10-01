using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    [Header("목표 설정")]
    public Transform kTarget;

    [Header("목표 따라가는 강도")]
    public float kTension = 1.75f;

    Vector3 mToTargetDist;
    float mOldHeight;

    // Use this for initialization
    void Start () {
		
	}
	
    public void Reset(Transform _target)
    {
        kTarget = _target;
                
        Vector3 pos = kTarget.position + Vector3.back * 4f;        
        transform.position = pos;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (kTarget == null)
            return;

        Vector3 pos = kTarget.position + Vector3.back * 4f;        
         
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * kTension);
        mOldHeight = kTarget.position.y;
    }    
}
