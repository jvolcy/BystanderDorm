using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/* ======================================================================
 * ScreenFade leverages the functions of XDialog, the node-based dialog
 * system to implment a screen fader.
 ====================================================================== */
public class ScreenFade : MonoBehaviour
{
    
    Animator animator;
    public bool StartBlack = false;

    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {
        debug("Start()...");

        //get a reference to our animator
        animator = GetComponent<Animator>();

        if (!animator)
        {
            debug("WARNING: Start() ... no <Animator> component found!");
        }

        if (StartBlack) { Black(); }
        else { Clear();  }
    }

    public void Fade2Black() { animator.SetTrigger("Fade2Black"); }
    public void Fade2Clear() { animator.SetTrigger("Fade2Clear"); }
    public void Black() { animator.SetTrigger("Black"); }
    public void Clear() { animator.SetTrigger("Clear"); }

    /* ======================================================================
    * Stubs for legacy compatibility
    ====================================================================== */
    /*
    public void FadeIn(bool NoAnimation = false)
    {

        if (NoAnimation)
        {
            //debug(name + ": CQC:FadeIn(NoAnimation)...");
            //animator.Play("FadeInInstantly");
            Clear();
        }
        else //if (isFadedOut)
        {
            //debug(name + ": CQC:FadeIn()...");
            //animator.Play("FadeIn");
            Fade2Clear();
        }

    }



    public void FadeOut(bool NoAnimation = false)
    {
        if (NoAnimation)
        {
            //debug(name + ": CQC:FadeOut(NoAnimation)...");
            //animator.Play("FadeOutInstantly");
            Black();
        }
        else //if (!isFadedOut)
        {
            //debug(name + ": CQC:FadeOut()...");
            //animator.Play("FadeOut");
            Fade2Black();
        }

    }
    */

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