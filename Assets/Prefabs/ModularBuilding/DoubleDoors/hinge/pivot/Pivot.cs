using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorSwing { LH, RH };

public class Pivot : MonoBehaviour
{
    [HideInInspector]
    public DoorSwing DoorType;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Open()
    {
        if (DoorType == DoorSwing.LH)
        {
            animator.Play("LH_Open");
        }
        else
        {
            animator.Play("RH_Open");
        }
    }

    public void Close()
    {
        if (DoorType == DoorSwing.LH)
        {
            animator.Play("LH_Close");
        }
        else
        {
            animator.Play("RH_Close");
        }
    }
}
