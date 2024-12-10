using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSignLight : MonoBehaviour
{
    [SerializeField] bool On = true;
    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;
    [SerializeField] MeshRenderer lightMesh;

    // Start is called before the first frame update
    void Start()
    {

        //we are on by default.  If off is selected, turn off the light.
        if (On)
        {
            GetComponentInChildren<Light>().enabled = true;
            lightMesh.material = OnMaterial;
        }
        else
        {
            GetComponentInChildren<Light>().enabled = false;
            lightMesh.material = OffMaterial;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
