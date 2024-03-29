﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffixMakerScript : MonoBehaviour
{
    public GameObject vowelContent, consonantContent;
    public Phoneme affixPhoneme, parentPhoneme;
    public bool isPrefix;
    public GameObject prefabedItem;
    public Text thisIPA, thisLetters, parentIPA, parentLetter;

    // Start is called before the first frame update
    void Start()
    {
        Manager manager = Object.FindObjectOfType<Manager>();

        // Get the consonant phone pool from the manager
        LanguageManager languageManager = manager.languageManager;

        Phoneme[][] allConsonant = new Phoneme[4][];
        allConsonant[0] = languageManager.consonantBoS;
        allConsonant[1] = languageManager.consonantBoW;
        allConsonant[2] = languageManager.consonantEoS;
        allConsonant[3] = languageManager.consonantEoW;

        foreach (Phoneme[] ps in allConsonant) {
            foreach (Phoneme p in ps)
            {
                if (p.preceding != null || p.successing != null)
                {
                    continue;
                }
                GameObject newConsonant = (GameObject)Instantiate(prefabedItem, consonantContent.transform);
                newConsonant.transform.GetComponent<UneditibleItemScript>().IPA.text = p.getIPA();
                newConsonant.transform.GetComponent<UneditibleItemScript>().letters.text = p.letters;
                newConsonant.transform.GetComponent<UneditibleItemScript>().phoneme = p;
            }
        }

        Phoneme[][] allVowel = new Phoneme[2][];
        allVowel[0] = languageManager.vowelAS;
        allVowel[1] = languageManager.vowelUS;
        foreach (Phoneme[] ps in allVowel)
        {
            foreach (Phoneme p in ps)
            {
                if (p.preceding != null || p.successing != null)
                {
                    continue;
                }
                GameObject newConsonant = (GameObject)Instantiate(prefabedItem, vowelContent.transform);
                newConsonant.transform.GetComponent<UneditibleItemScript>().IPA.text = p.getIPA();
                newConsonant.transform.GetComponent<UneditibleItemScript>().letters.text = p.letters;
                newConsonant.transform.GetComponent<UneditibleItemScript>().phoneme = p;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void handleCheckButtonPressed()
    {
        if (affixPhoneme.phones.Length > 0)
        {
            parentIPA.text = thisIPA.text;
            parentLetter.text = thisLetters.text;
            parentPhoneme.clear();
            parentPhoneme.addPhone(affixPhoneme);
        }
        Destroy(this.gameObject);
    }

    public void handleResetButtonPressed()
    {
        thisIPA.text = "";
        thisLetters.text = "";
        affixPhoneme = new Phoneme();
    }
}
