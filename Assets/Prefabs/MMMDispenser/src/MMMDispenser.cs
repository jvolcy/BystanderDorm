using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMMDispenser : MonoBehaviour
{
    [SerializeField] GameObject MMMPrefab;
    [SerializeField] Transform DispensePoint;

    Animator animator;

    public void Dispense()
    {
        var obj = Instantiate(MMMPrefab);
        obj.transform.position = DispensePoint.position;
        obj.transform.rotation = DispensePoint.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Dispense();
    }
}
