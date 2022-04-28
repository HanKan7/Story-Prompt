using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] paths;
    public GameObject enemyPrefab;

    public int numberOfEnemies;
    int enemyCount = 0;
    public float spawnEnemyTime = 2;
    float spawnTimeCount = 0;

    public bool enemySpawning = false;
    public bool dialoguing = false;
    //public bool talkedWithBossThisScene = false;
    
    public GameObject screen;
    public GameObject mainCamera;
    public float sceneTransitionTime = 2;
    public float blackingSpeed = 0.5f;
    Color originalColor;
    float changingAlpha = 0;
    public bool quiteEffecting = false;
    public bool enterEffecting = false;
    SceneManagement sceneManager;

    static GameManager gMInstance;
    //static GameObject screenInstance;
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        originalColor = screen.transform.GetChild(0).GetComponent<Image>().color;
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();

    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //DontDestroyOnLoad(screen);
        if (gMInstance == null)
        {
            gMInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    

    // Update is called once per frame
    void Update()
    {
        if (mainCamera == null) {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        if (quiteEffecting || enterEffecting) {
            sceneManager.transitionEffect(screen, mainCamera);
        }

        if (enemySpawning) {
            paths = GameObject.FindGameObjectsWithTag("EnemyPath");
        }

        if (enemySpawning && ! dialoguing && enemyCount < numberOfEnemies) {
            SpawnEnemy();     
        }
    }

    void SpawnEnemy() {
        if (spawnTimeCount < spawnEnemyTime)
        {
            spawnTimeCount += Time.deltaTime;
        }
        else {
            for (int i = 0; i < paths.Length; i++) {
                EnemyPaths path = paths[i].GetComponent<EnemyPaths>();
                /*Vector3 spawnPoint = paths[i].enemyPath.transform.GetChild(0).transform.position;
                GameObject tempEnemyPrefab = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                EnemyMovement tempEnemy = tempEnemyPrefab.GetComponent<EnemyMovement>();
                tempEnemy.path = paths[i].enemyPath;
                tempEnemy.targetBuilding = paths[i].targetBuilding;*/
                Vector3 spawnPoint = path.enemyPath.transform.GetChild(0).transform.position;
                GameObject tempEnemyPrefab = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                EnemyMovement tempEnemy = tempEnemyPrefab.GetComponent<EnemyMovement>();
                tempEnemy.path = path.enemyPath;
                tempEnemy.targetBuilding = path.targetBuilding;
            }
            enemyCount++;
            spawnTimeCount = 0;
        }
    }

    /*
    void transitionEffect() {
        if (mainCamera != null)
        {
            mainCamera.GetComponent<RipplePostProcessor>().transitionRippleEffect(changingAlpha * (1 / blackingSpeed));
        }
        else {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        
        if (quiteEffecting && changingAlpha < 255) //Entering scene
        {
            changingAlpha += blackingSpeed * Time.deltaTime; // 0 to 255
            Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, changingAlpha);
            screen.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        } else if (enterEffecting && changingAlpha > 0) //Exiting the scene
        {
            changingAlpha -= blackingSpeed * Time.deltaTime;
            Color tempColor = new Color(originalColor.r, originalColor.g, originalColor.b, changingAlpha);
            screen.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        }

    }


    public void changeScene(int targetScene) {
        StartCoroutine(transScene(targetScene));
        
    }

    IEnumerator transScene(int targetScene)
    {
        quiteEffecting = true; //exit animation
        yield return new WaitForSeconds(sceneTransitionTime);
        quiteEffecting = false;
        SceneManager.LoadScene(targetScene);
        enterEffecting = true;
        yield return new WaitForSeconds(sceneTransitionTime);
        enterEffecting = false;
        talkedWithBossThisScene = false;
    }

    */

}




/*
[System.Serializable]
public class EnemyPaths {
    public GameObject enemyPath;
    public GameObject targetBuilding;
}*/
