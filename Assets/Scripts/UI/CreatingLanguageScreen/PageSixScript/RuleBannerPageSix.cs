using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleBannerPageSix : MonoBehaviour
{
    public Text description;
    public WordFormat format;
    public GameObject prefabedEditor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The functions called when delete button is pressed
    // Destroy this rule banner
    public void handleDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    // The function called when edit button is pressed
    public void handleEditButtonPressed()
    {
        GameObject instanceEditor = (GameObject)Instantiate(prefabedEditor, transform.parent.parent.parent);
        CreateRulePageSix instanceScript = instanceEditor.GetComponent<CreateRulePageSix>();

        instanceScript.oldBanner = this;

        // populate num of syllable and accent
        instanceScript.syllableNum = format.numOfSyllable;
        instanceScript.holderOfSylNum.text = format.numOfSyllable.ToString();
        instanceScript.aFormat = format;


        // populate the accents
        if (format.accentRules != null && format.accentRules.Length > 0)
        {
            instanceScript.accentNum = format.accentRules.Length;
            instanceScript.holderOfAccNum.text = format.accentRules.Length.ToString();

            foreach (WordFormat.AccentRule ar in format.accentRules)
            {
                GameObject newAccentRuleItem = (GameObject)Instantiate(instanceScript.prefabedOneAccent, instanceScript.accentPanel.transform);
                AddAccentPageSix accentScript = newAccentRuleItem.transform.GetComponent<AddAccentPageSix>();

                // if count back
                if (ar.backword)
                {
                    accentScript.coundBack.value = 1;
                }
                else
                {
                    accentScript.coundBack.value = 0;
                }

                // positions
                accentScript.holder.text = ar.position.ToString();

                // accent types
                if (ar.accents.Length > 1)
                {
                    string commonCon = null, commonLvl = null;
                    bool findUniqueCon = false, findUniqueLvl = false;

                    foreach (AccentPhone ap in ar.accents)
                    {
                        if (commonCon == null)
                        {
                            commonCon = ap.Contour;
                            commonLvl = ap.Level;
                        } 
                        else
                        {
                            if (!commonCon.Equals(ap.Contour))
                            {
                                findUniqueCon = true;
                            }
                            if (!commonLvl.Equals(ap.Level))
                            {
                                findUniqueLvl = true;
                            }
                        }
                    }

                    if (findUniqueLvl && findUniqueCon)
                    {
                        accentScript.accentType.value = accentScript.accentType.options.Count;
                    }
                    else
                    {
                        if(findUniqueCon)
                        {
                            accentScript.accentType.value = findValueFromDropDown(accentScript.accentType, commonLvl);
                        }
                        else
                        {
                            accentScript.accentType.value = findValueFromDropDown(accentScript.accentType, commonCon);
                        }
                    }
                }
                else
                {
                    accentScript.accentType.value = findValueFromDropDown(accentScript.accentType, ar.accents[0].IPA);
                }
            }
        }

        // populate affix
        if (!format.arabicStyle)
        {   

            if (format.specialLeading != null)
            {
                instanceScript.specialAffixToggle.isOn = true;
                instanceScript.specialAffixPanel.SetActive(true);
                instanceScript.specialPrefixToggle.isOn = true;
                instanceScript.prefixPhoneme = new Phoneme();
                instanceScript.prefixPhoneme.addPhone(format.specialLeading);
                //  specialPrefixIPA, specialSuffixIPA, specialPrefixLetters
                instanceScript.specialPrefixIPA.text = instanceScript.prefixPhoneme.getIPA();
                instanceScript.specialPrefixLetters.text = instanceScript.prefixPhoneme.letters;

            }

            if (format.specialEnding != null)
            {
                instanceScript.specialAffixToggle.isOn = true;
                instanceScript.specialAffixPanel.SetActive(true);
                instanceScript.specialSuffixToggle.isOn = true;
                instanceScript.suffixPhoneme = new Phoneme();
                instanceScript.suffixPhoneme.addPhone(format.specialEnding);
                //  specialPrefixIPA, specialSuffixIPA, specialPrefixLetters
                instanceScript.specialSuffixIPA.text = instanceScript.suffixPhoneme.getIPA();
                instanceScript.specialSuffixLetters.text = instanceScript.suffixPhoneme.letters;

            }
        }


        // populate special phone
        if (!format.arabicStyle)
        {
            if (format.specialVowel != null && format.specialVowel.Length > 0)
            {
                instanceScript.specialPhoneToggle.isOn = true;
                instanceScript.specialPhonePanel.SetActive(true);
                instanceScript.specialVowelToggle.isOn = true;

                string totalIPA = "", totalLet = "";
                instanceScript.specialPickedVowel = new Phoneme[format.specialVowel.Length];
                for (int i = 0; i < format.specialVowel.Length; i++)
                {
                    if (i != 0)
                    {
                        totalIPA += " ,";
                        totalLet += " ,";
                    }
                    instanceScript.specialPickedVowel[i] = new Phoneme();
                    instanceScript.specialPickedVowel[i].addPhone(format.specialVowel[i]);
                    totalIPA += instanceScript.specialPickedVowel[i].getIPA();
                    totalLet += instanceScript.specialPickedVowel[i].letters;
                }

                //specialVowelIPA, specialConsonantIPA, specialVowelLetters, specialConsonantLetters;
                instanceScript.specialVowelIPA.text = totalIPA;
                instanceScript.specialVowelLetters.text = totalLet;

            }

            if (format.specialConsonant != null && format.specialConsonant.Length > 0)
            {
                instanceScript.specialPhoneToggle.isOn = true;
                instanceScript.specialPhonePanel.SetActive(true);
                instanceScript.specialConsonantToggle.isOn = true;

                string totalIPA = "", totalLet = "";
                instanceScript.specialPickedConsonant = new Phoneme[format.specialConsonant.Length];
                for (int i = 0; i < format.specialConsonant.Length; i++)
                {
                    if (i != 0)
                    {
                        totalIPA += " ,";
                        totalLet += " ,";
                    }
                    instanceScript.specialPickedConsonant[i] = new Phoneme();
                    instanceScript.specialPickedConsonant[i].addPhone(format.specialConsonant[i]);
                    totalIPA += instanceScript.specialPickedConsonant[i].getIPA();
                    totalLet += instanceScript.specialPickedConsonant[i].letters;
                }

                //specialVowelIPA, specialConsonantIPA, specialVowelLetters, specialConsonantLetters;
                instanceScript.specialConsonantIPA.text = totalIPA;
                instanceScript.specialConsonantLetters.text = totalLet;

            }
        }


        // populate arabic style
    }
    private int findValueFromDropDown(Dropdown dropdown, string text)
    {
        int count = 0;
        foreach (Dropdown.OptionData dpdnOpd in dropdown.options)
        {
            if (dpdnOpd.text.Equals(text))
            {
                break;
            }
            count++;
        }
        return count;
    }
}
