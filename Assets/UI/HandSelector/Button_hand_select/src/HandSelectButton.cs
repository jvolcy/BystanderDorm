using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSelectButton : MonoBehaviour
{
    [SerializeField] Image SelectorImage;

    public bool Selected
    {
        set {
            Color color = SelectorImage.color;
            color.a = value ? 1.0f : 0.0f;
            SelectorImage.color = color;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Selected = false;
    }


}
