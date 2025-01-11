using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// This is the scene manager for the campus night scene.  The project is 
/// organized such that there is a singleton GameManager that survives from
/// scene to scene.  Each scene has a scene manager.
public class CampusNightSceneManager : MonoBehaviour
{
    [SerializeField] Transform BellesHallEntry;
    [SerializeField] Transform MelaninHallEntry;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //put us in front of our room, facing the door
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(BellesHallEntry);
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
