using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable] struct MyStruct { public string item1; public int item2; }
    [SerializeField] MyStruct myStruct = new MyStruct{ item1="default string", item2=1234};

    GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("X");


        }
    }

    /* ======================================================================
     * This signal handler is called immediately after the lights are
     * dimmed in the room.  Here, we will close the door (if it is open)
     * and reposition the player so that she is facing the clock.
    ====================================================================== */
    public void PrepareRoomScene()
    {
        Debug.Log("signaled");
        CharacterController characterController = Player.GetComponent<CharacterController>();
        characterController.enabled = false;
        Player.transform.position = new Vector3(11.4f, 9.25f, -4.20f);
        Player.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        characterController.enabled = true;

    }
}

/* ======================================================================
 * 
====================================================================== */