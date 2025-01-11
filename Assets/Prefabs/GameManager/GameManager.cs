using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.XR.Management;   //for EventHandler

/// This is the Game Manager script. The project is organized
/// such that there is a singleton GameManager that survives from
/// scene to scene.  Each scene has a scene manager that exists only in
/// the scene.  The GM is a singleton with a few static members to support
/// the scene managers and manage the overall game.  While a GM object
/// exists in every scene for debugging purposes, the expected mode of
/// operation is that the GM is instantiated in the start scene and
/// survives all subsequent scene changes (do not destroy on load).  When
/// any scene other than the 'Start' scene is loaded, its GM will presumably
/// self-destruct pursuant to the singleton game logic as the GM from the
/// previous scene will already exist.
public class GameManager : MonoBehaviour
{
    //Select the playmode with an enum.
    /// <summary>
    /// The PlayMode enum specifies if we are operating in XR or Desktop
    /// mode.  The option exists for development purposes only.  In XR
    /// mode, the player is instantiated as an XOrigin object.  In Desktop
    /// mode, the player is a first-person player object.
    /// </summary>
    public enum PlayMode { Desktop, XR }
    [Header("Play Mode")]
    public PlayMode playMode = PlayMode.Desktop;

    [Space]
    [Header("Player Prefabs")]
    /// The two PlayMode modalities require two different player prefabs.
    /// Note that the player is instantiated by the GM on startup and
    /// survives scene changes (do not destroy on load).
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


    /// <summary>
    /// Awake()
    /// </summary>
    private void Awake()
    {
        /// Implement singleton GM
        if (instance == null)
        {
            debug("New GM.");
            instance = this;
        }
        else
        {
            //self-terminate if there already exists a GM
            debug("A GM already exists... self-terminating.");
            Destroy(this);
            return;
        }

        /// Enable all objects tagged as XR and disalbe all objects as DESKTOP
        /// if we are in XR mode.  Likewise, enable all objects tagged as
        /// DESKTOP and disalbe all objects as XR if we are in DESKTOP mode.
        debug("Enable/Disable XR/DESKTOP objects...");
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
                debug("GameManager: Could not find the XROrigin. ***************************");
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



    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        FadeIn();
    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {

            Transform playerStartPosition = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform;
            if (playerStartPosition)
            {

                debug("GM: Found a 'PlayerStartPosition marker' Relocating player...");
                playerCtrl.TelePort(playerStartPosition);
            }
        }

    }


    /// <summary>
    /// Enable all objects tagged as XR and disalbe all objects as DESKTOP
    /// if we are in XR mode.  Likewise, enable all objects tagged as
    /// DESKTOP and disalbe all objects as XR if we are in DESKTOP mode.
    /// This option is exists only to support development without an HMD.
    /// </summary>
    public void EnableDisableXrAndDesktopObjs()
    {
        GameObject[] XR;
        GameObject[] DESKTOP;

        /// find all objects tagged as XR and DESKTOP
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
        debug("GM: Loaded scene " + CurrentScene + "(from " + LastScene + ").");

        LastScene = CurrentScene;
        CurrentScene = scene.name;

        //Look for XR and DESKTOP labels and enable/disable accordingly
        EnableDisableXrAndDesktopObjs();


        //Position the player in the scene
        if (LastScene == "MelaninHall" && CurrentScene == "Campus Scene")
        {
            debug("Relocating Player *********");
            //playerCtrl.TelePort(MelaninToCampusPlayerPosition);
        }
        else
        {
            Transform playerStartPosition = GameObject.FindGameObjectWithTag("PlayerStartPosition").transform;
            if (playerStartPosition)
            {

                debug("GM: Found a 'PlayerStartPosition marker' Relocating player...");
                playerCtrl.TelePort(playerStartPosition);
            }
        }

        //unselect all quads after loading
        //CanvasQuadSelect("");
        debug("GameManager:OnSceneLoaded()...Fading In...");
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
            debug("GM: Failed to Load scene " + NextScene + ".");
            return;
        }
    }

    public void UsePlayerHandModels(bool val)
    {
        if (playMode == PlayMode.XR)
        {
            if (!xrOrigin)
            {
                debug("ERROR GameManager:UsePlayerHandModels() - No XROrigin.");
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
            debug("GameManager: Failed to parse color sting " + clrStr);
        }
    }

    public void SetHandColor(Color clr)
    {
        if (playMode == PlayMode.XR)
        {
            if (!xrOrigin)
            {
                debug("ERROR GameManager:SetHandColor() - No XROrigin.");
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

    /// <summary>
    /// Helper function that prepends source file name and line number to
    /// messages that target the Unity console.  Replace debug() calls
    /// with calls to debug() to use this feature.
    /// </summary>
    /// <param name="msg">The msg to send to the console.</param>
    void debug(string msg)
    {
        var stacktrace = new System.Diagnostics.StackTrace(true);
        string currentFile = System.IO.Path.GetFileName(stacktrace.GetFrame(1).GetFileName());
        int currentLine = stacktrace.GetFrame(1).GetFileLineNumber();  //frame 1 = caller
        Debug.Log(currentFile + "[" + currentLine + "]: " + msg);
    }
}

/* ======================================================================
 * 
 * ====================================================================== */