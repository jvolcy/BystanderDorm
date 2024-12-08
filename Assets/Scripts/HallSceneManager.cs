using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;

public class HallSceneManager : MonoBehaviour
{
    [SerializeField] GameObject objToEnableAfterGFTL;   //for debugging only
    [SerializeField] Vector3 playerWakeUpPosition = new Vector3(11f, 9.15f, 0f);
    [SerializeField] Vector3 playerWakeUpOrientation = new Vector3(0f, 180f, 0f);

    GameObject Player;

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
            //put us in front of our room, facing the door
            Player.GetComponent<PlayerCtrl>().TelePort(new Vector3(11f, 9.15f, 0f), new Vector3(0f, 180f, 0f));
            objToEnableAfterGFTL.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            //InputTracking.Recenter();
           // var x = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRInputSubsystem>().TryRecenter();
            //Debug.Log("Recentering " + (x ? "succeeded." : "failed."));
        }
    }


    /* ======================================================================
     * This signal handler is called immediately after the lights are
     * dimmed in the room.  Here, we will close the door (if it is open)
     * and reposition the player so that she is facing the clock.
     ====================================================================== */
    public void PrepareRoomSceneSignalReceiver()
    {
        Player.GetComponent<PlayerCtrl>().TelePort(playerWakeUpPosition, playerWakeUpOrientation);
    }

}

/* ======================================================================
 * 
 ====================================================================== */