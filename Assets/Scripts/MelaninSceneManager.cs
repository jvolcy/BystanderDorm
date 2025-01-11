using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This is the scene manager for the melanin hall scene.  The project is 
/// organized such that there is a singleton GameManager that survives from
/// scene to scene.  Each scene has a scene manager.
public class MelaninSceneManager : MonoBehaviour
{
    //public GameManager gameManager = null;

    // Start is called before the first frame update

    /*
    void Start()
    {
        debug("MelaninSceneManager:Start()...");
        FindGameManager();
        if (!gameManager)
        {
            debug("MelaninSceneManager:Start()...Did not find a game manager. ******************");
        }
    }
  */

    public void LoadScene(string sceneName)
    {
        //FindGameManager();
        /*
        if (!gameManager)
        {
            debug("MelaninSceneManager:LoadScene - did not find an GameManager.");
            debug("Could not load scene " + sceneName);
            return;
        }
        */
        GameManager.LoadScene(sceneName);
    }
    /*
    void FindGameManager()
    {
        if (gameManager) return;

        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            debug("MelaninSceneManager:FindGameManager - did not find a GameManager.");
        }
    }
    */

    //pass-through functions for timelines
    public void FadeIn(bool instant=false) { GameManager.instance.FadeIn(instant); }
    public void FadeOut(bool instant = false) { GameManager.instance.FadeOut(instant); }


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
