using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    // Make this object unique and global
    public static Manager Instance { get; private set; }

    // Variabls loaded from file system
    public LocaleManager localeManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            localeManager = new LocaleManager();
            localeManager.initialize();
        }
    }

    public void changeLocale(int key)
    {
        localeManager.changeLocale(key);
    }
}
