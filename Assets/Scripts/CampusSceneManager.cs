using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampusSceneManager : MonoBehaviour
{
    //GameManager gameManager;
    [SerializeField] CanvasQuad BeginSimulation;
    [SerializeField] CanvasQuad ScreenFade;
    [SerializeField] EventTrigger MelaninHallTrigger;
    [SerializeField] EventTrigger BellesDormTrigger;

    //flag that turns true after the practice session.  Used by the "campus scene"
    //to allow the player to control transition to the "campus night scene".
    public bool bReadyToBeginSimulation = false;

    private void Awake()
    {
        //subscribe to the scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {

        Debug.Log("CampusSceneManager: Start()...");
        ScreenFade.FadeOut();
    }
    


    public void LoadScene(string sceneName)
    {
        /*
        FindGameManager();

        if (!gameManager)
        {
            Debug.Log("CampusSceneManager:LoadScene - did not find an GameManager.");
            Debug.Log("Could not load scene " + sceneName);
            return;
        }
        */
        bReadyToBeginSimulation = true;         //signal time to display the "begin simulation" button if we return to this scene
        Destroy(BeginSimulation.gameObject);    //destroy the canvas quad
        GameManager.LoadScene(sceneName);       //load the requested scene
    }

    /*
    void FindGameManager()
    {
        if (!gameManager)
        {
            gameManager = FindObjectOfType<GameManager>();

            if (!gameManager)
            {
                Debug.Log("CampusSceneManager:FindGameManager - did not find a GameManager.");
            }
        }
    }
    */

    private void OnEnable()
    {
        ScreenFade.FadeOut();

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (bReadyToBeginSimulation && scene.name == "Campus Scene")
        {
            BeginSimulation.Show(true);
            MelaninHallTrigger.enabled = false;

            //unsubscribe to the scene load event
            SceneManager.sceneLoaded -= OnSceneLoaded;


        }
    }

}
