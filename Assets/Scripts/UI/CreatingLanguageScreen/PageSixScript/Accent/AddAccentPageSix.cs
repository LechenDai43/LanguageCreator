using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAccentPageSix : MonoBehaviour
{
    public Dropdown coundBack, accentType;
    public Text positionField, holder;
    // Start is called before the first frame update
    void Start()
    {
        coundBack.ClearOptions();
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        Dropdown.OptionData optionData = new Dropdown.OptionData();
        optionData.text = "Foreward";
        options.Add(optionData);
        optionData = new Dropdown.OptionData();
        optionData.text = "Backward";
        options.Add(optionData);
        coundBack.options = options;

        accentType.ClearOptions();
        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;
        // Contour, Level;
        List<string> contour = new List<string>(), level = new List<string>();
        options = new List<Dropdown.OptionData>();
        AccentPhone[] accents = languageManager.accents;
        foreach (AccentPhone ap in accents)
        {
            bool contourIn = false, levelIn = false;
            foreach (string str in contour)
            {
                if (str.Equals(ap.Contour))
                {
                    contourIn = true;
                    break;
                }
            }
            if (!contourIn)
            {
                contour.Add(ap.Contour);
            }
            foreach (string str in level)
            {
                if (str.Equals(ap.Level))
                {
                    levelIn = true;
                    break;
                }
            }
            if (!levelIn)
            {
                level.Add(ap.Level);
            }
            optionData = new Dropdown.OptionData();
            optionData.text = ap.IPA;
            options.Add(optionData);
        }
        foreach (string str in contour)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = str;
            options.Add(optionData);
        }
        foreach (string str in level)
        {
            optionData = new Dropdown.OptionData();
            optionData.text = str;
            options.Add(optionData);
        }
        optionData = new Dropdown.OptionData();
        optionData.text = "all";
        options.Add(optionData);
        accentType.options = options;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
