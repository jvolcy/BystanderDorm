using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.XR.Management;   //for EventHandler

public class GameManager : MonoBehaviour
{
    //Select the playmode with an enum
    public enum PlayMode { Desktop, XR }
    [Header("Play Mode")]
    public PlayMode playMode = PlayMode.Desktop;

    [Space]
    [Header("Player Prefabs")]
    [SerializeField] GameObject DesktopPlayerPrefab;
    [SerializeField] GameObject XRPlayerPrefab;

    public static event EventHandler<string> SelectCanvasQuad;
    public static event EventHandler ExitingScene;   //invoked immediately before loading the next scene.

    [HideInInspector]
    public GameObject Player;
    PlayerCtrl playerCtrl;

    XROrigin xrOrigin = null;

    static public GameManager instance;

    string CurrentScene = "None";
    string LastScene = "None";

    public float SceneTransitionDelay = 2.25f;
    string NextScene;

    public static bool visitedMelaninHall = false;     //set to true after we visit Melanin Hall (used by CampusSceneManager)

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
        {
            Player = Instantiate(DesktopPlayerPrefab);
            playerCtrl = Player.GetComponent<PlayerCtrl>();
        }
        else
        {
            Player = Instantiate(XRPlayerPrefab);
            xrOrigin = Player.GetComponentInChildren<XROrigin>();
            if (xrOrigin == null)
            {
                Debug.Log("GameManager: Could not find the XROrigin. ***************************");
                playerCtrl = null;
            }
            else
            {
                playerCtrl = xrOrigin.GetComponent<PlayerCtrl>();
            }
        }
        DontDestroyOnLoad(Player);
        DontDestroyOnLoad(this);    //GameManager


        //subscribe to the scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void dummy()
    {
        Debug.Log("XXXXXXXXXXXXXXXXXXXXXX dummy XXXXXXXXXXXXXXXXXXXXXX");
        GameObject obj = new();
        obj.transform.position = new Vector3(1.6299999952316285f, 0.0f, 4.0f);
        obj.transform.rotation = new Quaternion(-0.012117132544517517f, 0.1727437525987625f, 0.002125272061675787f, 0.9848899841308594f);
        GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(obj.transform);
    }


    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {
        //dummy();

        //Debug.Log("GM: Start()...");
        FadeIn();
    }


    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            Transform playerStartPosition = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform;
            if (playerStartPosition)
            {

                Debug.Log("GM: Found a 'PlayerStartPosition marker' Relocating player...");
                playerCtrl.TelePort(playerStartPosition);
            }
        }


        /*
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
        */
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
            //playerCtrl.TelePort(MelaninToCampusPlayerPosition);
        }
        else
        {
            Transform playerStartPosition = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform;
            if (playerStartPosition)
            {

                Debug.Log("GM: Found a 'PlayerStartPosition marker' Relocating player...");
                playerCtrl.TelePort(playerStartPosition);
            }
        }

        //unselect all quads after loading
        //CanvasQuadSelect("");
        Debug.Log("GameManager:OnSceneLoaded()...Fading In...");
        FadeIn();
    }


    public static void LoadScene(string sceneName)
    {
        instance._LoadScene(sceneName);
    }

    /* ======================================================================
     * 
     ====================================================================== */
    void _LoadScene(string sceneName)
    {
        //signal that we are exiting the current scene.  If a GameManager
        //object has not been instantiated, the delay parameter is ignored
        //and the scene loads immediately.
        ExitingScene?.Invoke(null, null);

        //if a GameManager object has been instantiated, the static
        //'instance' reference will not be null.  We can use this to
        //delay the loading of the next scene by the specified amount of time.
        //This gives subscribers to the ExitingScene time to clean up.  For
        //example, the player begins the camera fade-out animation on this event.
        NextScene = sceneName;
        if (instance)
        {
            Invoke("ExeLoadScene", SceneTransitionDelay);
        }
        else
        {
            ExeLoadScene();
        }
    }

    void ExeLoadScene()
    {
        //load the next scene
        Scene scene;

        scene = SceneManager.LoadScene(NextScene, new LoadSceneParameters(LoadSceneMode.Single));

        if (scene == null)
        {
            Debug.Log("GM: Failed to Load scene " + NextScene + ".");
            return;
        }
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
            playerCtrl.UseHandControls(val);
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
            playerCtrl.SetHandColor(clr);
        }
    }



    /// <summary>
    /// Helper function to fade out (fade to black) the display.
    /// Note that we have to fade in the primary quad to create a
    /// black-out of the display.
    /// </summary>
    
    public void FadeOut(bool instant = false)
    {
        playerCtrl.FadeOut(instant);
    }
    

    /// <summary>
    /// Helper function to fade in (fade from black) the display.
    /// Note that we have to fade out the primar quad to create a
    /// fade-in of the display.
    /// </summary>
    public void FadeIn(bool instant = false)
    {
        playerCtrl.FadeIn(instant);
    }

    /// <summary>
    /// Invoke the OnSelectCanvasQuad event.  All quads subscribers
    /// will receive the event.  The one whose id matches the parameter
    /// will "Show" itself.  All others will "Hide".  Set the id to a
    /// string that matches none of the ids to force all quads to hide.
    /// </summary>
    /// <param name="id"></param>
    public static void CanvasQuadSelect(string id)
    {
        SelectCanvasQuad?.Invoke(null, id);
    }
}

/* ======================================================================
 * 
 * ====================================================================== */