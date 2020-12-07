using System;
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
    public GameObject prefabedWordItem;
    public GameObject parent;
    public string pathTitle;
    // public GameObject wordPanel;

    // Start is called before the first frame update
    void Start()
    {
        
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
            foreach (Word word in words)
            {
                Word newWord = getTransformed(word);
                GameObject instanceWord = (GameObject)Instantiate(prefabedWordItem, wordPanel.GetComponent<WordPanelScript>().wordPanel.transform);
                instanceWord.GetComponent<Text>().text = newWord.ToString();
            }
        }
        catch (Exception e)
        {

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
        return word;
    }
}
