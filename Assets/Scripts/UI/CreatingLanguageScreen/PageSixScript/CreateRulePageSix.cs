using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRulePageSix : MonoBehaviour
{
    // Meta information
    private int syllableNum = 0, accentNum = 0;
    public WordFormat aFormat;
    public GameObject prefabedRuleBanner;
    public RuleBannerPageSix oldBanner;

    // Information about who created this panel
    public GameObject fromThisPanel;
    public int fromThisIndex;

    // GameObject attached to this panel
    public Text holderOfSylNum, holderOfAccNum;
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
    public Phoneme[] vowelsPhonemeForArabic, specialPickedVowel, specialPickedConsonant;

    // Prefabed GameObject
    // TODO...
    // List all the prefabed object needed for create rule
    public GameObject prefabedOneAccent, prefabedOneVowel;
    public GameObject prefabedAffixMaker;
    public GameObject prefabedSpecialPhonemePicker;

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
        aFormat = new WordFormat();
        aFormat.numOfSyllable = syllableNum;        
        
        if (arabicFormatToggle.isOn)
        {
            aFormat.arabicStyle = true;
            aFormat.consonantWithSemivowel = useSemivoweledConsonant.isOn;
            aFormat.consonantCluster = useClusteredConsonant.isOn;

            List<Phoneme> forVowelHolder = new List<Phoneme>();
            for (int i = 0; i < arabicFormatContent.transform.childCount; i++)
            {
                ToBePickedScript script = arabicFormatContent.transform.GetChild(i).GetComponent<ToBePickedScript>();
                if (script.aVowelPhoneme == null)
                {
                    aFormat.arabicStyle = false;
                    aFormat.consonantWithSemivowel = false;
                    aFormat.consonantCluster = false;
                    break;
                }
                forVowelHolder.Add(script.aVowelPhoneme);
            }
            aFormat.vowelHolders = forVowelHolder.ToArray();
        }
        else
        {
            aFormat.arabicStyle = false;
            
            // Get accent rules
            if (accentNum > 0)
            {
                List<WordFormat.AccentRule> aList = new List<WordFormat.AccentRule>();
                for (int i = 0; i < accentPanel.transform.childCount; i++)
                {
                    AddAccentPageSix script = accentPanel.transform.GetChild(i).GetComponent<AddAccentPageSix>();
                    WordFormat.AccentRule wfar = new WordFormat.AccentRule();

                    if (script.coundBack.options.ToArray()[script.coundBack.value].text.Equals("Backward"))
                    {
                        wfar.backword = true;
                    }
                    else
                    {
                        wfar.backword = false;
                    }

                    try
                    {
                        if (script.positionField.text.Equals(""))
                        {
                            continue;
                        }
                        wfar.position = int.Parse(script.positionField.text);

                        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
                        LanguageManager languageManager = manager.languageManager;
                        AccentPhone[] accents = languageManager.accents;

                        if (script.accentType.value.Equals("all"))
                        {
                            wfar.accents = accents;
                        }
                        else
                        {
                            List<AccentPhone> potentialAccent = new List<AccentPhone>();
                            foreach (AccentPhone actp in accents)
                            {
                                string value = script.accentType.options.ToArray()[script.accentType.value].text;
                                string IPA = actp.IPA;
                                string contour = actp.Contour;
                                string level = actp.Level;
                                if (value.Equals(IPA) || value.Equals(contour) || value.Equals(level))
                                {
                                    potentialAccent.Add(actp);
                                }
                            }
                            wfar.accents = potentialAccent.ToArray();
                        }
                        aList.Add(wfar);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                aFormat.accentRules = aList.ToArray();
            }

            // Get special affix
            if (specialAffixToggle.isOn)
            {
                if (specialPrefixToggle.isOn && prefixPhoneme != null && prefixPhoneme.phones.Length > 0)
                {
                    aFormat.specialLeading = prefixPhoneme;
                }
                if (specialSuffixToggle.isOn && suffixPhoneme != null && suffixPhoneme.phones.Length > 0)
                {
                    aFormat.specialEnding = suffixPhoneme;
                }
            }

            // Get special phone
            if (specialPhoneToggle.isOn)
            {
                if (specialVowelToggle.isOn && specialPickedVowel != null && specialPickedVowel.Length > 0)
                {
                    aFormat.specialVowel = specialPickedVowel;
                }

                if (specialConsonantToggle.isOn && specialPickedConsonant != null && specialPickedConsonant.Length > 0)
                {
                    aFormat.specialConsonant = specialPickedConsonant;
                }
            }

            // Create a rule banner 
            // prefabedRuleBanner
            // RuleBannerPageSix

            if (oldBannder != null && fromThisPanel == null)
            {
                oldBannder.format = aFormat;
                oldBannder.description.text = aFormat.getDescription();
            }
            if (oldBannder == null && fromThisPanel != null)
            {
                GameObject instanceBanner = (GameObject)Instantiate(prefabedRuleBanner, fromThisPanel.transform);
                RuleBannerPageSix instanceScript = instanceBanner.GetComponent<RuleBannerPageSix>();
                instanceScript.format = aFormat;
                instanceScript.description.text = aFormat.getDescription();
            }
            

            Destroy(this.transform.gameObject);
        }
    }

    // Do not save this rule and exit the rule edit panel
    public void onDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    // Display the corresponding panel as the toggle in formatter changed
    public void onFormatterToggleChanged(int toggleIndex)
    {
        if (toggleIndex == 0)
        {
            if (specialAffixToggle.isOn)
            {
                specialAffixPanel.SetActive(true);
                arabicFormatPanel.SetActive(false);
                arabicFormatToggle.isOn = false;
            }
            else
            {
                specialAffixPanel.SetActive(false);
            }
        }
        else if (toggleIndex == 1)
        {
            if (specialPhoneToggle.isOn)
            {
                specialPhonePanel.SetActive(true);
                arabicFormatPanel.SetActive(false);
                arabicFormatToggle.isOn = false;
            }
            else
            {
                specialPhonePanel.SetActive(false);
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
            else
            {
                arabicFormatPanel.SetActive(false);
            }
        }
        onNumberOfAccentChanged();
        changeNumberOfOneAccent();
    }

    // Change the number of valid vowel holder and accent as the number of syllable chagned
    public void onNumberOfSyllableChanged()
    {
        StartCoroutine(enterSylNumberHelper());
        StartCoroutine(enterAccNumberHelper());
    }

    // Change the number of accent rule as the number of accent chagned
    public void onNumberOfAccentChanged()
    {
        StartCoroutine(enterAccNumberHelper());
        
    }

    // Open the editor to change the affix
    public void onEditeAffixButtonPressed(int toggleIndex)
    {
        if (!specialAffixToggle.isOn)
        {
            return;
        }
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
        if (specialVowelToggle.isOn && specialPhoneToggle.isOn)
        {
            GameObject instancePhonePicker = (GameObject)Instantiate(prefabedSpecialPhonemePicker, transform);
            SpecialPhonePickingScript script = instancePhonePicker.GetComponent<SpecialPhonePickingScript>();
            script.populate(1);
            script.parent = this;
            script.parentIPA = specialVowelIPA;
            script.parentLetters = specialVowelLetters;
        }
    }

    // Open the editor to add or delete consonant
    public void onAddSpecialConsonantButtonPressed()
    {
        if (specialConsonantToggle.isOn && specialPhoneToggle.isOn)
        {
            GameObject instancePhonePicker = (GameObject)Instantiate(prefabedSpecialPhonemePicker, transform);
            SpecialPhonePickingScript script = instancePhonePicker.GetComponent<SpecialPhonePickingScript>();
            script.populate(0);
            script.parent = this;
            script.parentIPA = specialConsonantIPA;
            script.parentLetters = specialConsonantLetters;

        }
    }

    // Change the number of OneAccent
    private void changeNumberOfOneAccent()
    {
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();

        // Get the consonant phone pool from the manager
        LanguageManager languageManager = manager.languageManager;

        if (languageManager.accents.Length <= 0)
        {
            return;
        }

        if (arabicFormatToggle.isOn)
        {
            accentNum = 0;
        }

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
        int currentNumOfVowel = arabicFormatContent.transform.childCount;
        if (currentNumOfVowel < syllableNum)
        {
            while (arabicFormatContent.transform.childCount < syllableNum)
            {
                GameObject newVowelItem = (GameObject)Instantiate(prefabedOneVowel, arabicFormatContent.transform);
            }

        }
        else if (currentNumOfVowel > syllableNum)
        {

            while (arabicFormatContent.transform.childCount > syllableNum)
            {
                GameObject toDelete = arabicFormatContent.transform.GetChild(arabicFormatContent.transform.childCount - 1).gameObject;
                toDelete.transform.parent = null;
                Destroy(toDelete);
            }
        }
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
