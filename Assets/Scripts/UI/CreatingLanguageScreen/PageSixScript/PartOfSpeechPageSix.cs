using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfSpeechPageSix : MonoBehaviour
{
    public int index;
    public GameObject thisPanel;

    // Start is called before the first frame update
    void Start()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * (index % 2) / 2, width / 2);
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height * (index / 2) / 2, height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * (index % 2) / 2, width / 2);
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height * (index / 2) / 2, height / 2);
    }

    // Add new rule button function
    public GameObject prefabedPanel;
    public void addNewRuleButtonPressed()
    {
        GameObject instancePanel = (GameObject)Instantiate(prefabedPanel, transform.parent);
        instancePanel.transform.GetComponent<CreateRulePageSix>().fromThisPanel = thisPanel;
        instancePanel.transform.GetComponent<CreateRulePageSix>().fromThisIndex = index;
    }

    // Handle the button pressed to import rules
    public GameObject[] otherPartOfSpeech; // This is the immediate parent of the rule items
    public GameObject prefabedBanner;
    public void importRuleButtonPressed(int buttonIndex)
    {
        if (buttonIndex >= otherPartOfSpeech.Length)
        {
            return;
        }

        GameObject template = otherPartOfSpeech[buttonIndex].thisPanel ;
        for (int i = 0; i < template.transform.childCount; i++)
        {
            RuleBannerPageSix bannerScriptOld = template.transform.GetChild(i).GetComponent<RuleBannerPageSix>();
            GameObject instanceBanner = (GameObject)Instantiate(prefabedBanner, thisPanel.transform);
            RuleBannerPageSix bannerScriptNew = instanceBanner.transform.GetChild(i).GetComponent<RuleBannerPageSix>();
            bannerScriptNew.description.text = bannerScriptOld.description.text;

            WordFormat newWordFormat = new WordFormat();
            // Copy the fields that is primary type or need not deep copy
            newWordFormat.numOfSyllable = bannerScriptOld.format.numOfSyllable;
            newWordFormat.arabicStyle = bannerScriptOld.format.arabicStyle;
            newWordFormat.consonantWithSemivowel = bannerScriptOld.format.consonantWithSemivowel;
            newWordFormat.specialVowel = bannerScriptOld.format.specialVowel;
            newWordFormat.specialConsonant = bannerScriptOld.format.specialConsonant;
            newWordFormat.vowelHolders = bannerScriptOld.format.vowelHolders;

            // Deep copy other field
            Phoneme newLeading = new Phoneme();
            newLeading.addPhone(bannerScriptOld.format.specialLeading);
            Phoneme newEnding = new Phoneme();
            newEnding.addPhone(bannerScriptOld.format.specialEnding);
            newWordFormat.specialLeading = newLeading;
            newWordFormat.specialEnding = newEnding;

            // Deep copy accent rules
            WordFormat.AccentRule[] newAccentRuleList = new WordFormat.AccentRule[bannerScriptOld.format.accentRules.Length];
            for (int j = 0; j < newAccentRuleList.Length; j++)
            {
                WordFormat.AccentRule newAccentRule = new WordFormat.AccentRule();
                WordFormat.AccentRule oldAccentRule = bannerScriptOld.format.accentRules[j] ;
                newAccentRule.backword = oldAccentRule.backword;
                newAccentRule.position = oldAccentRule.position;
                newAccentRule.accents = new AccentPhone[oldAccentRule.accents.Length];

                for (int k = 0; k < newAccentRule.accents.Length; k++)
                {
                    newAccentRule.accents[k] = oldAccentRule.accents[k];
                }

                newAccentRuleList[j] = newAccentRule;
            }
            newWordFormat.accentRules = newAccentRuleList;

            bannerScriptNew.format = newWordFormat;
        }
    }
}
