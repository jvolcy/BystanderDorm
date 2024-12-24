using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DualController : MonoBehaviour
{
    //[SerializeField] ActionBasedController HandController;
    //[SerializeField] ActionBasedController DeviceController;
    [SerializeField] Transform HandController;
    [SerializeField] Transform DeviceController;

    //This static flag "UsingHandController" ensures that
    //all DualController objects are configured in the
    //same way:  they either both show a hand or
    //both show a device.
    static bool UsingHandController = false;

    /// <summary>
    /// Function that sets the display mode to either
    /// "hand" or "device" model.
    /// </summary>
    /// <param name="val">true="hand" model; false="device" model.</param>
    public void UseHandController(bool val)
    {
        if (val)
        {
            DeviceController.gameObject.SetActive(false);
            HandController.gameObject.SetActive(true);
        }
        else
        {
            DeviceController.gameObject.SetActive(true);
            HandController.gameObject.SetActive(false);
        }

        UsingHandController = val;
    }

   /// <summary>
   /// One hand will set the static UsingHandController flag.  When
   /// the second hand is instantiated, it will assume the
   /// same configuration.  This way, we avoid the situation
   /// where one had is in "hand" mode and the other is in
   /// "device" mode.
   /// </summary>
    void Start()
    {
        UseHandController(UsingHandController);
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) { UseHandController(true); }
        if (Input.GetKeyDown(KeyCode.D)) { UseHandController(false); }
    }
    */
}
