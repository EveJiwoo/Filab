using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneTransition : MonoBehaviour
{

    public int sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public Animator animator;
    

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") 
        {
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
