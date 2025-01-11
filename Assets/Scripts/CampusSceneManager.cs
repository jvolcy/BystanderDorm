using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// This is the scene manager for the campus scene.  The project is
/// organized such that there is a singleton GameManager that survives from
/// scene to scene.  Each scene has a scene manager.
public class CampusSceneManager : MonoBehaviour
{

    [SerializeField] CanvasQuad BeginSimulation;
    [SerializeField] EventTrigger MelaninHallTrigger;
    [SerializeField] Transform BellesHallEntry;
    [SerializeField] Transform MelaninHallEntry;
    [SerializeField] Transform MelaninToCampusPlayerPosition;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //put us in front of our room, facing the door
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(MelaninHallEntry);
        }
    }


    public void LoadScene(string sceneName)
    {

        if (sceneName == "MelaninHall")
        {
            debug("CampusSceneManager:LoadScene()...Visited MelaninHall set to true.");
            GameManager.visitedMelaninHall = true;
        }

        GameManager.LoadScene(sceneName);       //load the requested scene
    }


    private void OnDestroy()
    {
        debug("CampusSceneManager: Unsubscribing to OnSceneLoaded...");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        debug("CampusSceneManager:OnSceneLoaded()...");

        if (GameManager.visitedMelaninHall == true)
        {
            debug("CampusSceneManager: Melaning Hall visited.");
            GameManager.CanvasQuadSelect("BeginSimulation");
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(MelaninToCampusPlayerPosition);
            MelaninHallTrigger.gameObject.SetActive(false);
        }
        else
        {
            debug("CampusSceneManager: Melaning Hall NOT visited.");
            GameManager.CanvasQuadSelect("");
            MelaninHallTrigger.gameObject.SetActive(true);
        }
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
