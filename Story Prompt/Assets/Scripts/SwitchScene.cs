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

    void Start()
    {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && sceneManager.thingsDone >= sceneManager.thingsNeedToFinish) {
            sceneManager.changeScene(targetScene, transitionTime);
        }
    }

}
