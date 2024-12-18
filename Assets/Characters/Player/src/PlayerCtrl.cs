using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] bool DestroyIfDuplicate = true;

    //the component that updates the hand color
    MagicColorUpdate[] magicColorUpdates;

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


    private void Start()
    {
        //var obj =  FindObjectOfType<MagicColorUpdate>();

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

}
