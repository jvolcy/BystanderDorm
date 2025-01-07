using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusNightSceneManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //put us in front of our room, facing the door
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(new Vector3(0f, 0.1f, -5f), new Vector3(0f, 180f, 0f));
        }
    }
}
