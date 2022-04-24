using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyPaths[] paths;
    public GameObject enemyPrefab;

    public int numberOfEnemies;
    public float spawnEnemyTime = 2;
    float spawnTimeCount = 0;

    public bool enemySpawning = false;
    public bool dialoguing = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (enemySpawning && ! dialoguing) {
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
                Vector3 spawnPoint = paths[i].enemyPath.transform.GetChild(0).transform.position;
                GameObject tempEnemyPrefab = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
                EnemyMovement tempEnemy = tempEnemyPrefab.GetComponent<EnemyMovement>();
                tempEnemy.path = paths[i].enemyPath;
                tempEnemy.targetBuilding = paths[i].targetBuilding;
            }
            spawnTimeCount = 0;
        }
    }


}

[System.Serializable]
public class EnemyPaths {
    public GameObject enemyPath;
    public GameObject targetBuilding;
}
