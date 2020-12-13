using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class WordPanelScript : MonoBehaviour
{

    public Word[] holdingWord;
    public GameObject parent, wordPanel;
    public Dropdown partOfSpeech, typeOfWord;
    public GameObject saveButton, fileButton;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR
            fileButton.SetActive(true);
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveWords()
    {
        if (holdingWord == null || holdingWord.Length <= 0)
        {
            return;
        }
        LanguageScreenManager lsm = parent.GetComponent<LanguageScreenManager>();
        int type = typeOfWord.value;
        if (partOfSpeech.value == 0)
        {
            lsm.others[type].addWords(holdingWord);
        }
        else if (partOfSpeech.value == 1)
        {
            lsm.verbs[type].addWords(holdingWord);
        }
        else if (partOfSpeech.value == 2)
        {
            lsm.nouns[type].addWords(holdingWord);
        }
        else
        {
            lsm.adjectives[type].addWords(holdingWord);
        }
        lsm.saveVocabulary();

        closePanel();
    }

    public void closePanel()
    {
        this.gameObject.SetActive(false);
        while (wordPanel.transform.childCount > 0)
        {
            Transform tem = wordPanel.transform.GetChild(0);
            tem.parent = null;
            Destroy(tem.gameObject);
        }
        holdingWord = null;
        partOfSpeech = null;
        typeOfWord = null;
    }

    // [MenuItem("Examples/Save Texture to file")]

    public void saveAsFile()
    {
        #if UNITY_EDITOR
        Texture2D texture = new Texture2D(300, 300);

        if (texture == null)
        {
            UnityEditor.EditorUtility.DisplayDialog(
                "Select Texture",
                "You Must Select a Texture first!",
                "Ok");
            return;
        }

        string path = UnityEditor.EditorUtility.SaveFilePanel(
            "Save Current Words",
            "",
            "vocabulary.csv",
            "csv");

        if (path.Length != 0)
        {
            string value = "Words, Comment,\r\n";
            for (int i = 0; i < wordPanel.transform.childCount; i++)
            {
                Text aText = wordPanel.transform.GetChild(i).GetComponent<Text>();
                value += aText.text + ", ,\r\n";
            }

            System.IO.File.WriteAllText(path, value);
        }
        // Debug.Log(Application.dataPath);
        // string winPath = Application.dataPath.Replace("/", "\\");
        
        
        #endif

    }
}
