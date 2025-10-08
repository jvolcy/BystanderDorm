using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] string HandSelectToken = "HandSelectToken";
    [SerializeField] string LoadCampusSceneToken = "LoadCampusSceneToken";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
//        GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(MelaninHallEntry);
    }

    public void NBDialogCloseCallback((string dialogToken, int buttonIndex) val)
    {
        //Debug.Log("StartSceneManager: Dialog Token = " + val.dialogToken + 
        //    ".  buttonIndex = " + val.buttonIndex);

        if (val.dialogToken == HandSelectToken)
        {
            //once the user selects a skin tone, go right into the simulation
            GameManager.instance.SetHandColor(val.buttonIndex);
            GameManager.LoadScene("Campus Night Scene");
        }
        else if (val.dialogToken == LoadCampusSceneToken)
        {
            //the user has requested the sandbox.  The skintone will be selected there.
            GameManager.LoadScene("Campus Scene");
        }
    }
}
