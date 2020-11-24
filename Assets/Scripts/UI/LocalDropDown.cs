using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalDropDown : MonoBehaviour
{
    public Dropdown dropDown;

    public void ChangeLocale()
    {
        Manager manager = Object.FindObjectOfType<Manager>();
        manager.changeLocale(dropDown.value);
    }

    void Start()
    {
        // Clear the default options
        dropDown.ClearOptions();

        // Get the string list of the new options
        Manager manager = Object.FindObjectOfType<Manager>();
        string[] localeStringArray = manager.localeManager.getAllLocales();

        // Convert the string array to option list
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        for (int i = 0; i < localeStringArray.Length; i++)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = localeStringArray[i];
            options.Add(optionData);
        }

        // Set the option list to the drop down list
        dropDown.options = options;
    }

    
}
