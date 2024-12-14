using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to an object with a Renderer.  The script augments
/// the Renderer's material with an emissive element, resulting in a
/// highlighted appearance.  Note: objects with exiting emissive materials
/// may not benefit from highlighting.
/// You may specify the emissive color in the inspector.
/// Use the two public methods Highlight() and UnHighlight() to toggle the
/// object's highlighted state.
/// </summary>
public class highlight : MonoBehaviour
{
    public Color32 HighlightColor = new Color32(0x3C, 0x23, 0x3C, 0xFF);
    Material OriginalMat;
    Material HighlightedMat;
    Renderer render;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        //get a reference to the object Renderer
        render = GetComponentInChildren<Renderer>();

        //store the objects original material
        OriginalMat = render.material;

        //create a new highlighted material by copying the original
        //and adding an emissive element
        HighlightedMat = new(OriginalMat);
        HighlightedMat.EnableKeyword("_EMISSION");  //turn on emission
        HighlightedMat.SetColor("_EmissionColor", HighlightColor);
    }

    /*
    /// <summary>
    /// Update is called once per frame.  Here it is used for debugging only.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Highlight();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            UnHighlight();
        }

    }
    */

    /// <summary>
    /// 
    /// </summary>
    void Highlight()
    {
        render.material = HighlightedMat;
    }

    /// <summary>
    /// 
    /// </summary>
    void UnHighlight()
    {
        render.material = OriginalMat;
    }
}
