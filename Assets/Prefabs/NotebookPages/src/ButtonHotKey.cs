using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHotKey : MonoBehaviour
{

    /* ======================================================================
     * Update is called once per frame
    ====================================================================== */

    [SerializeField] KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
