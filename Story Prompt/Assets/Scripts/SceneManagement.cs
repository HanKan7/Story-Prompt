using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    public int currentSceneNumber;
    public float changingAlpha = 1;
    //bool quiteEffecting = false;
    //bool enterEffecting = false;
    GameManager gameManager;
    //public GameObject screen;
    //public GameObject mainCamera;
    public float blackingSpeed = 0.5f;
    public bool talkedWithBossThisScene = false;
    public int thingsNeedToFinish = 2;
    public int thingsDone = 0;
    //Sprite Gate;
    public Sprite openGate;
    public bool fightThisScene = false;
    public GameObject[] enemyPaths;

    //Audio
    [SerializeField] AudioSource openGateSound;

    private void Start()
    {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
        //Gate = GameObject.Find("Gate").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
        if (fightThisScene) {
            gameManager.enemySpawning = true;
            gameManager.paths = enemyPaths;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            //changeScene(-1, 2);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if(currentSceneNumber == 1 || currentSceneNumber == 4 ||currentSceneNumber == 6)
            {
                ReplayScene();
            }


        }

        if (thingsDone >= thingsNeedToFinish) 
        {
            if (openGate != null && GameObject.Find("Gate").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite != openGate) { 
                GameObject.Find("Gate").transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = openGate;
                openGateSound.Play();
                gameManager.sceneManager = this;
            } 
        }



    }

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
        //mainCamera.GetComponent<RipplePostProcessor>().transitionRippleEffect(changingAlpha * (1 / blackingSpeed));
        /*if (mainCamera != null)
        {

            // Debug.Log("MainCamera Found!");
            //mainCamera.GetComponent<RipplePostProcessor>().transitionRippleEffect(changingAlpha * (1 / blackingSpeed));
        }
        else
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }*/
        //////////////////////////////////////////////////////////////////
        if (gameManager.quiteEffecting && changingAlpha < 254) //Entering scene
        {
            changingAlpha += blackingSpeed * Time.deltaTime; // 0 to 255

            Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, changingAlpha);
            screen.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        }
        else if (gameManager.enterEffecting && changingAlpha > 1) //Exiting the scene
        {
            changingAlpha -= blackingSpeed * Time.deltaTime;
            Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, changingAlpha);
            screen.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        }

    }

    public void changeScene(int targetScene, float sceneTransitionTime)
    {
        //StartCoroutine(transScene(targetScene, sceneTransitionTime));
        GoToScene(targetScene);
    }




    IEnumerator transScene(int targetScene, float sceneTransitionTime)
    {
        gameManager.quiteEffecting = true; //exit animation
        yield return new WaitForSeconds(0);
        gameManager.quiteEffecting = false;
        //SceneManager.LoadScene(targetScene);
        if (targetScene < 0)
        {
            ReplayScene();
        }
        else 
        {
            GoToScene(targetScene);
        }
        gameManager.enterEffecting = true;//enterAnimation
        yield return new WaitForSeconds(0);
        gameManager.enterEffecting = false;
        //gameManager.talkedWithBossThisScene = false;
        
    }
}
