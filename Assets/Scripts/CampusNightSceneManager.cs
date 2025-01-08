using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusNightSceneManager : MonoBehaviour
{
    [SerializeField] Transform BellesHallEntry;
    [SerializeField] Transform MelaninHallEntry;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //put us in front of our room, facing the door
            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(BellesHallEntry);
        }
    }
}
