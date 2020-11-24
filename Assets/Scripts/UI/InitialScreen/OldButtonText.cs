using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldButtonText : MonoBehaviour
{
    public Text text;
    private Manager manager;
    private int locale;

    // Start is called before the first frame update
    void Start()
    {
        manager = Object.FindObjectOfType<Manager>();
        locale = manager.localeManager.getCurrentLocale();
    }

    // Update is called once per frame
    void Update()
    {
        if (locale != manager.localeManager.localePointer)
        {
            locale = manager.localeManager.getCurrentLocale();
            updateText();
        }

    }

    void updateText()
    {
        string textContent = manager.localeManager.getInitialScreenLocale().OldButton;
        text.text = textContent;
    }
}
