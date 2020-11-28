using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialPhonePickingScript : MonoBehaviour
{
    public GameObject prefabedItem, phonemeContent, selectedContent;
    public CreateRulePageSix parent;
    public Text parentIPA, parentLetters;
    public int thisIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populate(int index)
    {
        thisIndex = index;
        Phoneme[] phonemes;
        Manager manager = Object.FindObjectOfType<Manager>();

        // Get the consonant phone pool from the manager
        LanguageManager languageManager = manager.languageManager;


        // Get the phonemes form the language manager
        if (index == 0)
        {
            phonemes = new Phoneme[languageManager.consonantBoS.Length + languageManager.consonantEoS.Length];
            int i = 0;
            for (i = 0; i < languageManager.consonantBoS.Length; i++)
            {
                phonemes[i] = languageManager.consonantBoS[i];
            }

            for (int j = 0; j < languageManager.consonantEoS.Length; j++)
            {
                phonemes[i + j] = languageManager.consonantEoS[j];
            }
        }
        else
        {
            phonemes = new Phoneme[languageManager.vowelUS.Length + languageManager.vowelAS.Length];
            int i = 0;
            for (i = 0; i < languageManager.vowelUS.Length; i++)
            {
                phonemes[i] = languageManager.vowelUS[i];
            }

            for (int j = 0; j < languageManager.vowelAS.Length; j++)
            {
                phonemes[i + j] = languageManager.vowelAS[j];
            }
        }

        // Populate the content with the phoneme
        foreach (Phoneme p in phonemes)
        {
            if (p.preceding != null || p.successing != null)
            {
                continue;
            }
            GameObject newConsonant = (GameObject)Instantiate(prefabedItem, phonemeContent.transform);
            newConsonant.transform.GetComponent<UneditibleItemScript>().IPA.text = p.getIPA();
            newConsonant.transform.GetComponent<UneditibleItemScript>().letters.text = p.letters;
            newConsonant.transform.GetComponent<UneditibleItemScript>().phoneme = p;
        }
    }

    public void onSaveButtonPressed()
    {
        if (selectedContent.transform.childCount > 0)
        {
            Phoneme[] selectedPhoneme = new Phoneme[selectedContent.transform.childCount];
            string totalIPA = "", totalLetters = "";

            selectedPhoneme[0] = new Phoneme();
            selectedPhoneme[0].addPhone(selectedContent.transform.GetChild(0).GetComponent<PickedPhoneItem>().phoneme);
            totalIPA += selectedPhoneme[0].getIPA();
            totalLetters += selectedPhoneme[0].letters;

            for (int i = 1; i < selectedContent.transform.childCount; i++)
            {
                selectedPhoneme[i] = new Phoneme();
                selectedPhoneme[i].addPhone(selectedContent.transform.GetChild(i).GetComponent<PickedPhoneItem>().phoneme);
                totalIPA += ", " + selectedPhoneme[i].getIPA();
                totalLetters += ", " + selectedPhoneme[i].letters;
            }

            if (thisIndex == 0)
            {
                parent.specialPickedConsonant = selectedPhoneme;
            }
            else
            {
                parent.specialPickedVowel = selectedPhoneme;
            }

            parentIPA.text = totalIPA;
            parentLetters.text = totalLetters;
        }
        Destroy(this.transform.gameObject);
    }

    public void onCancelButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }
}
