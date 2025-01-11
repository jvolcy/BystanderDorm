using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] bool triggerOnPlayerOnly = true;
    public UnityEvent onEnterTrigger;
    public UnityEvent onExitTrigger;

    // Start is called before the first frame update
    void Start()
    {
        //turn off the mesh renderer
        GetComponent<MeshRenderer>().enabled = false;
    }


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

    /// <summary>
    /// Helper function that prepends source file name and line number to
    /// messages that target the Unity console.  Replace Debug.Log() calls
    /// with calls to debug() to use this feature.
    /// </summary>
    /// <param name="msg">The msg to send to the console.</param>
    void debug(string msg)
    {
        var stacktrace = new System.Diagnostics.StackTrace(true);
        string currentFile = System.IO.Path.GetFileName(stacktrace.GetFrame(1).GetFileName());
        int currentLine = stacktrace.GetFrame(1).GetFileLineNumber();  //frame 1 = caller
        Debug.Log(currentFile + "[" + currentLine + "]: " + msg);
    }
}
