using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeSet1 : MonoBehaviour
{

    public Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(0, 30, 0);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject obj = new();
            obj.transform.position = transform.position;
            obj.transform.LookAt(target);

            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(obj.transform);
        }
    }
}
