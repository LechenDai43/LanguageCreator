using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRulePageSix : MonoBehaviour
{
    // Meta information
    private int syllableNum = 0, accentNum = 0;

    // Information about who created this panel
    public GameObject fromThisPanel;
    public int fromThisIndex;

    // GameObject attached to this panel
    public Text numOfSylInput, numOfAccInput;
    public GameObject accentPanel;
    public Toggle specialAffixToggle, specialPhoneToggle, arabicFormatToggle;
    public GameObject specialAffixPanel, specialPhonePanel, arabicFormatPanel;

    // GameObject of the sub panels
    public Toggle specialPrefixToggle, specialSuffixToggle;
    public Text specialPrefixIPA, specialSuffixIPA, specialPrefixLetters, specialSuffixLetters;
    public Toggle specialVowelToggle, specialConsonantToggle;
    public Text specialVowelIPA, specialConsonantIPA, specialVowelLetters, specialConsonantLetters;
    public Toggle useSemivoweledConsonant, useClusteredConsonant;
    public GameObject arabicFormatContent;
    public Phoneme suffixPhoneme, prefixPhoneme;
    public Phoneme[] vowelsPhonemeForArabic;

    // Prefabed GameObject
    // TODO...
    // List all the prefabed object needed for create rule
    public GameObject prefabedOneAccent, prefabedOneVowel;
    public GameObject prefabedAffixMaker;

    // Start is called before the first frame update
    void Start()
    {
        suffixPhoneme = new Phoneme();
        prefixPhoneme = new Phoneme();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Save the newly created rule
    public void onSaveButtonPressed()
    {

    }

    // Do not save this rule and exit the rule edit panel
    public void onDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    // Display the corresponding panel as the toggle in formatter changed
    public void onFormatterToggleChanged(int toggleIndex)
    {
        if (toggleIndex < 2)
        {
            if (specialAffixToggle.isOn || specialPhoneToggle.isOn)
            {
                arabicFormatPanel.SetActive(false);
                arabicFormatToggle.isOn = false;
            }

            if (specialPhoneToggle.isOn)
            {
                specialPhonePanel.SetActive(true);
            }

            if (specialAffixToggle.isOn)
            {
                specialAffixPanel.SetActive(true);
            }
        }
        else
        {
            if (arabicFormatToggle.isOn)
            {
                arabicFormatPanel.SetActive(true);
                specialPhonePanel.SetActive(false);
                specialAffixPanel.SetActive(false);
                specialAffixToggle.isOn = false;
                specialPhoneToggle.isOn = false;
                changeNumberOfOneVewol();
            }
        }
    }

    // Change the number of valid vowel holder and accent as the number of syllable chagned
    public void onNumberOfSyllableChanged()
    {
        StartCoroutine(enterSylNumberHelper());
    }

    // Change the number of accent rule as the number of accent chagned
    public void onNumberOfAccentChanged()
    {
        StartCoroutine(enterAccNumberHelper());
        
    }

    // Open the editor to change the affix
    public void onEditeAffixButtonPressed(int toggleIndex)
    {
        // Check if the corrensponding affix type is enabled
        bool canCreate = false;
        if (toggleIndex == 0)
        {
            canCreate = specialPrefixToggle.isOn;
        }
        else
        {
            canCreate = specialSuffixToggle.isOn;
        }

        // If the correponding affix type is enabled, then open the affix maker
        if (canCreate)
        {
            GameObject instanceAffixMaker = (GameObject)Instantiate(prefabedAffixMaker, transform);
            AffixMakerScript affixMakerScript = instanceAffixMaker.GetComponent<AffixMakerScript>();
            affixMakerScript.isPrefix = toggleIndex == 0;
            affixMakerScript.affixPhoneme = new Phoneme();
            // Debug.Log(prefixPhoneme.phones.Length);
            if (toggleIndex == 0)
            {
                affixMakerScript.parentIPA = specialPrefixIPA;
                affixMakerScript.parentLetter = specialPrefixLetters;
                affixMakerScript.thisIPA.text = specialPrefixIPA.text;
                affixMakerScript.thisLetters.text = specialPrefixLetters.text;
                affixMakerScript.affixPhoneme.addPhone(prefixPhoneme);
                affixMakerScript.parentPhoneme = prefixPhoneme;
            }
            else
            {
                affixMakerScript.parentIPA = specialSuffixIPA;
                affixMakerScript.parentLetter = specialSuffixLetters;
                affixMakerScript.thisIPA.text = specialSuffixIPA.text;
                affixMakerScript.thisLetters.text = specialSuffixLetters.text;
                affixMakerScript.affixPhoneme.addPhone(suffixPhoneme);
                affixMakerScript.parentPhoneme = suffixPhoneme;
            }
        }
    }

    // Open the editor to add or delete vowel
    public void onAddSpecialVowelButtonPressed()
    {

    }

    // Open the editor to add or delete consonant
    public void onAddSpecialConsonantButtonPressed()
    {

    }

    // Change the number of OneAccent
    private void changeNumberOfOneAccent()
    {
        int currentNumOfAccent = accentPanel.transform.childCount;
        if (currentNumOfAccent < accentNum)
        {
            while (accentPanel.transform.childCount < accentNum)
            {
                GameObject newAccentRuleItem = (GameObject)Instantiate(prefabedOneAccent, accentPanel.transform);
            }
            
        }
        else if (currentNumOfAccent > accentNum)
        {
            
            while (accentPanel.transform.childCount > accentNum)
            {
                GameObject toDelete = accentPanel.transform.GetChild(accentPanel.transform.childCount - 1).gameObject;
                toDelete.transform.parent = null;
                Destroy(toDelete);
            }
        } 
    }

    // Change the number of OneVewol
    private void changeNumberOfOneVewol()
    {

    }

    private IEnumerator enterSylNumberHelper()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);
        try
        {
            int newNum = int.Parse(numOfSylInput.text);
            if (newNum >= 1)
            {
                syllableNum = newNum;
                if (arabicFormatToggle.isOn)
                {
                    changeNumberOfOneVewol();
                }
                if (accentNum > syllableNum)
                {
                    changeNumberOfOneAccent();
                }
            }   

        }
        catch (Exception e)
        {
        }
    }

    private IEnumerator enterAccNumberHelper()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);
        try
        {
            int newNum = int.Parse(numOfAccInput.text);
            if (newNum >= 0)
            {
                accentNum = newNum;
                if (accentNum > syllableNum)
                {
                    accentNum = syllableNum;
                    
                }
                changeNumberOfOneAccent();

            }

        }
        catch (Exception e)
        {
        }
    }


}
