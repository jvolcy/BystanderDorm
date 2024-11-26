using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] bool triggerOnPlayerOnly = true;
    public UnityEvent onEnterTrigger;
    public UnityEvent onExitTrigger;
    //public UnityEvent<string, string> onEnterTriggerWithID;
    //public UnityEvent<string, string> onExitTriggerWithID;

    // Start is called before the first frame update
    void Start()
    {
        //turn off the mesh renderer
        GetComponent<MeshRenderer>().enabled = false;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        //are we only triggering on objects with the "Player" tag?
        if (triggerOnPlayerOnly && !other.CompareTag("Player")) { return; }

        if (onEnterTrigger != null) { onEnterTrigger.Invoke(); }
        //if (onEnterTriggerWithID != null) { onEnterTriggerWithID.Invoke(name, other.name); }
    }

    private void OnTriggerExit(Collider other)
    {
        //are we only triggering on objects with the "Player" tag?
        if (triggerOnPlayerOnly && !other.CompareTag("Player")) { return; }

        if (onExitTrigger != null) { onExitTrigger.Invoke(); }
        //if (onExitTriggerWithID != null) { onExitTriggerWithID.Invoke(name, other.name); }
    }
}
