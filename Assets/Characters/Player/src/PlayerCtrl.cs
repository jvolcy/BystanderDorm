using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] bool DestroyIfDuplicate = true;

    /* ======================================================================
     * If there is already an object tagged as Player, self-destruct
     ====================================================================== */
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


    /* ======================================================================
     * This functionn teleports the player to the specified position and
     * orientation (local euler angles)
     ====================================================================== */
    public void TelePort(Vector3 position, Vector3 localEulers)
    {
        //CharacterController characterController = GetComponent<CharacterController>();
        //characterController.enabled = false;
        transform.position = position;
        transform.localEulerAngles = localEulers;
        Physics.SyncTransforms();
        //characterController.enabled = true;

    }


    /* ======================================================================
     * 
     ====================================================================== */
}
