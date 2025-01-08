using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class PlayerStartPosition : MonoBehaviour
{
    public bool AutoMovePlayerOnSceneLoad = true;
    public bool AutoMovePlayerOnSceneReLoad = true;
    public XRInputSubsystem inputSubsystem;

    GameObject Player;

    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {
        //find the Player object.  There should be one under XR
        //and one under DESKTOP, but only one of these will be active.
       Player = GameObject.FindGameObjectWithTag("Player");

        if (AutoMovePlayerOnSceneLoad)
        {
            //SetPlayerStartPosition();
            Player.GetComponent<PlayerCtrl>().TelePort(transform.position, transform.localEulerAngles);

        }

        //subscribe to the scene load event
        if (AutoMovePlayerOnSceneReLoad)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    /* ======================================================================
     * When this object is destroyed, unsubscribe to the scene loaded
     * event.
     ====================================================================== */
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    /* ======================================================================
     * 
     ====================================================================== */
    /*
    void SetPlayerStartPosition()
    {
        Player.transform.position = transform.position;
        Player.transform.localEulerAngles = transform.localEulerAngles;

        inputSubsystem.TryRecenter();

        //var cam = GameObject.FindGameObjectWithTag("MainCamera"); ;
        //cam.transform.position = Vector3.zero;
        //cam.transform.rotation = Quaternion.identity;

        Physics.SyncTransforms();
    }
    */
    /* ======================================================================
     * 
     ====================================================================== */
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("PlayerStartPosition: Loaded scene " + scene.name);
        Player.GetComponent<PlayerCtrl>().TelePort(transform.position, transform.localEulerAngles);
        //SetPlayerStartPosition();
    }

}


/* ======================================================================
 * 
 ====================================================================== */