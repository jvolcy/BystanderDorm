using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Select the playmode with an enum
    enum PlayMode { Desktop, XR }
    [SerializeField] PlayMode playMode = PlayMode.Desktop;

    //set the parent gameObjects for XR and DESKTOP specific GOs
    //[SerializeField] GameObject DESKTOP;
    //[SerializeField] GameObject XR;
    [SerializeField] GameObject[] DontDestroyObjList;
    [SerializeField] string FirstScene = "Campus Scene";

    GameObject Player;

    static GameManager instance;

   /* ======================================================================
    * 
    ====================================================================== */
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }

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
        EnableDisableXrAndDesktopObjs();

        /*
        //Enable/Disable XR/DESKTOP player and other GOs
        if (playMode == PlayMode.Desktop)
        {
            XR.SetActive(false);
            DESKTOP.SetActive(true);
        }
        else
        {
            DESKTOP.SetActive(false);
            XR.SetActive(true);
        }
        */
        //subscribe to the scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;

        //find the Player object.  There should be on under XR
        //and one under DESKTOP, but only one of these will be active.
        Player = GameObject.FindGameObjectWithTag("Player");

        SceneManager.LoadScene(FirstScene);
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
    void EnableDisableXrAndDesktopObjs()
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
     * This signal handler is called immediately after the lights are
     * dimmed in the room.  Here, we will close the door (if it is open)
     * and reposition the player so that she is facing the clock.
     ====================================================================== */
    public void PrepareRoomScene()
    {
        Debug.Log("signaled");
        CharacterController characterController = Player.GetComponent<CharacterController>();
        characterController.enabled = false;
        Player.transform.position = new Vector3(11.4f, 9.25f, -4.20f);
        Player.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        characterController.enabled = true;

    }
}

/* ======================================================================
 * 
 ====================================================================== */