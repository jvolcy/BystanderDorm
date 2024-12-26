using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusSceneManager : MonoBehaviour
{
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("MelaninSceneManager:Start - did not find an GameManager.");
        }
    }

    public void LoadScene(string sceneName)
    {
        if (!gameManager)
        {
            Debug.Log("MelaninSceneManager:LoadScene - did not find an GameManager.");
            return;
        }

        gameManager.LoadScene(sceneName);
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
