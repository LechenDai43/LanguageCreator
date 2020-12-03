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

            Vocabulary vcb = null;
            try
            {
                string consonantPath = Application.dataPath + "/Files/Customization/" + currentLanguage.Name + ".vocabulary";
                StreamReader sr;
                sr = new StreamReader(consonantPath);
                string content = sr.ReadToEnd();
                vcb = JsonUtility.FromJson<Vocabulary>(content);
            }
            catch(Exception e)
            {
                vcb = new Vocabulary();
                vcb.Name = currentLanguage.Name;
            }

            scrollView.SetActive(true);
            vcb.addWords(partOfSpeech.value, words);

            string langaugeData = JsonUtility.ToJson(vcb);
            System.IO.File.WriteAllText(Application.dataPath + "/Files/Customization/" + currentLanguage.Name + ".vocabulary", langaugeData);

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
}
