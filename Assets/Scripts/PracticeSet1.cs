using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeSet1 : MonoBehaviour
{

    [SerializeField] GameObject borderObjectPrefab;
    [SerializeField] float borderRadius = 30f;
    [SerializeField] int borderDegInc = 10;

    // Start is called before the first frame update
    void Start()
    {
        BuildScene(borderRadius, borderDegInc) ;
    }

    // Update is called once per frame
    void Update()
    {

        

        
    }


    void BuildScene(float radius = 30, int degInc = 10)
    {
        GameObject obj;
        for (int angle = 0; angle < 360; angle+=degInc)
        {
            obj = Instantiate(borderObjectPrefab);
            obj.transform.position = Vector3.zero;
            obj.transform.Rotate(0, angle, 0);
            obj.transform.Translate(obj.transform.forward * radius);
        }
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

