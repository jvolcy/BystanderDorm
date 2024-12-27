using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelaninSceneManager : MonoBehaviour
{
    GameManager gameManager = null;

    // Start is called before the first frame update
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
            Debug.Log("MelaninSceneManager:LoadScene - did not find an GameManager.");
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
            Debug.Log("MelaninSceneManager:FindGameManager - did not find a GameManager.");
        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
