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
    [SerializeField] GameObject DESKTOP;
    [SerializeField] GameObject XR;
    [SerializeField] GameObject[] DontDestroyObjList;

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



        //find the Player object.  There should be on under XR
        //and one under DESKTOP, but only one of these will be active.
        Player = GameObject.FindGameObjectWithTag("Player");

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