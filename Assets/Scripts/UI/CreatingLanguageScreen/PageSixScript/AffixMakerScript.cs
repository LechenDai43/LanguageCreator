using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffixMakerScript : MonoBehaviour
{
    public GameObject vowelContent, consonantContent;
    public Phoneme affixPhoneme;
    public bool isPrefix;
    public GameObject prefabedItem;
    public Text thisIPA, thisLetters, parentIPA, parentLetter;

    // Start is called before the first frame update
    void Start()
    {
        GameObject newItem;
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
            }
        }

        Phoneme[][] allVowel = new Phoneme[2][];
        allVowel[0] = languageManager.vowelAS;
        allVowel[1] = languageManager.vowelUS;
        Debug.Log(allVowel[0].Length);
        Debug.Log(allVowel[1].Length);
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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleCheckButtonPressed()
    {

    }

    public void handleResetButtonPressed()
    {

    }
}
