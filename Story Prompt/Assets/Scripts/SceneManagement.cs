using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    int currentSceneNumber;

    private void Start()
    {
        currentSceneNumber = SceneManager.GetActiveScene().buildIndex;  
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
}
