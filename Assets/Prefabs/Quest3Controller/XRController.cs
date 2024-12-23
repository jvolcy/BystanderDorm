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


        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            float val = ActivateValue.action.ReadValue<float>();
            Debug.Log("RightActivateValue = " + val);

            Vector2 val2 = TeleportDirection.action.ReadValue<Vector2>();
            Debug.Log("RightTeleportDirection = " + val2);

            float val4 = SelectValue.action.ReadValue<float>();
            Debug.Log("RightSelectValue = " + val4);

            //float val5 = RightActivateValue.action.ReadValue<float>();
            //Debug.Log("RightActivateValue = " + val)5;

        }
        */
    }


    /*
    private void Activate(InputAction.CallbackContext obj)
    {
        float val = obj.action.ReadValue<float>();
        //obj.ReadValue<float>();
        animator.SetFloat("Trigger",val);
        Debug.Log("Activated--> " + val);
    }

    private void DeActivate(InputAction.CallbackContext obj)
    {
        float val = obj.action.ReadValue<float>();
        animator.SetFloat("Trigger", 0);
        Debug.Log("De-Activated--> " + val);
    }

    private void Select(InputAction.CallbackContext obj)
    {
        animator.SetFloat("Bumper", 1);
        Debug.Log("Selected!!");
    }
    private void DeSelect(InputAction.CallbackContext obj)
    {
        animator.SetFloat("Bumper", 0);
        //Debug.Log("DeSelected!!");
    }
    */

}
