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
    [SerializeField] bool UseHandControllerOnStartup = false;


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
    }

    // Start is called before the first frame update
    void Start()
    {
        UseHandController(UseHandControllerOnStartup);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) { UseHandController(true); }
        if (Input.GetKeyDown(KeyCode.D)) { UseHandController(false); }
    }
}
