using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/* ======================================================================
 * CanvasQuad produces a floating canvas in world space that is suitable
 * for use in VR and on a Desktop application.  Use the Inspector
 * PlayMode option to specify the modality.
 * In either case, the floating canvas is parented to the main camera.
 * You may specify the camera in the Inspector or you may let the script
 * find it for you.  The srcipt will first search for active cameras
 * before looking for inactive ones.
 * You specify the distance the canvas should be from the camera and
 * its physical size.  The defaults are 0.5m x 0.5m at a distance of
 * 0.5m from the camera.
 * In XR mode, the script will search for and disable the 
 * "StandaloneInputModule" or "InputSystemUIInputModule" components of
 * the EnventSystem object that is automatically (or manually) created
 * by the UI. See the note here for details:
 * https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/ui-setup.html
 * 
 * NOTE THAT, BY DEFAULT, THE CANVAS IS SCALED AT 0,0,0.  That means
 * that it is not visible for editing.  Change the scale to 1,1,1 while
 * editing the UI.  Reset it to 0,0,0 when done.  That way, the canvas
 * is not visible (in the "hide") state on startup.
 ====================================================================== */
public class ScreenFade : MonoBehaviour
{
    //Select the playmode with an enum
    public enum PlayMode { Desktop, XR }
    public PlayMode playMode = PlayMode.Desktop;

    //state variable that tracks whether or not the CanvasQuad is faded out
    bool isFadedOut = false;

    //[SerializeField]
    //GameObject CanvasChildObj;  //the child Canvas object associated with this CanvasQuad

    Animator animator;

    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void OnEnable()
    {
        debug("ScreenFade:OnEnable()...");

        //if we are in VR mode, disable any "Standalone Input Module" or
        //"Input System UI Input Module" in the scene
        if (playMode == PlayMode.XR)
        {
            UnityEngine.EventSystems.StandaloneInputModule[] mods = FindObjectsByType<UnityEngine.EventSystems.StandaloneInputModule>(FindObjectsSortMode.None);
            foreach (var mod in mods) { mod.enabled = false; }

            UnityEngine.InputSystem.UI.InputSystemUIInputModule[] mods2 = FindObjectsByType<UnityEngine.InputSystem.UI.InputSystemUIInputModule>(FindObjectsSortMode.None);
            foreach (var mod in mods2) { mod.enabled = false; }
        }

        //get a reference to our animator
        animator = GetComponent<Animator>();

        if (!animator)
        {
            debug("WARNING: ScreenFade:OnEnable() ... no <Animator> component found!");
        }
       
    }

    /* ======================================================================
    * Start is called before the first frame update
    ====================================================================== */

    public void FadeIn(bool NoAnimation = false)
    {
        if (!isFadedOut) return;        //nothing to do

        if (NoAnimation)
        {
            //debug(name + ": CQC:FadeIn(NoAnimation)...");
            animator.Play("FadeInInstantly");
        }
        else //if (isFadedOut)
        {
            //debug(name + ": CQC:FadeIn()...");
            animator.Play("FadeIn");
        }

        isFadedOut = false;
    }



    public void FadeOut(bool NoAnimation = false)
    {
        if (isFadedOut) return;     //nothing to do

        if (NoAnimation)
        {
            //debug(name + ": CQC:FadeOut(NoAnimation)...");
            animator.Play("FadeOutInstantly");
        }
        else //if (!isFadedOut)
        {
            //debug(name + ": CQC:FadeOut()...");
            animator.Play("FadeOut");
        }

        isFadedOut = true;
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