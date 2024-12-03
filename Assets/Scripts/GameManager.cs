using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Select the playmode with an enum
    enum PlayMode { Desktop, XR }
    [SerializeField] PlayMode playMode = PlayMode.Desktop;

    [SerializeField] GameObject[] DontDestroyObjList;
    [SerializeField] string FirstScene = "Campus Scene";

    //the public sceneManager is used by the timelines
    public SceneManager sceneManager;

    GameObject Player;

    static GameManager instance;

   /* ======================================================================
    * 
    ====================================================================== */
    private void Awake()
    {
        EnableDisableXrAndDesktopObjs();
        /*
        string tag;
        if (playMode == PlayMode.Desktop) { tag = "DESKTOP"; }
        else if (playMode == PlayMode.XR) { tag = "XR"; }
        else { tag = "Unknown"; }
        */
        if (instance == null && CompareTag(tag))
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

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

        //configure our persistent objects
        foreach (var obj in DontDestroyObjList)
        {
            DontDestroyOnLoad(obj);
        }
    }


    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {

        //subscribe to the scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;

        //find the Player object.  There should be on under XR
        //and one under DESKTOP, but only one of these will be active.
        Player = GameObject.FindGameObjectWithTag("Player");

        //SceneManager.LoadScene(FirstScene);
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
        Debug.Log("GM: Loaded scene " + scene.name);
        //Look for XR and DESKTOP labels and enable/disable accordingly
        EnableDisableXrAndDesktopObjs();
    }

    /* ======================================================================
     * 
     ====================================================================== */
    public void LoadNextScene()
    {
        //load the next scene
        Debug.Log("Load Next Scene...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /* ======================================================================
     * 
     ====================================================================== */
    public void LoadScene(string sceneName)
    {
        //load the next scene
        SceneManager.LoadScene(sceneName);
    }

}

/* ======================================================================
 * 
 * ====================================================================== */