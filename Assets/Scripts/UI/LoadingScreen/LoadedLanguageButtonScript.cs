using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization;

public class LoadedLanguageButtonScript : MonoBehaviour
{
    public Text displayedText;
    public string languagePath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickThisButton()
    {
        if (languagePath != null) {
            Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
            StreamReader sr = new StreamReader(languagePath);
            string content = sr.ReadToEnd();
            LanguageFamily language = JsonUtility.FromJson<LanguageFamily>(content);
            sr.Close();
            manager.currentLanguage = language;
            SceneManager.LoadScene("LanguageScreen");
        }
    }
}
