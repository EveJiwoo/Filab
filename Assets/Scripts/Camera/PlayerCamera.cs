using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    static public PlayerCamera Instance;

    FollowCamera mFollow;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;

        mFollow = GetComponent<FollowCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Transform _target)
    {
        mFollow.Reset(_target);
    }
}
