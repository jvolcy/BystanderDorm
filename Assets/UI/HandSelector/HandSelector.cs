using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandSelector : MonoBehaviour
{
    [SerializeField] HandSelectButton[] handSelectButtons;

    int _selectedButton;

    int selectedButton
    {
        get { return _selectedButton; }
        set
        {
            _selectedButton = Mathf.Clamp(value, 0, handSelectButtons.Length-1);

            //unselect all buttons
            foreach (var button in handSelectButtons)
            {
                button.Selected = false;
            }

            handSelectButtons[_selectedButton].Selected = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //select the 1st button
        selectedButton = 0;
    }

    
    // Update is called once per frame
    void Update()
    {
        //********** For Debugging only **********
        if (Input.GetKeyDown(KeyCode.RightArrow))
        { selectedButton++; }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        { selectedButton--; }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        { selectedButton -= 3; }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        { selectedButton += 3; }
    }



    public void SelectButton(int buttonNumber)
    {
        selectedButton = buttonNumber;
        Debug.Log("HandSelector.cs: selected hand color is " + handSelectButtons[buttonNumber].GetComponent<Image>().color);
    }
}
