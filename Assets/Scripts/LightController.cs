using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float SkyboxIntensity = 1f;
    [SerializeField] Light[] HallLights;
    [SerializeField] Light[] RoomLights;
    [SerializeField] [Range(0f, 10f)] float HallLightIntensity = 1f;
    [SerializeField] [Range(0f, 10f)] float RoomLightIntensity = 1f;
    [SerializeField] bool DisableSkybox = false;

    // Start is called before the first frame update
    void Start()
    {
        if (DisableSkybox)
        RenderSettings.skybox = null;

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
        //{
            //RenderSettings.skybox = DisableSkybox ? null : SkyboxMaterial;
            RenderSettings.ambientIntensity = SkyboxIntensity;
            RenderSettings.reflectionIntensity = SkyboxIntensity;
            foreach (Light light in HallLights)
            {
                light.intensity = HallLightIntensity;
            }

            foreach (Light light in RoomLights)
            {
                light.intensity = RoomLightIntensity;
            }
        //}
    }


}
