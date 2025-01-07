using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelaninSceneManager : MonoBehaviour
{
    //public GameManager gameManager = null;

    // Start is called before the first frame update

    /*
    void Start()
    {
        Debug.Log("MelaninSceneManager:Start()...");
        FindGameManager();
        if (!gameManager)
        {
            Debug.Log("MelaninSceneManager:Start()...Did not find a game manager. ******************");
        }
    }
  */

    public void LoadScene(string sceneName)
    {
        //FindGameManager();
        /*
        if (!gameManager)
        {
            Debug.Log("MelaninSceneManager:LoadScene - did not find an GameManager.");
            Debug.Log("Could not load scene " + sceneName);
            return;
        }
        */
        GameManager.LoadScene(sceneName);
    }
    /*
    void FindGameManager()
    {
        if (gameManager) return;

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("MelaninSceneManager:FindGameManager - did not find a GameManager.");
        }
    }
    */

    //pass-through functions for timelines
    public void FadeIn(bool instant=false) { GameManager.instance.FadeIn(instant); }
    public void FadeOut(bool instant = false) { GameManager.instance.FadeOut(instant); }


    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
