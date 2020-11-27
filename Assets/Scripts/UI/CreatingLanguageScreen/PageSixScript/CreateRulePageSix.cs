using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRulePageSix : MonoBehaviour
{
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
    public Text specialPrefixIPA, specialSuffixIPA, specialPrefixLetters, specialSuffixLetter;
    public Toggle specialVowelToggle, specialConsonantToggle;
    public Text specialVowelIPA, specialConsonantIPA, specialVowelLetters, specialConsonantLetter;
    public Toggle useSemivoweledConsonant, useClusteredConsonant;
    public GameObject arabicFormatContent;

    // Prefabed GameObject
    // TODO...
    // List all the prefabed object needed for create rule
    public GameObject prefabedOneAccent, prefabedOneVowel;

    // Start is called before the first frame update
    void Start()
    {
        
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

    }

    // Change the number of valid vowel holder and accent as the number of syllable chagned
    public void onNumberOfSyllableChanged()
    {

    }

    // Change the number of accent rule as the number of accent chagned
    public void onNumberOfAccentChanged()
    {

    }

    // Open the editor to change the affix
    public void onEditeAffixButtonPressed(int toggleIndex)
    {

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
        
    }

    // Change the number of OneVewol
    private void changeNumberOfOneVewol()
    {

    }
}
