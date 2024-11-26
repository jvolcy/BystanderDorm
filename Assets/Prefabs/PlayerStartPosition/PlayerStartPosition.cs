using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPosition : MonoBehaviour
{
    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {
        //find the Player object.  There should be one under XR
        //and one under DESKTOP, but only one of these will be active.
        var Player = GameObject.FindGameObjectWithTag("Player");

        Player.transform.position = transform.position;
        Player.transform.rotation = transform.rotation;
    }

}


/* ======================================================================
 * 
 ====================================================================== */