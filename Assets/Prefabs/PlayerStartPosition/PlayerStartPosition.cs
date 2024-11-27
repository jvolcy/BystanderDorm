using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStartPosition : MonoBehaviour
{
    public bool AutoMovePlayerOnSceneLoad = true;
    public bool AutoMovePlayerOnSceneReLoad = true;

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
            SetPlayerStartPosition();
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
    void SetPlayerStartPosition()
    {
        Player.transform.position = transform.position;
        Player.transform.rotation = transform.rotation;
    }

    /* ======================================================================
     * 
     ====================================================================== */
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("PlayerStartPosition: Loaded scene " + scene.name);
        SetPlayerStartPosition();
    }

}


/* ======================================================================
 * 
 ====================================================================== */