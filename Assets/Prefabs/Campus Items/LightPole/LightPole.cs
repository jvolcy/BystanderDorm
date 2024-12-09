using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPole : MonoBehaviour
{
    [SerializeField] bool On = true;
    [SerializeField] Material OnMaterial;
    [SerializeField] Material OffMaterial;
    [SerializeField] int MaterialIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        var ren = GetComponent<MeshRenderer>();
        var materialsList = new List<Material>();

        //Note: we have to replace the entire materials array.
        //The glass material in this model is at index 1.
        ren.GetMaterials(materialsList);

        //we are on by default.  If off is selected, turn off the light.
        if (On)
        {
            GetComponentInChildren<Light>().enabled = true;
            materialsList[MaterialIndex] = OnMaterial;
        }
        else
        {
            GetComponentInChildren<Light>().enabled = false;
            materialsList[MaterialIndex] = OffMaterial;
        }
        GetComponent<MeshRenderer>().SetMaterials(materialsList);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
