using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TeleportPadController : MonoBehaviour
{
    public string DefaultTeleportPadText = "X";

    //transform of the GO tagged as "Player"
    Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        //find the "Player" GO and store a reference to its transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;

        //set the text for the teleport pad
        TeleportPadText = DefaultTeleportPadText;
    }

    // Update is called once per frame
    void Update()
    {
        //get the coordinates of the player's feet
        Vector3 playerPos = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);

        //force the teleportText to point to the player's feet.
        transform.LookAt(playerPos);
    }

    public string TeleportPadText
    {
        set 
        {
            //set the text for the teleport pad
            TextMeshPro txt = GetComponentInChildren<TextMeshPro>();
            txt.text = value;
        }
        get 
        {
            //get the text for the teleport pad
            TextMeshPro txt = GetComponentInChildren<TextMeshProUGUI>().GetComponent<TextMeshPro>();
            return txt.text; 
        }
    }
}


/* ======================================================================
 * 
 ====================================================================== */