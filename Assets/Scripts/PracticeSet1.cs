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
            transform.Rotate(0f, Random.Range(0f,90f), 0f);
            transform.position = new Vector3(Random.Range(-50f, 50f), 0f, Random.Range(-50f, 50f));
        }
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject obj = new();
            obj.transform.position = new Vector3(1.6299999952316285f,0.0f,4.0f);
            obj.transform.rotation = new Quaternion(-0.012117132544517517f, 0.1727437525987625f, 0.002125272061675787f, 0.9848899841308594f);
            //obj.transform.position = transform.position;
            //obj.transform.LookAt(target);

            GameManager.instance.Player.GetComponentInChildren<PlayerCtrl>().TelePort(obj.transform);
        }
        */
    }
}
