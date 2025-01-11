using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Management;

/// This is the scene manager for the hall scene.  The project is 
/// organized such that there is a singleton GameManager that survives from
/// scene to scene.  Each scene has a scene manager.
public class HallSceneManager : MonoBehaviour
{
    [SerializeField] GameObject objToEnableAfterGFTL;   //for debugging only
    [SerializeField] Transform playerWakeUpPoint;
    [SerializeField] Transform roomEntryPoint;
    [SerializeField] Vector3 playerWakeUpPosition = new Vector3(11f, 9.15f, 0f);
    [SerializeField] Vector3 playerWakeUpOrientation = new Vector3(0f, 180f, 0f);

    //[HideInInspector]
    //public GameManager gameManager = null;

    // Start is called before the first frame update

    void Start()
    {
        debug("HallSceneManager:Start()...");
        //FindGameManager();
        if (!GameManager.instance)
        {
            debug("HallSceneManager:Start()...Did not find a game manager. ******************");
        }
    }


    public void LoadScene(string sceneName)
    {
        //FindGameManager();

        if (!GameManager.instance)
        {
            debug("HallSceneManager:LoadScene - did not find an GameManager.");
            debug("Could not load scene " + sceneName);
            return;
        }

        GameManager.LoadScene(sceneName);
    }

    /*
    void FindGameManager()
    {
        if (gameManager) return;

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            debug("HallSceneManager:FindGameManager - did not find a GameManager.");
        }
    
    }
    */

    public void FadeIn(bool instant = false) { GameManager.instance.FadeIn(instant); }
    public void FadeOut(bool instant = false) { GameManager.instance.FadeOut(instant); }


    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //FindGameManager();
            //put us in front of our room, facing the door
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(roomEntryPoint); //(11f, 9.15f, 0f), new Vector3(0f, 180f, 0f)
            objToEnableAfterGFTL.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            //InputTracking.Recenter();
           // var x = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<XRInputSubsystem>().TryRecenter();
            //debug("Recentering " + (x ? "succeeded." : "failed."));
        }
    }


    /* ======================================================================
     * This signal handler is called immediately after the lights are
     * dimmed in the room.  Here, we will close the door (if it is open)
     * and reposition the player so that she is facing the clock.
     ====================================================================== */
    public void PrepareRoomSceneSignalReceiver()
    {
        //debug("HallSceneManager: PrepareRoomSceneSignalReceiver()...");
        //FindGameManager();
        GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(playerWakeUpPoint);
    }

    /// <summary>
    /// Helper function that prepends source file name and line number to
    /// messages that target the Unity console.  Replace Debug.Log() calls
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
 ====================================================================== */