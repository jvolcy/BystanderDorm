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

    [Tooltip("Canvas quads on the list of secondary quads are displayed one " +
        "at a time. When you select to display any one of these, all others " +
        "are automatically de-selected.")]
    public CanvasQuad[] SecondaryCanvasQuadGroup;


    private void Start()
    {
        Debug.Log("CQC: Start()...");
        SceneManager.sceneLoaded += OnSceneLoaded;
        UnSelectAll();
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        Debug.Log("CanvasQuadController: Loaded scene " + scene.name);
        //Player.FadeIn();
    }

    /// <summary>
    /// for Debug only.
    /// </summary>
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) FadeOut();
        if (Input.GetKeyDown(KeyCode.I)) FadeIn();
    }
    */



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
        SecondaryCanvasQuadGroup[index].Hide(true);
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
        Debug.Log("CQC: Start UnSelectAll()...");
        for (int i = 0; i < SecondaryCanvasQuadGroup.Length; i++)
        {
            UnSelect(i);
        }
        Debug.Log("CQC: UnSelectAll() End.");

    }
}
