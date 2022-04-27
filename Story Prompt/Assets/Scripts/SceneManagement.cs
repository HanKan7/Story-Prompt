using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    int currentSceneNumber;
    public float changingAlpha = 0;
    //bool quiteEffecting = false;
    //bool enterEffecting = false;
    GameManager gameManager;
    //public GameObject screen;
    //public GameObject mainCamera;
    public float blackingSpeed = 0.5f;
    public bool talkedWithBossThisScene = false;
    private void Start()
    {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }

    /*private void Update()
    {
        if (quiteEffecting || enterEffecting) {
            transitionEffect(screen, mainCamera);
        }


    }*/

    public void GoToScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(currentSceneNumber + 1);
    }

    public void ReplayScene()
    {
        SceneManager.LoadScene(currentSceneNumber);
    }


    public void transitionEffect(GameObject screen, GameObject mainCamera)
    {
        Color originalColor = screen.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        mainCamera.GetComponent<RipplePostProcessor>().transitionRippleEffect(changingAlpha * (1 / blackingSpeed));
        if (mainCamera != null)
        {

            // Debug.Log("MainCamera Found!");
            //mainCamera.GetComponent<RipplePostProcessor>().transitionRippleEffect(changingAlpha * (1 / blackingSpeed));
        }
        else
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        //////////////////////////////////////////////////////////////////
        if (gameManager.quiteEffecting && changingAlpha < 255) //Entering scene
        {
            changingAlpha += blackingSpeed * Time.deltaTime; // 0 to 255
            
            Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, changingAlpha);
            screen.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        }
        else if (gameManager.enterEffecting && changingAlpha > 0) //Exiting the scene
        {
            changingAlpha -= blackingSpeed * Time.deltaTime;
            Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, changingAlpha);
            screen.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        }

    }

    public void changeScene(int targetScene, float sceneTransitionTime)
    {
        StartCoroutine(transScene(targetScene, sceneTransitionTime));

    }

    IEnumerator transScene(int targetScene, float sceneTransitionTime)
    {
        gameManager.quiteEffecting = true; //exit animation
        yield return new WaitForSeconds(sceneTransitionTime);
        gameManager.quiteEffecting = false;
        //SceneManager.LoadScene(targetScene);
        GoToScene(targetScene);
        gameManager.enterEffecting = true;//enterAnimation
        yield return new WaitForSeconds(sceneTransitionTime);
        gameManager.enterEffecting = false;
        //gameManager.talkedWithBossThisScene = false;
        
    }
}
