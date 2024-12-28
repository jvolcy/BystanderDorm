using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Select the playmode with an enum
    enum PlayMode { Desktop, XR }
    [Header("Play Mode")]
    [SerializeField] PlayMode playMode = PlayMode.Desktop;

    [Space]
    [Header("Player Prefabs")]
    [SerializeField] GameObject DesktopPlayerPrefab;
    [SerializeField] GameObject XRPlayerPrefab;

    //[Space]
    //[Header("Persistent GOs")]
    //[SerializeField] GameObject[] DontDestroyObjList;

    [Space]
    [Header("Scenes")]
    //[SerializeField] string FirstScene = "Campus Scene";

    [SerializeField] Vector3 MelaninToCampusStartPosition = new Vector3(0f, 0f, 7f);
    [SerializeField] Vector3 MelaninToCampusStartRotation = new Vector3(0f, 180f, 0f);


    //the public sceneManager is used by the timelines
    public SceneManager sceneManager;

    GameObject Player;
    XROrigin xrOrigin = null;

    static GameManager instance;

    string CurrentScene = "None";
    string LastScene = "None";

    /* ======================================================================
     * 
     ====================================================================== */
    private void Awake()
    {

        if (instance == null)
        {
            Debug.Log("New GM.");
            instance = this;
        }
        else
        {
            Debug.Log("A GM already exists... self-terminating.");
            Destroy(this);
            return;
        }

        Debug.Log("Enable/Disable XR/DESKTOP objects...");
        EnableDisableXrAndDesktopObjs();

        /* Per https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html:
         * For active GameObjects placed in a Scene, Unity calls Awake after all
         * active GameObjects in the Scene are initialized, so you can safely 
         * use methods such as GameObject.FindWithTag to query other GameObjects.
         */
        //configure our CanvasQuad objects
        CanvasQuad[] quads = FindObjectsByType<CanvasQuad>(FindObjectsSortMode.None);
        foreach (var quad in quads)
        {
            if (playMode == PlayMode.Desktop)
            { quad.playMode = CanvasQuad.PlayMode.Desktop; }
            else
            { quad.playMode = CanvasQuad.PlayMode.XR; }
        }

        //instantiate the player

        if (playMode == PlayMode.Desktop)
        { Player = Instantiate(DesktopPlayerPrefab); }
        else
        {
            Player = Instantiate(XRPlayerPrefab);
            xrOrigin = Player.GetComponentInChildren<XROrigin>();
            if (xrOrigin == null)
            {
                Debug.Log("GameManager: Could not find the XROrigin. ***************************");
            }
        }
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(this);    //GameManager

        //subscribe to the scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;

    }


    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {




        //find the Player object.  There should be one under XR
        //and one under DESKTOP, but only one of these will be active.
        //Player = GameObject.FindGameObjectWithTag("Player");

    }


    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X");
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            //load the next scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            //load the prev scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }


    /* ======================================================================
     * 
     ====================================================================== */
    public void EnableDisableXrAndDesktopObjs()
    {
        GameObject[] XR;
        GameObject[] DESKTOP;

        XR = GameObject.FindGameObjectsWithTag("XR");
        DESKTOP = GameObject.FindGameObjectsWithTag("DESKTOP");

        //Enable/Disable XR/DESKTOP player and other GOs
        if (playMode == PlayMode.Desktop)
        {
            foreach (var xr in XR) { xr.SetActive(false); }
            foreach (var desktop in DESKTOP) { desktop.SetActive(true); }
        }
        else
        {
            foreach (var xr in XR) { xr.SetActive(true); }
            foreach (var desktop in DESKTOP) { desktop.SetActive(false); }
        }
    }



    /* ======================================================================
     * 
     ====================================================================== */
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("GM: Loaded scene " + CurrentScene + "(from " + LastScene + ").");

        LastScene = CurrentScene;
        CurrentScene = scene.name;

        //Look for XR and DESKTOP labels and enable/disable accordingly
        EnableDisableXrAndDesktopObjs();


        //Position the player in the scene
        if (LastScene == "MelaninHall" && CurrentScene == "Campus Scene")
        {
            Debug.Log("Relocating Player *********");
            Player.GetComponent<PlayerCtrl>().SetPosition(MelaninToCampusStartPosition, MelaninToCampusStartRotation);
        }
        else
        {
            Transform playerStartPosition = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform;
            if (playerStartPosition)
            {
                Debug.Log("GM: Found a 'PlayerStartPosition marker' Relocating player...");
                Player.GetComponent<PlayerCtrl>().SetPosition(playerStartPosition.position, playerStartPosition.localEulerAngles);
            }
        }
    }
    

    /* ======================================================================
     * 
     ====================================================================== */
    /*
    public void LoadNextScene()
    {
        //load the next scene
        Debug.Log("Load Next Scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */

    /* ======================================================================
     * 
     ====================================================================== */
    public void LoadScene(string sceneName)
    {
        //load the next scene
        Scene scene;

        scene = SceneManager.LoadScene(sceneName, new LoadSceneParameters(LoadSceneMode.Single));

        if (scene == null)
        {
            Debug.Log("GM: Failed to Load scene " + sceneName + ".");
            return;
        }
        /*
        LastScene = CurrentScene;
        CurrentScene = sceneName;
        Debug.Log("GM: Loaded scene " + CurrentScene + " (from " + LastScene + ").");

        if (LastScene == "MelaninHall" && CurrentScene == "Campus Scene")
        {
            Debug.Log("Relocating Player *********");
            Player.GetComponent<PlayerCtrl>().SetPosition(MelaninToCampusStartPosition, MelaninToCampusStartRotation);
        }
        */
    }


    public void UsePlayerHandModels(bool val)
    {
        if (playMode == PlayMode.XR)
        {
            if (!xrOrigin)
            {
                Debug.Log("ERROR GameManager:UsePlayerHandModels() - No XROrigin.");
                return;
            }
            xrOrigin.GetComponent<PlayerCtrl>().UseHandControls(val);
        }
    }

    public void SetHandColor(string clrStr)
    {
        Color clr;
        if (ColorUtility.TryParseHtmlString(clrStr, out clr))
        {
            SetHandColor(clr);
        }
        else
        {
            Debug.Log("GameManager: Failed to parse color sting " + clrStr);
        }
    }

    public void SetHandColor(Color clr)
    {
        if (playMode == PlayMode.XR)
        {
            if (!xrOrigin)
            {
                Debug.Log("ERROR GameManager:SetHandColor() - No XROrigin.");
                return;
            }
            xrOrigin.GetComponent<PlayerCtrl>().SetHandColor(clr);
        }
    }



}

/* ======================================================================
 * 
 * ====================================================================== */