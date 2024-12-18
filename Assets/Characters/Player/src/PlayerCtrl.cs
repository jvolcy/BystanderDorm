using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] bool DestroyIfDuplicate = true;

    //the component that updates the hand color
    MagicColorUpdate magicColorUpdate;

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
        var obj = FindObjectOfType<MagicColorUpdate>();
        magicColorUpdate = obj.GetComponent<MagicColorUpdate>();
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
        magicColorUpdate.MagicUpdate(clr);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PalyerCtrl triggered by " + other.name);
    }

}
