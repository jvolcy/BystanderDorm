using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusSceneManager : MonoBehaviour
{
    GameManager gameManager;

    /*
    void Start()
    {
        FindGameManager();
    }
    */

    public void LoadScene(string sceneName)
    {
        FindGameManager();

        if (!gameManager)
        {
            Debug.Log("CampusSceneManager:LoadScene - did not find an GameManager.");
            Debug.Log("Could not load scene " + sceneName);
            return;
        }

        gameManager.LoadScene(sceneName);
    }

    void FindGameManager()
    {
        if (!gameManager) { gameManager = FindObjectOfType<GameManager>(); }

        if (!gameManager)
        {
            Debug.Log("CampusSceneManager:FindGameManager - did not find a GameManager.");
        }
    }
}
