using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GenerateWord : MonoBehaviour
{
    public Dropdown partOfSpeech, typeOfWord;
    public Text inputNum;
    public GameObject scrollView, wordPanel;
    public int dropDownValuePOS;
    public GameObject prefabedWordItem;
    public GameObject parent;
    public string pathTitle;

    // Start is called before the first frame update
    void Start()
    {
        changePartOfSpeech();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("c"))
        {
            scrollView.SetActive(false);
            while (wordPanel.transform.childCount > 0)
            {
                Transform tem = wordPanel.transform.GetChild(0);
                tem.parent = null;
                Destroy(tem.gameObject);
            }
        }
    }

    public void createVocabulary ()
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
            foreach (Word word in words)
            {
                GameObject instanceWord = (GameObject)Instantiate(prefabedWordItem, wordPanel.transform);
                instanceWord.GetComponent<Text>().text = word.ToString();
            }

            LanguageScreenManager lsm = parent.GetComponent<LanguageScreenManager>();
            if (partOfSpeech.value == 0)
            {
                lsm.others[type].addWords(words);
            }
            else if (partOfSpeech.value == 1)
            {
                lsm.verbs[type].addWords(words);
            }
            else if (partOfSpeech.value == 2)
            {
                lsm.nouns[type].addWords(words);
            }
            else
            {
                lsm.adjectives[type].addWords(words);
            }
        }
        catch(Exception e)
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
                pathTitle = "O";
                break;
            case 1:
                list = currentLanguage.Verbs;
                pathTitle = "V";
                break;
            case 2:
                list = currentLanguage.Nouns;
                pathTitle = "N";
                break;
            case 3:
                list = currentLanguage.Adjectives;
                pathTitle = "A";
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
}
