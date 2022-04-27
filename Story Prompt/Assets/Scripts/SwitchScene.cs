using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{

    public int targetScene = 1;
    public float transitionTime = 2;
    SceneManagement sceneManager;
    //GameObject mainCamera;

    //bool effecting = false;

    //public float sceneTransitionTime = 1;
    //Color originalColor;
    //float changingAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();

    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            sceneManager.changeScene(targetScene, transitionTime);
        }
    }

}
