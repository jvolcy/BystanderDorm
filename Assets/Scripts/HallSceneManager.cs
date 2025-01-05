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

    [HideInInspector]
    public GameManager gameManager = null;

    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("HallSceneManager:Start()...");
        FindGameManager();
        if (!gameManager)
        {
            Debug.Log("HallSceneManager:Start()...Did not find a game manager. ******************");
        }
    }


    public void LoadScene(string sceneName)
    {
        FindGameManager();

        if (!gameManager)
        {
            Debug.Log("HallSceneManager:LoadScene - did not find an GameManager.");
            Debug.Log("Could not load scene " + sceneName);
            return;
        }

        GameManager.LoadScene(sceneName);
    }

    void FindGameManager()
    {
        if (gameManager) return;

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.Log("HallSceneManager:FindGameManager - did not find a GameManager.");
        }

    }


    public void FadeIn(bool instant = false) { gameManager.FadeIn(instant); }
    public void FadeOut(bool instant = false) { gameManager.FadeOut(instant); }


    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            FindGameManager();
            //put us in front of our room, facing the door
            gameManager.Player.GetComponentInChildren<PlayerCtrl>().TelePort(new Vector3(11f, 9.15f, 0f), new Vector3(0f, 180f, 0f));
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
        //Debug.Log("HallSceneManager: PrepareRoomSceneSignalReceiver()...");
        FindGameManager();
        gameManager.Player.GetComponentInChildren<PlayerCtrl>().TelePort(playerWakeUpPosition, playerWakeUpOrientation);
    }

}

/* ======================================================================
 * 
 ====================================================================== */