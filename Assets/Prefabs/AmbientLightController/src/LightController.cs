using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ======================================================================
 * The LightController controls the lighting intensities in the hall, 
 * the room and the ambient light.  Each of these can be individually set.
 * To avoid busy setting these intensities in Update(), we create
 * shadow registers to keep track of when the intensity values change.
 ====================================================================== */
public class LightController : MonoBehaviour
{
    public bool UpdateInRealTime = false;
    public Color AmbientLightColor = Color.black;

    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */
    void Update()
    {
        if (UpdateInRealTime)
        {
            RenderSettings.ambientLight = AmbientLightColor;
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


    /* ======================================================================
     * 
     ====================================================================== */
}

