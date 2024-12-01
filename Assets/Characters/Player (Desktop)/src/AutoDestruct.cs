using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{

    /* ======================================================================
     * If there is already an object tagged as Player, self-destruct
     ====================================================================== */
    private void Awake()
    {
        var ExistingPlayers = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("# of existing players found: " + ExistingPlayers.Length);
        if (ExistingPlayers.Length > 1)
        {
            Debug.Log("Destroying TEMP Player...");
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /* ======================================================================
     * 
     ====================================================================== */
}
