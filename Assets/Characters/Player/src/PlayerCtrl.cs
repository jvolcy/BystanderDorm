using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils;

public class PlayerCtrl : MonoBehaviour
{
    ScreenFade screenFade;

    //the component that updates the hand color
    MagicColorUpdate[] magicColorUpdates;

    DualController[] dualControllers;
    bool ControllersFound = false;
    bool UsingHandControls;

    XROrigin xrOrigin;

    /// <summary>
    /// Awake(): Enforece singleton: if there is already an object tagged as Player, self-destruct
    /// </summary>
    private void Awake()
    {
        Debug.Log("PlayerCtrl:Awake()...");

        var ExistingPlayers = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("# of existing players found: " + ExistingPlayers.Length);
        if (ExistingPlayers.Length > 1)
        {
            Debug.Log("Destroying TEMP Player...");
            Destroy(gameObject);
        }

        screenFade = GetComponentInChildren<ScreenFade>();
        if (screenFade == null)
        {
            Debug.Log("WARNING:PlayerCtrl:Awake() --> Did not find a ScreenFade child object.");
        }

        xrOrigin = GetComponent<XROrigin>();
        GameManager.ExitingScene += OnExitingScene;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void Start()
    {
        Debug.Log("PlayerCtrl:Start()...");
        LocateControllers();
        UseHandControls(false);
        //FadeIn();
    }

    public void UseHandControls(bool val)
    {
        if (!ControllersFound) LocateControllers();

        foreach (DualController dc in dualControllers)
        {
            dc.UseHandController(val);
        }

        UsingHandControls = val;
    }

    
    
    private void Update()
    {

        //toggle between hand and device control visuals
        if (Input.GetKeyDown(KeyCode.X))
        {
            UseHandControls(!UsingHandControls);
        }
    
    }


    /// <summary>
    /// Helper function to fade out (fade to black) the display.
    /// Note that we have to fade in the primary quad to create a
    /// black-out of the display.
    /// </summary>
    public void FadeOut(bool instant = false)
    {
        Debug.Log("PlayerCtrl:FadeOut()...");
        screenFade.FadeIn(instant);
    }

    /// <summary>
    /// Helper function to fade in (fade from black) the display.
    /// Note that we have to fade out the primar quad to create a
    /// fade-in of the display.
    /// </summary>
    public void FadeIn(bool instant = false)
    {
        Debug.Log("PlayerCtrl:FadeIn()...");
        screenFade.FadeOut(instant);
    }


    void LocateControllers()
    {
        //if we've already found both controllers, do nothing
        if (ControllersFound) return;

        dualControllers = GetComponentsInChildren<DualController>(true);
        Debug.Log("PlayerCtrl: found " + dualControllers.Length + " DualController.");

        ControllersFound = dualControllers.Length == 2;
    }


    /// <summary>
    /// This functionn teleports the player to the specified position and
    /// orientation (local euler angles)
    /// </summary>
    /// <param name="position">Position to teleport to</param>
    /// <param name="localEulers">Orientation at teleport point as localEuler angles.</param>
    public void TelePort(Vector3 position, Vector3 localEulers)
    {
        //CharacterController characterController = GetComponent<CharacterController>();
        //characterController.enabled = false;
        transform.position = position;
        transform.localEulerAngles = localEulers;
        Physics.SyncTransforms();
        //characterController.enabled = true;

    }

    public void TelePort(Transform t)
    {
        Vector3 camOffset = new Vector3(0f, xrOrigin.CameraYOffset, 0f);
        xrOrigin.MoveCameraToWorldLocation(t.position + camOffset);
        bool val = xrOrigin.MatchOriginUpCameraForward(t.up, t.forward);
        //bool val = xrOrigin.MatchOriginUpOriginForward(t.up, t.forward);
        if (!val)
        {
            Debug.Log("PlayerCtrl: TelePort: MatchOriginUpOriginForward() failed.");
        }
        //Physics.SyncTransforms();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="clr"></param>
    public void SetHandColor(Color clr)
    {
        magicColorUpdates = GetComponentsInChildren<MagicColorUpdate>(true);
        Debug.Log("Found " + magicColorUpdates.Length + " MagicColorUpdate components.");

        foreach (var magicColorUpdate in magicColorUpdates)
        {
            magicColorUpdate.MagicUpdate(clr);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("PlayerCtrl triggered by " + other.tag);
        if (other.CompareTag("MMM"))
        {
            //Debug.Log("changing colors...");

            //Get the color of the MMM that hit our mouth
            var renderer = other.GetComponent<Renderer>();
            var clr = renderer.material.color;

            //set this as the target color for our hands
            SetHandColor(clr);
            Destroy(renderer.transform.parent.gameObject, 0.5f);
        }
    }



    private void OnDestroy()
    {
        Debug.Log("ScreenFade: Unsubscribing to OnSelectCanvasQuad...");
        GameManager.ExitingScene -= OnExitingScene;
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        FadeOut();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FadeIn();
    }
}
