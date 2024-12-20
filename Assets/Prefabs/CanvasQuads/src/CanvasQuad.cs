using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ======================================================================
 * CanvasQuad produces a floating canvas in world space that is suitable
 * for use in VR and on a Desktop application.  Use the Inspector
 * PlayMode option to specify the modality.
 * In either case, the floating canvas is parented to the main camera.
 * You may specify the camera in the Inspector or you may let the script
 * find it for you.  The srcipt will first search for active cameras
 * before looking for inactive ones.
 * You specify the distance the canvas should be from the camera and
 * its physical size.  The defaults are 0.5m x 0.5m at a distance of
 * 0.5m from the camera.
 * In XR mode, the script will search for and disable the 
 * "StandaloneInputModule" or "InputSystemUIInputModule" components of
 * the EnventSystem object that is automatically (or manually) created
 * by the UI. See the note here for details:
 * https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/ui-setup.html
 * 
 * NOTE THAT, BY DEFAULT, THE CANVAS IS SCALED AT 0,0,0.  That means
 * that it is not visible for editing.  Change the scale to 1,1,1 while
 * editing the UI.  Reset it to 0,0,0 when done.  That way, the canvas
 * is not visible (in the "hide") state on startup.
 ====================================================================== */
public class CanvasQuad : MonoBehaviour
{
    //Select the playmode with an enum
    public enum PlayMode { Desktop, XR }
    public PlayMode playMode = PlayMode.Desktop;

    public Camera MainCamera;
    public float DistanceFromCamera = 0.5f;
    public float Scale = 0.5f;

    [SerializeField][Tooltip("Whether or not the CanvasQuad is visible on startup.")]
    bool MinimizeOnStart = true;

    [SerializeField]
    [Tooltip("Whether or not the CanvasQuad is faded out on startup.")]
    bool FadedOutOnStart = false;

    //state variable that tracks whether or not the CanvasQuad is visible
    bool isMinimized;

    //state variable that tracks whether or not the CanvasQuad is faded out
    bool isFadedOut;

    Canvas canvas;  //the child canvas associated with this CanvasQuad
    RectTransform canvasRectTransform;

    Animator animator;
    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {
        bool CameraFound = false;

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
                Debug.Log("No object tagged as 'MainCamera' found.");
            }
            //if there is only one GO, parent to it.
            else if (gameObjects.Length == 1)
            {
                transform.parent = gameObjects[0].transform;
                CameraFound = true;
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

                //if there are no enabled objects, parent to the first found
                if (!CameraFound)
                {
                    transform.parent = gameObjects[0].transform;
                }
            }
        } //else (if (MainCamera != null))

        //set the transform
        if (CameraFound)
        {
            transform.localPosition = new Vector3(0f, 0f, DistanceFromCamera);
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(Scale, Scale, 1);
        }


        //if we are in VR mode, disable any "Standalone Input Module" or
        //"Input System UI Input Module" in the scene
        if (playMode == PlayMode.XR)
        {
            UnityEngine.EventSystems.StandaloneInputModule[] mods = FindObjectsByType<UnityEngine.EventSystems.StandaloneInputModule>(FindObjectsSortMode.None);
            foreach (var mod in mods) { mod.enabled = false; }

            UnityEngine.InputSystem.UI.InputSystemUIInputModule[] mods2 = FindObjectsByType<UnityEngine.InputSystem.UI.InputSystemUIInputModule>(FindObjectsSortMode.None);
            foreach (var mod in mods2) { mod.enabled = false; }
        }

        //get a reference to our animator
        animator = GetComponent<Animator>();

        //get a reference to our Canvas child object
        canvas = GetComponentInChildren<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        //visible on startup?
        if (MinimizeOnStart)
        { Hide(true); }
        else
        { Show(true); }


        //faded on startup?
        if (FadedOutOnStart)
        { FadeToTransparent(true); }
        else
        { FadeToOpaque(true); }

    } //Start()    

    /* ======================================================================
     * Show() - uses the animator to show the canvas
     ====================================================================== */
    public void Show(bool NoAnimation = false)
    {
        if (NoAnimation)
        {
            //Debug.Log("Setting Canvas localScale to Vector3.one");
            animator.Play("ShowInstantly");
        }
        else if (isMinimized)
        {
            //Debug.Log("Showing Canvas.");
            animator.Play("Show");
        }

        isMinimized = false;
    }

    /* ======================================================================
     * Hide() - uses the animator to hide the canvas
     ====================================================================== */
    public void Hide(bool NoAnimation = false)
    {
        if (NoAnimation)
        {
            //Debug.Log("Setting Canvas localScale to Vector3.zero");
            animator.Play("HideInstantly");
        }
        else if (!isMinimized)
        {
            //Debug.Log("Hinding Canvas.");
            animator.Play("Hide");
        }

        isMinimized = true;
    }



    public void FadeToOpaque(bool NoAnimation = false)
    {
        if (NoAnimation)
        {
            animator.Play("OpaqueInstantly");
        }
        else if (isFadedOut)
        {
            animator.Play("FadeToOpaque");
        }

        isFadedOut = false;
    }



    public void FadeToTransparent(bool NoAnimation = false)
    {
        if (NoAnimation)
        {
            animator.Play("TransparentInstantly");
        }
        else if (!isFadedOut)
        {
            animator.Play("FadeToTransparent");
        }

        isFadedOut = true;
    }


}

    /* ======================================================================
     * 
     ====================================================================== */