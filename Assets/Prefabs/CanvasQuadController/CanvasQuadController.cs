using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// CanvasQuads are floating UI Panels that attach themselves as children of
/// the main camera to produce a heads-up display in desktop or VR applications.
/// Such applications may make use of multiple such quads.  The component
/// CanvasQuadController helps manage these quads to simplify development.
/// There is a primary quad and a grouping of secondary quads.  The primary
/// quad is always displayed, though its alpha may be 0 (transparent).  The
/// secondary quads are displayed one at a time.  CanvasQuadController takes
/// care of that logic.  If you select to display one of the quads, the script
/// automatically unselects all other secondary quads.  Use the primary quad
/// for global effects like fade-in and fade-out.
/// </summary>
public class CanvasQuadController : MonoBehaviour
{
    [Tooltip("The primary quad is always displayed, though it may be " +
        "transparent.  Use this quad for global effects like fade-in " +
        "and fade-out.")]
    public CanvasQuad PrimaryCanvasQuad;

    [Tooltip("Canvas quads on the list of secondary quads are displayed one " +
        "at a time. When you select to display any one of these, all others " +
        "are automatically de-selected.")]
    public CanvasQuad[] SecondaryCanvasQuadGroup;


    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("CanvasQuadController: Loaded scene " + scene.name);
        FadeIn();
    }

    /// <summary>
    /// for Debug only.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) FadeOut();
        if (Input.GetKeyDown(KeyCode.I)) FadeIn();
        if (Input.GetKeyDown(KeyCode.Alpha1)) Select(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UnSelect(0);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Select(1);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UnSelect(1);
    }

    /// <summary>
    /// Helper function to fade out (fade to black) the display.
    /// Note that we have to fade in the primary quad to create a
    /// black-out of the display.
    /// </summary>
    public void FadeOut()
    {
        PrimaryCanvasQuad.FadeIn();
    }

    /// <summary>
    /// Helper function to fade in (fade from black) the display.
    /// Note that we have to fade out the primar quad to create a
    /// fade-in of the display.
    /// </summary>
    public void FadeIn()
    {
        PrimaryCanvasQuad.FadeOut();
    }

    /// <summary>
    /// Function to select one of the secondary quads to be displayed.
    /// All other quads are automatically de-selected (i.e., hidden).
    /// </summary>
    /// <param name="index">The index of the quad to display.  No 
    /// bounds checking is done on this index.</param>
    public void Select(int index)
    {
        for (int i = 0; i < SecondaryCanvasQuadGroup.Length; i++)
        {
            //unSelect all but the provided index
            if (i != index) UnSelect(i);
        }

        //select the quad at the specified index if it isn't already selected
        SecondaryCanvasQuadGroup[index].Show();
    }

    /// <summary>
    /// Function to de-select one of the secondary quads.  Supply the quad's
    /// index.
    /// </summary>
    /// <param name="index"></param>
    public void UnSelect(int index)
    {
        SecondaryCanvasQuadGroup[index].Hide();
    }

    /// <summary>
    /// Function to destroy no-longer needed canvas quads.
    /// </summary>
    /// <param name="index"></param>
    public void kill(int index)
    {
        Destroy(SecondaryCanvasQuadGroup[index]);
    }

    public void UnSelectAll()
    {
        for (int i = 0; i < SecondaryCanvasQuadGroup.Length; i++)
        {
            UnSelect(i);
        }
    }
}
