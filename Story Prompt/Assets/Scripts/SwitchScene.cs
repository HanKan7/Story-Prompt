using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwitchScene : MonoBehaviour
{

    public int targetScene = 1;
    GameManager gameManeger;
    //GameObject mainCamera;

    //bool effecting = false;

    //public float sceneTransitionTime = 1;
    //Color originalColor;
    //float changingAlpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameManeger = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //originalColor = gameManeger.screen.transform.GetChild(0).GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {/*
        if (effecting) {
            mainCamera.GetComponent<RipplePostProcessor>().transitionRippleEffect();
            changingAlpha += 0.5f * Time.deltaTime;
            Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, changingAlpha);
            gameManeger.screen.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            /*SceneManager.LoadScene(targetScene);
            gameManeger.GetComponent<GameManager>().enemySpawning = true;*/
            //mainCamera.GetComponent<RipplePostProcessor>().RippleEffect();
            //StartCoroutine(transScene());
            gameManeger.changeScene(targetScene);
        }
    }

}
