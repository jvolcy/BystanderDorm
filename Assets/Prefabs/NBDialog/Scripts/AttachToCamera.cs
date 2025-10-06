using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/* ======================================================================
 * AttachToCamera parents the GameObject to which it is attached to the
 * main camera.
 * You may specify the camera in the Inspector or you may let the script
 * find it for you.  The srcipt will first search for active cameras
 * before looking for inactive ones.  It will search in Awake() and then
 * again in Start() if a camera is not found in Awake().  You may specify
 * whether or not it should consider inactive cameras in the search.
 * You can specify the relative position of the GO relative to the camera.
 ====================================================================== */
public class AttachToCamera : MonoBehaviour
{
    //Select the playmode with an enum
    public Camera MainCamera;
    public Vector3 RelativePosition = Vector3.zero;
    public bool IncludeInactiveCameras = false;
    bool CameraFound = false;

    /* ======================================================================
     * Awake()
     ====================================================================== */
    void Awake()
    {
        debug("Awake()...");
        FindCamera();
    }

    /* ======================================================================
    * Start is called before the first frame update
    ====================================================================== */
    
    void Start()
    {
        if (!CameraFound)
        {
            debug("Start()...looking for camera...");
            FindCamera();
        }
    } //Start()    

    /// <summary>
    /// Function to find the main camera and parent the GO to it.
    /// </summary>
    public void FindCamera()
    {
        if (MainCamera != null)
        //user has specified the camera to use
        {
            transform.parent = MainCamera.transform;
            CameraFound = true;
        }
        //user has not specified the camera to use... look for it
        else
        {
            //find all game objects that are tagged as the main camera
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MainCamera");

            //Here is the sequence we will follow: 1) verify that there is at
            //least one GO tagged as "MainCamera". 2)If there is only one GO,
            //parent to it.  3) If there are multiple candidates, parent to the
            //first enabled object.  4) If there are no enabled objects, parent
            //to the first found.

            //verify that there is at least one GO tagged as "MainCamera"
            if (gameObjects.Length == 0)
            {
                debug("No object tagged as 'MainCamera' found... will try again in Start().");
                //abort, we will try again in Start()
                return;
            }
            //if there is only one GO, parent to it if the active option is met.
            else if (gameObjects.Length == 1)
            {
                //If the one camera is inactive and we are not including inactives, do nothing.
                //Otherwise attach to the camera.
                if (!(gameObjects[0].activeInHierarchy == false && IncludeInactiveCameras == false))
                {
                    //here, either the camera is active or it is inactive and we are including inactives
                    transform.parent = gameObjects[0].transform;
                    CameraFound = true;
                }
            }
            else
            {
                //if there are multiple candidates, parent to the first enabled object
                foreach (var go in gameObjects)
                {
                    if (go.activeInHierarchy)
                    {
                        transform.parent = go.transform;
                        CameraFound = true;
                        break;
                    }
                }

                //if there are no enabled objects, parent to the first found camera, if we are including inactives
                if (!CameraFound && IncludeInactiveCameras)
                {
                    transform.parent = gameObjects[0].transform;
                    CameraFound = true;
                }
            }
        } //else (if (MainCamera != null))

        //set the transform
        if (CameraFound)
        {
            transform.localPosition = RelativePosition;
            transform.localRotation = Quaternion.identity;  //no rotation
        }
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

    /* ======================================================================
     * 
     ====================================================================== */