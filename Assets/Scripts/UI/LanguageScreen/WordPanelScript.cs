using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class WordPanelScript : MonoBehaviour
{

    public Word[] holdingWord;
    public GameObject parent, wordPanel;
    public Dropdown partOfSpeech, typeOfWord;

    // Start is called before the first frame update
    void Start()
    {
        
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

        holdingWord = null;
        this.gameObject.SetActive(false);
        while (wordPanel.transform.childCount > 0)
        {
            Transform tem = wordPanel.transform.GetChild(0);
            tem.parent = null;
            Destroy(tem.gameObject);
        }
        partOfSpeech = null;
        typeOfWord = null;
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
}
