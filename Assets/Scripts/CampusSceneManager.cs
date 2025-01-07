using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampusSceneManager : MonoBehaviour
{
    //GameManager gameManager = null;

    [SerializeField] CanvasQuad BeginSimulationRef;
    [SerializeField] EventTrigger MelaninHallTriggerRef;
    [SerializeField] EventTrigger BellesDormTriggerRef;

    //flag that turns true after the practice session.  Used by the "campus scene"
    //to allow the player to control transition to the "campus night scene".
    public static bool bReadyToBeginSimulation = false;
    static CanvasQuad BeginSimulation = null;   //this reference will persist even when we change scenes.
    static EventTrigger BellesDormTrigger = null;
    static EventTrigger MelaninHallTrigger = null;

    private void Awake()
    {
        //subscribe to the scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;

        //We need to maintain static references to objects that need to survive a scene re-load
        if (!BeginSimulation) { BeginSimulation = BeginSimulationRef; }
        if (!BellesDormTrigger) { BellesDormTrigger = BellesDormTriggerRef; }
        if (!MelaninHallTrigger) { MelaninHallTrigger = MelaninHallTriggerRef; }
    }
    /*
    void Start()
    {
        Debug.Log("CampusSceneManager:Start()...");
        FindGameManager();
        if (!gameManager)
        {
            Debug.Log("CampusSceneManager:Start()...Did not find a game manager. ******************");
        }

    }

    void FindGameManager()
    {
        if (gameManager) return;

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("CampusSceneManager:FindGameManager - did not find a GameManager.");
        }
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //put us in front of our room, facing the door
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(new Vector3(0f, 0.1f, 5f), new Vector3(0f, 0f, 0f));
        }
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

        if (BeginSimulation != null)
        {
            Destroy(BeginSimulation.gameObject);    //destroy the canvas quad
        }

        //Debug.Log("CampusSceneManager: Loading scene " + sceneName);
        GameManager.LoadScene(sceneName);       //load the requested scene

        //Debug.Log("CampusSceneManager: Loaded scene " + sceneName);

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


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("CampusSceneManager:OnSceneLoad()...");
        if (bReadyToBeginSimulation && scene.name == "Campus Scene")
        {
            Debug.Log("CampusSceneManager:OnSceneLoad()...returned from Melanin Hall.");
            BeginSimulation.Show(true);
            MelaninHallTrigger.gameObject.SetActive(false);

            //unsubscribe to the scene load event
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        /*
        else
        {
            BeginSimulation.Hide(true);
        }
        */
    }


    //pass-through functions for timelines
    //public void FadeIn(bool instant = false) { FindGameManager(); gameManager.FadeIn(instant); }
    //public void FadeOut(bool instant = false) { FindGameManager(); gameManager.FadeOut(instant); }

    public void FadeIn(bool instant = false) { GameManager.instance.FadeIn(instant); }
    public void FadeOut(bool instant = false) { GameManager.instance.FadeOut(instant); }

}
