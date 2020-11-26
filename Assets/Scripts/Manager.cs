using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour//, //Singleton<Manager>
{
    // Make this object unique and global
    public static Manager Instance { get; private set; }

    // Variabls loaded from file system
    public LocaleManager localeManager;
    public PhoneManager phoneManager;
    public LanguageManager languageManager;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else if (Instance == null)
        {
            Instance = this;
            localeManager = new LocaleManager();
            localeManager.initialize();
            phoneManager = new PhoneManager();
            phoneManager.loadIn(false);
            languageManager = new LanguageManager();
            DontDestroyOnLoad(this.gameObject);
        }
        
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void changeLocale(int key)
    {
        localeManager.changeLocale(key);
    }


}
