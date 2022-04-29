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
    public int enemyDestroyed = 0;
    public int buildingDestroyed = 0;
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
    public SceneManagement sceneManager;
    public bool badEnding = true;
    bool enemyOut = false;

    static GameManager gMInstance;
    //static GameObject screenInstance;

    //Audio Elements
    public AudioSource mainBGM;
    public AudioSource fight;
    public AudioSource bossConfrontation;
    public AudioSource realisation;


    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        originalColor = screen.transform.GetChild(0).GetComponent<Image>().color;
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();
        mainBGM.Play();

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
    {/*
        if ((quiteEffecting || enterEffecting) && (changingAlpha >= 255 || changingAlpha <= 0)) {
            quiteEffecting = false;
            enterEffecting = false;
        }*/

        if (mainCamera == null)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        if(sceneManager == null)
        {
            sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();
        }
        /*if (quiteEffecting || enterEffecting)
        {
            sceneManager.transitionEffect(screen, mainCamera);
        }*/

        if (enemySpawning)
        {
            paths = GameObject.FindGameObjectsWithTag("EnemyPath");
        }

        if (enemySpawning && !dialoguing && enemyCount < numberOfEnemies)
        {
            SpawnEnemy();
        }
        else if (enemySpawning && enemyCount >= numberOfEnemies)
        {
            if (buildingDestroyed >= 3 || enemyDestroyed >= 15)
            {
                if (!enemyOut) {
                    enemyOut = true;
                    findSceneManager();
                    sceneManager.thingsDone += 1;
                }

            }
            if (buildingDestroyed >= 3)
            {
                GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < remainingEnemies.Length; i++)
                {
                    remainingEnemies[i].GetComponent<CapsuleCollider2D>().enabled = false;
                    remainingEnemies[i].GetComponent<EnemyMovement>().returnToGate = true;
                    badEnding = false;
                }
            }

        }
    }

    void SpawnEnemy()
    {
        if (spawnTimeCount < spawnEnemyTime)
        {
            spawnTimeCount += Time.deltaTime;
        }
        else
        {
            for (int i = 0; i < paths.Length; i++)
            {
                EnemyPaths path = paths[i].GetComponent<EnemyPaths>();
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

    public void findSceneManager() {
        sceneManager = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManagement>();
        Debug.Log(sceneManager.name);
    }
}