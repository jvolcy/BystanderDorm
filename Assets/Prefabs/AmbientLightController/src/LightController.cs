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
    public bool UpdateInRealTime = false;
    public Color AmbientLightColor = Color.black;

    /* ======================================================================
     * Start is called before the first frame update
     ====================================================================== */
    //void Start()
    //{
        /*
        if (DisableSkybox)
        RenderSettings.skybox = null;

        SetLightIntensities();
        */
    //}

    /* ======================================================================
     * Update is called once per frame
     ====================================================================== */
    void Update()
    {
        if (UpdateInRealTime)
        {
            RenderSettings.ambientLight = AmbientLightColor;
        }
    }

    /* ======================================================================
    * 
    ====================================================================== */
    /*
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


    */
    //----------  ----------

    /* ======================================================================
     * 
     ====================================================================== */
}

