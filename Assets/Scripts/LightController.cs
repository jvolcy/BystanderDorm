using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ======================================================================
 * The LightController controls the lighting intensities in the hall, 
 * the room and the ambient light.  Each of these can be individually set.
 * To avoid busy setting these intensities in Update(), we create
 * shadow registers to keep track of when the intensity values change.
 ====================================================================== */
public class LightController : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float SkyboxIntensity = 0f;
    [SerializeField] Light[] HallLights;
    [SerializeField] Light[] RoomLights;
    [SerializeField] [Range(0f, 10f)] float HallLightIntensity = 1f;
    [SerializeField] [Range(0f, 10f)] float RoomLightIntensity = 1f;
    [SerializeField] bool DisableSkybox = false;

    //---------- Shadow registers ----------
    float oldSkyboxIntensity;
    float oldHallLightIntensity;
    float oldRoomLightIntensity;


    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    void Start()
    {
        if (DisableSkybox)
        RenderSettings.skybox = null;

        SetLightIntensities();

    }

    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */
    void Update()
    {
        if (oldHallLightIntensity != HallLightIntensity) { SetLightIntensities(); }
        else if (oldRoomLightIntensity != RoomLightIntensity) { SetLightIntensities(); }
        else if (oldSkyboxIntensity != SkyboxIntensity) { SetLightIntensities(); }
    }

    /* ======================================================================
    * 
    ====================================================================== */
    void SetLightIntensities()
    {
        //RenderSettings.ambientIntensity = SkyboxIntensity;
        byte clr = (byte)(168 * SkyboxIntensity);
        RenderSettings.ambientLight = new Color32(clr, clr, clr, 255);
        //RenderSettings.reflectionIntensity = SkyboxIntensity;
        foreach (Light light in HallLights)
        {
            light.intensity = HallLightIntensity;
        }

        foreach (Light light in RoomLights)
        {
            light.intensity = RoomLightIntensity;
        }

        //---------- Update our shadow registers ----------
        oldSkyboxIntensity = SkyboxIntensity;
        oldHallLightIntensity = HallLightIntensity;
        oldRoomLightIntensity = RoomLightIntensity;
    }



    //----------  ----------

    /* ======================================================================
     * 
     ====================================================================== */
}

