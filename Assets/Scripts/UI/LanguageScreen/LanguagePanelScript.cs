﻿using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class LanguagePanelScript : MonoBehaviour
{
    public Dropdown partOfSpeech, typeOfWord;
    public Text inputNum;
    public GameObject subdivisionView, wordPanel;
    public int dropDownValuePOS;

    public LanguageScreenManager languageScreenManager;

    public GameObject sublanguageButtonPrefab, createSubdivisionPrefab;
    public GameObject prefabedWordItem;

    public GameObject parent; // populate on creating
    public SoundChange changingRule; // populate on creating

    // Start is called before the first frame update
    void Start()
    {
        changePartOfSpeech();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createVocabulary()
    {
        try
        {
            int num = int.Parse(inputNum.text);
            int type = typeOfWord.value;
            Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
            LanguageFamily currentLanguage = manager.currentLanguage;
            Word[] words = null;

            if (partOfSpeech.value == 0)
            {
                words = currentLanguage.generateGenralWord(type, num);
            }
            else if (partOfSpeech.value == 1)
            {
                words = currentLanguage.generateVerbWord(type, num);
            }
            else if (partOfSpeech.value == 2)
            {
                words = currentLanguage.generateNounWord(type, num);
            }
            else
            {
                words = currentLanguage.generateAdjectiveWord(type, num);
            }

            wordPanel.GetComponent<WordPanelScript>().holdingWord = words;
            wordPanel.GetComponent<WordPanelScript>().partOfSpeech = partOfSpeech;
            wordPanel.GetComponent<WordPanelScript>().typeOfWord = typeOfWord;
            wordPanel.GetComponent<WordPanelScript>().saveButton.SetActive(true);
            wordPanel.SetActive(true);

            foreach (Word word in words)
            {
                Word newWord = getTransformed(word);
                GameObject instanceWord = (GameObject)Instantiate(prefabedWordItem, wordPanel.GetComponent<WordPanelScript>().wordPanel.transform);
                instanceWord.GetComponent<Text>().text = newWord.ToString();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void changePartOfSpeech()
    {
        // LanguageFamily currentLanguage;
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
        LanguageFamily currentLanguage = manager.currentLanguage;
        dropDownValuePOS = partOfSpeech.value;
        Morphome[] list = null;
        switch (dropDownValuePOS)
        {
            case 0:
                list = currentLanguage.Generals;
                break;
            case 1:
                list = currentLanguage.Verbs;
                break;
            case 2:
                list = currentLanguage.Nouns;
                break;
            case 3:
                list = currentLanguage.Adjectives;
                break;
        }

        reformatType(list);
    }

    public void createSubdivision()
    {
        GameObject createSubPanel = (GameObject)Instantiate(createSubdivisionPrefab, languageScreenManager.transform);
        AddSandhiPanelScript istantiatedScript = createSubPanel.GetComponent<AddSandhiPanelScript>();
        istantiatedScript.parentScript = this;
        // instanceWord.GetComponent<Text>().text = newWord.ToString();
    }

    public void showOldWord()
    {
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
        LanguageFamily currentLanguage = manager.currentLanguage;
        Word[] words = null;
        int num = typeOfWord.value;
        if (partOfSpeech.value == 0)
        {
            words = languageScreenManager.others[num].Word;
        }
        else if (partOfSpeech.value == 1)
        {
            words = languageScreenManager.verbs[num].Word;
        }
        else if (partOfSpeech.value == 2)
        {
            words = languageScreenManager.nouns[num].Word;
        }
        else
        {
            words = languageScreenManager.adjectives[num].Word;
        }

        foreach (Word word in words)
        {
            Word newWord = getTransformed(word);
            GameObject instanceWord = (GameObject)Instantiate(prefabedWordItem, wordPanel.GetComponent<WordPanelScript>().wordPanel.transform);
            instanceWord.GetComponent<Text>().text = newWord.ToString();
        }
        wordPanel.GetComponent<WordPanelScript>().saveButton.SetActive(false);
        wordPanel.SetActive(true);
    }

    public void backUpperDirectory()
    {
        parent.SetActive(true);
        Destroy(this.gameObject);
    }

    public void openSubdivision()
    {

    }

    public void populateSubLanguageButton()
    {
        if (changingRule != null)
        {
            StreamReader sr;
            for (int i = 0; i < changingRule.Branches.Length; i++)
            {
                string path = changingRule.Branches[i];
                sr = new StreamReader(Application.dataPath + path);
                string content = sr.ReadToEnd();
                SoundChange oneSub = JsonUtility.FromJson<SoundChange>(content);

                GameObject instanceButton = (GameObject)Instantiate(sublanguageButtonPrefab, subdivisionView.transform);
                SubLanguageButton buttonScript = instanceButton.GetComponent<SubLanguageButton>();
                buttonScript.parentScript = this;
                buttonScript.soundChange = oneSub;
                buttonScript.buttonText.text = oneSub.Name;
                sr.Close();
            }
        }
        else
        {
            Debug.Log("Sound change did not find");
        }
    }

    private void reformatType(Morphome[] list)
    {
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach (Morphome mp in list)
        {
            Dropdown.OptionData od = new Dropdown.OptionData();
            od.text = mp.getDescription();
            options.Add(od);
        }
        typeOfWord.options = options;
    }

    private Word getTransformed(Word word)
    {
        Word result = Word.deepCopy(word);
        if (parent != null)
        {
            LanguagePanelScript lps = parent.GetComponent<LanguagePanelScript>();
            if (lps != null)
            {
                result = lps.getTransformed(result);
            }
        }

        // Debug.Log(changingRule != null);
        if (changingRule != null)
        {
            result = changingRule.change(result);
        }
        return result;
    }


}
