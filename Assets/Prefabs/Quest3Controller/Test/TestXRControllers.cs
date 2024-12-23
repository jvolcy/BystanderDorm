using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestXRControllers : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] [Range(0, 1)] float bumper;
    [SerializeField] [Range(0, 1)] float trigger;
    [SerializeField] [Range(-1, 1)] float thumbstickX;
    [SerializeField] [Range(-1, 1)] float thumbstickY;
    [SerializeField] bool buttonA;
    [SerializeField] bool buttonB;
    [SerializeField] bool home;

    [SerializeField] bool LeftHand;

    Vector3 LeftHandScale = new Vector3(-1, 1, 1);
    Vector3 RightHandScale = new Vector3(1, 1, 1);

    ///----------------------------------------------------------------------
    /// Start is called before the first frame update
    ///----------------------------------------------------------------------
    void Start()
    {
        
    }

    ///----------------------------------------------------------------------
    /// Update is called once per frame
    ///----------------------------------------------------------------------
    void Update()
    {
        animator.transform.localScale = LeftHand ? LeftHandScale : RightHandScale;

        //---------- bumper ----------
        animator.SetFloat("Bumper", bumper);

        //----------trigger ----------
        animator.SetFloat("Trigger", trigger);

        //----------Button A ----------
        animator.SetBool("ButtonA", buttonA);

        //----------Button B ----------
        animator.SetBool("ButtonB", buttonB);

        //----------Home ----------
        animator.SetBool("Home", home);

        //----------Thumbstick ----------
        animator.SetFloat("ThumbX", LeftHand ? -thumbstickX : thumbstickX);
        animator.SetFloat("ThumbY", thumbstickY);

    }


    ///----------------------------------------------------------------------
    ///----------------------------------------------------------------------

}
