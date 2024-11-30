using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class HallSceneManager : MonoBehaviour
{
    [SerializeField] GameObject objToEnableAfterGFTL;   //for debugging only
    //[SerializeField] PlayableDirector RoomDoorTL;
    
    GameObject Player;

   /* ======================================================================
    * 
    ====================================================================== */


    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CharacterController characterController = Player.GetComponent<CharacterController>();

            //put us in front of our room
            characterController.enabled = false;
            Player.transform.position = new Vector3(10.98f, 9.15f, -0.45f);
            //Player.transform.Rotate(0, 0f, 0);
            characterController.enabled = true;

            EndOfGirlfriendTLSignalReceiver();

        }
    }
    

    /* ======================================================================
     * Here, we need to enable Hall Arrow #3 when the girlfriend timeline
     * concludes.
     ====================================================================== */
    
    public void EndOfGirlfriendTLSignalReceiver()
    {
        objToEnableAfterGFTL.SetActive(true);
    }
    

    /* ======================================================================
     * This signal handler is called immediately after the lights are
     * dimmed in the room.  Here, we will close the door (if it is open)
     * and reposition the player so that she is facing the clock.
     ====================================================================== */
    public void PrepareRoomSceneSignalReceiver()
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