using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject buttonContent;
    public GameObject prefabedButton;

    // Start is called before the first frame update
    void Start()
    {
        string directoryPath = Application.dataPath + "/Files/Customization/Languages";
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        FileInfo[] fileInfos = dir.GetFiles("*.languageFamily");
        foreach (FileInfo ff in fileInfos)
        {
            try
            {
                string filePath = ff.FullName;
                string name = ff.Name.Replace(".languageFamily", "");

                GameObject instanceButton = (GameObject)Instantiate(prefabedButton, buttonContent.transform);
                LoadedLanguageButtonScript buttonScript = instanceButton.GetComponent<LoadedLanguageButtonScript>();
                buttonScript.displayedText.text = name;
                buttonScript.languagePath = filePath;

            }
            catch (Exception e)
            {

            }
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backToHome()
    {

        SceneManager.LoadScene("InitialScreen");
    }
}
