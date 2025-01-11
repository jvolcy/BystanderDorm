using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRController : MonoBehaviour
{

    //boolean: true = right hand; false = left hand
    public bool RightHand = true;

    //ActionBasedController controller;
    //public GameObject HandController;

    [SerializeField] InputActionReference ActivateValue;        //this is the trigger
    [SerializeField] InputActionReference SelectValue;          //this is the bumper
    [SerializeField] InputActionReference TeleportDirection;    //this is the thumbstick

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Hand Start");
        animator = GetComponent<Animator>();

        /*
        controller = GetComponentInParent<ActionBasedController>();
        controller.activateAction.action.started += Activate;
        controller.selectAction.action.started += Select;
        controller.activateAction.action.canceled += DeActivate;
        controller.selectAction.action.started += Select;
        controller.selectAction.action.canceled += DeSelect;
        */
        //scale x for left or right hand
        transform.localScale = new Vector3(transform.localScale.x * (RightHand ? 1f : -1f), transform.localScale.y, transform.localScale.z);
    }


        
    // Update is called once per framer
    void Update()
    {
        float trigger = ActivateValue.action.ReadValue<float>();
        Vector2 ThumbStick = TeleportDirection.action.ReadValue<Vector2>();
        float bumper = SelectValue.action.ReadValue<float>();

        //float val5 = RightActivateValue.action.ReadValue<float>();
        animator.SetFloat("Trigger", trigger);
        animator.SetFloat("Bumper", bumper);
        animator.SetFloat("ThumbX", ThumbStick.x * (RightHand ? 1f : -1f));
        animator.SetFloat("ThumbY", ThumbStick.y);

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
