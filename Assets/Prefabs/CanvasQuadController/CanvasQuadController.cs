using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasQuadController : MonoBehaviour
{
    public CanvasQuad FadeCanvasQuad;
    public CanvasQuad[] canvasQuads;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) FadeCanvasQuad.FadeToTransparent();
        if (Input.GetKeyDown(KeyCode.O)) FadeCanvasQuad.FadeToOpaque();
        if (Input.GetKeyDown(KeyCode.Alpha1)) Select(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) UnSelect(0);
        if (Input.GetKeyDown(KeyCode.Alpha3)) Select(1);
        if (Input.GetKeyDown(KeyCode.Alpha4)) UnSelect(1);
    }

    public void Select(int index)
    {
        for (int i = 0; i < canvasQuads.Length; i++)
        {
            //unSelect all but the provided index
            if (i != index) UnSelect(i);
        }

        //select the quad at the specified index if it isn't already selected
        canvasQuads[index].Show();
        //Animator animator = canvasQuads[index].GetComponent<Animator>();
        //if (animator.GetBool("Show") == false) { animator.SetBool("Show", true); }
    }

    public void UnSelect(int index)
    {
        canvasQuads[index].Hide();
        //Animator animator = canvasQuads[index].GetComponent<Animator>();
        //if (animator.GetBool("Show") == true) { animator.SetBool("Show", false); }
    }

    public void kill(int index)
    {
        Destroy(canvasQuads[index]);
    }
}
