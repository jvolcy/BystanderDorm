using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] bool DestroyIfDuplicate = true;
    public Color32 HoverHighlightColor = new Color32(0x3C, 0x23, 0x3C, 0xFF);

    /// <summary>
    /// Awake(): Enforece singleton: if there is already an object tagged as Player, self-destruct
    /// </summary>
    private void Awake()
    {
        FindObjectOfType<GameManager>().EnableDisableXrAndDesktopObjs();

        //if (DestroyIfDuplicate)
        //{
            var ExistingPlayers = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log("# of existing players found: " + ExistingPlayers.Length);
            if (ExistingPlayers.Length > 1)
            {
                Debug.Log("Destroying TEMP Player...");
                Destroy(gameObject);
            }
        //}
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

    /// <summary>
    /// Event handler for Hover Enter.  Here, we assume that the interactable
    /// object we are hovering over has a Render component on the same object
    /// as the XRInteractable component.  We will replace the GO's material
    /// with an exact copy with emissions turned on.
    /// GO's material.
    /// </summary>
    /// <param name="args"></param>
    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        //make a copy of the target GO's material
        Renderer renderer = args.interactableObject.transform.GetComponentInChildren<Renderer>();

        if (renderer == null)
        {
            Debug.Log("PlayerCtrl.cs - OnHoverEnter(): No Renderer found on object " + args.interactableObject.transform.name);
            return;
        }

        Debug.Log("PlayerCtrl.cs - OnHoverEnter() " + args.interactableObject.transform.name);

        Material mat = new(renderer.material);

        //turn on the material's emission and set its color
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", HoverHighlightColor);

        //assign the newly  created material to the GO
        renderer.material = mat;
    }

    /// <summary>
    /// Event handler for Hover Exit.  Here, we simply turn off the emission
    /// on the target GO's Renderer.
    /// </summary>
    /// <param name="args"></param>
    public void OnHoverExit(HoverExitEventArgs args)
    {
        Debug.Log("PlayerCtrl.cs - OnHoverExit() " + args.interactableObject.transform.name);

        //turn off the GO's Renderer's material's emission
        args.interactableObject.transform.GetComponentInChildren<Renderer>().material.DisableKeyword("_EMISSION");
    }
}
