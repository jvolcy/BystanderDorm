using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    //[SerializeField][Tooltip("Whether or not the CanvasQuad is visible on startup.")]
    //bool MinimizeOnStart = true;

    //[SerializeField]
    //[Tooltip("Whether or not the CanvasQuad is faded out on startup.")]
    //bool FadedOutOnStart = false;

    [SerializeField]
    [Tooltip("The unique ID for this Quad.")]
    string ID;

    //state variable that tracks whether or not the CanvasQuad is visible
    bool isMinimized;

    //state variable that tracks whether or not the CanvasQuad is faded out
    bool isFadedOut;

    [SerializeField]
    GameObject CanvasChildObj;  //the child Canvas object associated with this CanvasQuad

    RectTransform canvasRectTransform;

    Animator animator;
    private IEnumerator disableGO = null;

    bool CameraFound = false;

    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Awake()
    {
        Debug.Log(ID + ": CanvasQuad:Awake()...");

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
                Debug.Log("No object tagged as 'MainCamera' found... will try again in Start().");
                //abort, we will try again in Start()
                return;
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
        var canvas = GetComponentInChildren<Canvas>();
        canvasRectTransform = canvas.GetComponent<RectTransform>();

        /*
        //faded on startup?
        if (FadedOutOnStart)
        {
            isFadedOut = false;     //synchornize the state variable on startup
            FadeOut(true);
        }
        else
        {
            isFadedOut = true;     //synchornize the state variable on startup
            FadeIn(true);
        }
       
        //visible on startup?
        if (MinimizeOnStart)
        {
            isMinimized = false;     //synchornize the state variable on startup
            Hide(true);
        }
        else
        {
            isMinimized = true;     //synchornize the state variable on startup
            Show(true);
        }

        if (disableGO != null) StopCoroutine(disableGO);

        if (FadedOutOnStart || MinimizeOnStart)
        {
            Debug.Log("CanvasQuad:OnEnable() --> Deactivating " + name);
            CanvasChildObj.SetActive(false);
        }
        else
        {
            Debug.Log("CanvasQuad:OnEnable() --> Aactivating " + name);
            CanvasChildObj.SetActive(true);
        }
        */

        GameManager.SelectCanvasQuad += OnSelectCanvasQuad;
        GameManager.ExitingScene += OnExitingScene;
    }

    /* ======================================================================
    * Start is called before the first frame update
    ====================================================================== */
    
    void Start()
    {
        Debug.Log("CanvasQuad:Start()...");
        if (!CameraFound)
        {
            Debug.Log("CanvasQuad:Start()...looking for camera...");
            Awake();
        }




    } //Start()    
    

    /* ======================================================================
     * Show() - uses the animator to show the canvas
     ====================================================================== */
    public void Show(bool NoAnimation = false)
    {
        if (disableGO != null) StopCoroutine(disableGO);
        CanvasChildObj.SetActive(true);
        //Debug.Log(name + ": CQC: Show()...");

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
        Debug.Log(name + ": CQC: Hide()...");

        if (NoAnimation)
        {
            //Debug.Log("Setting Canvas localScale to Vector3.zero");
            animator.Play("HideInstantly");
            DisableGO(0.1f);
        }
        else if (!isMinimized)
        {
            //Debug.Log("Hinding Canvas.");
            animator.Play("Hide");
            DisableGO(2.0f);
        }

        isMinimized = true;
    }

    IEnumerator _DisableGO(float delay)
    {
        // delay for the specified time
        yield return new WaitForSeconds(delay);

        CanvasChildObj.SetActive(false);
    }


    void DisableGO(float delay)
    {
        disableGO = _DisableGO(delay);
        StartCoroutine(disableGO);
    }

    public void FadeIn(bool NoAnimation = false)
    {
        if (disableGO != null) StopCoroutine(disableGO);
        
        CanvasChildObj.SetActive(true);

        if (NoAnimation)
        {
            //Debug.Log(name + ": CQC:FadeIn(NoAnimation)...");
            animator.Play("FadeInInstantly");
        }
        else //if (isFadedOut)
        {
            //Debug.Log(name + ": CQC:FadeIn()...");
            animator.Play("FadeIn");
        }

        isFadedOut = false;
    }



    public void FadeOut(bool NoAnimation = false)
    {
        if (NoAnimation)
        {
            //Debug.Log(name + ": CQC:FadeOut(NoAnimation)...");
            animator.Play("FadeOutInstantly");
            DisableGO(0.1f);
        }
        else //if (!isFadedOut)
        {
            //Debug.Log(name + ": CQC:FadeOut()...");
            animator.Play("FadeOut");
            DisableGO(2.0f);
        }

        isFadedOut = true;
    }

    private void OnDestroy()
    {
        Debug.Log(ID + ": Unsubscribing to OnSelectCanvasQuad...");
        GameManager.SelectCanvasQuad -= OnSelectCanvasQuad;
        GameManager.ExitingScene -= OnExitingScene;
    }

    /// <summary>
    /// Not to be confused with OnSceneLoaded, which is invoked immediately
    /// after a scene is loaded, OnLoadScene is an event defined in our
    /// GameManager.  It is invoked immediately *before* an attempt is made
    /// to load a new scene.
    /// </summary>
    /// <param name="sender"></param>
    void OnExitingScene(object sender, System.EventArgs e)
    {
        //auto terminate if we are changing scenes.
        Destroy(gameObject);
    }

    public void OnSelectCanvasQuad(object sender, string target_id)
    {
        if (ID == target_id)
        {
            Show();
        }
        else
        {
            Debug.Log(ID + "Xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
            Hide(true);
        }

    }

}

    /* ======================================================================
     * 
     ====================================================================== */