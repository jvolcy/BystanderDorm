using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //turn off the mesh renderer
        GetComponent<MeshRenderer>().enabled = false;
    }


}
