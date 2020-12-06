using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateLanguageScreenManager : MonoBehaviour
{
    // The pages in this scene
    public GameObject[] panels;
    private int panelPointer;

    // The text that needs to change locale but not in pages
    public Text backButtonText;
    public Text nextButtonText;

    // The buttons on the scene
    public GameObject backButton;
    public GameObject nextButton;

    // Start is called before the first frame update
    void Start()
    {
        // Once the scene is loaded, only the first page is shown
        panelPointer = 0;
        for (int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
        panels[0].SetActive(true);

        // As the first page doesn't have previous page, so the back button is hidden
        backButton.SetActive(false);
        nextButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Go back to the inital page
    public void backInitialPage()
    {
        Manager manager = Object.FindObjectOfType<Manager>();
        manager.languageManager = new LanguageManager();
        SceneManager.LoadScene("InitialScreen");
    }

    // Go to the previous page
    public void backPage()
    {
        // Go to the previous page
        panelPointer--;

        // If the first page is already shown, then do nothing
        if (panelPointer < 0)
        {
            panelPointer = 0;
            return;
        }

        // If the first page is not already shown, then hide the current page and display the previous page
        changePage(panelPointer + 1);

    }

    // Go to the next page
    public void nextPage()
    {
        // Go to the next page
        panelPointer++;

        // If the last page is already shown, then do nothing
        if (panelPointer >= panels.Length)
        {
            panelPointer = panels.Length - 1;
            return;
        }

        // If the last page is not already shown, then hide the current page and display the next page
        changePage(panelPointer - 1);
    }

    // Change the display of pages
    private void changePage(int oldNum)
    {
        // Change the page first
        purelyChangePage(panelPointer);

        // Call the corresponding function for page changing
        switch(oldNum)
        {
            case 0:
                outOfPageOne();
                break;
            case 1:
                outOfPageTwo();
                break;
            case 2:
                outOfPageThree();
                break;
            case 3:
                outOfPageFour();
                break;
            case 4:
                outOfPageFive();
                break;
        }
    }

    // Functions when page changes
    public GameObject pageOneNameInput;
    private void outOfPageOne()
    {        
        string inputStr = pageOneNameInput.transform.GetComponent<Text>().text;
        if (inputStr.Equals(""))
        {
            purelyChangePage(0);
        }
        else
        {
            Manager manager = Object.FindObjectOfType<Manager>();
            manager.languageManager.languageName = inputStr;

        }
    }

    public GameObject pageTwoBoW, pageTwoBoS, pageTwoEoW, pageTwoEoS;
    public GameObject pageTwoOverSizeItem;
    private void outOfPageTwo()
    {
        // Check if there any defined consonant(s clusters) for the begin of word and syllable
        int numOfBoWConsonent, numOfBoSConsonent;
        numOfBoWConsonent = pageTwoBoW.transform.childCount;
        numOfBoSConsonent = pageTwoBoS.transform.childCount;

        // If there is no such consonant(s clusters) then force to stay on page 2
        if (numOfBoSConsonent <= 1 || numOfBoWConsonent <= 1)
        {
            purelyChangePage(1);
        }
        // Otherwise, add all the consonant(s clusters) to the manager
        else
        {
            // Get the manager
            Manager manager = Object.FindObjectOfType<Manager>();
            LanguageManager languageManager = manager.languageManager;

            List<Transform> listToDestroy = new List<Transform>();

            // Get the consonants for begin of word
            List<Phoneme> phonemeListForBoW = new List<Phoneme>();
            for (int i = 1; i < numOfBoWConsonent; i++)
            {
                phonemeListForBoW.Add(pageTwoBoW.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
                listToDestroy.Add(pageTwoBoW.transform.GetChild(i));
            }
            languageManager.setBoW(phonemeListForBoW.ToArray());

            // Get the consonants for begin of syllable
            List<Phoneme> phonemeListForBoS = new List<Phoneme>();
            for (int i = 1; i < numOfBoSConsonent; i++)
            {
                phonemeListForBoS.Add(pageTwoBoS.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
                listToDestroy.Add(pageTwoBoS.transform.GetChild(i));
            }
            languageManager.setBoS(phonemeListForBoS.ToArray());

            // Get the consonants for end of word
            int numOfEoWConsonent, numOfEoSConsonent;
            numOfEoWConsonent = pageTwoEoW.transform.childCount;
            List<Phoneme> phonemeListForEoW = new List<Phoneme>();
            for (int i = 1; i < numOfEoWConsonent; i++)
            {
                phonemeListForEoW.Add(pageTwoEoW.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
                listToDestroy.Add(pageTwoEoW.transform.GetChild(i));
            }
            languageManager.setEoW(phonemeListForEoW.ToArray());

            // Get the consonants for end of syllable
            numOfEoSConsonent = pageTwoEoS.transform.childCount;
            List<Phoneme> phonemeListForEoS = new List<Phoneme>();
            for (int i = 1; i < numOfEoSConsonent; i++)
            {
                phonemeListForEoS.Add(pageTwoEoS.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
                listToDestroy.Add(pageTwoEoS.transform.GetChild(i));
            }
            languageManager.setEoS(phonemeListForEoS.ToArray());

            // Destroy all the over size items
            foreach (Transform tf in listToDestroy)
            {
                Destroy(tf.gameObject);
            }
        }
    }
    private IEnumerator intoPageTwo()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);

        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;

        populateLowerPanelWithOversizeItem(pageTwoBoW, languageManager.consonantBoW, pageTwoOverSizeItem);
        populateLowerPanelWithOversizeItem(pageTwoBoS, languageManager.consonantBoS, pageTwoOverSizeItem);
        populateLowerPanelWithOversizeItem(pageTwoEoW, languageManager.consonantEoW, pageTwoOverSizeItem);
        populateLowerPanelWithOversizeItem(pageTwoEoS, languageManager.consonantEoS, pageTwoOverSizeItem);
    }

    public GameObject pageThreeAS, pageThreeUS;
    private void outOfPageThree()
    {
        // Check if there are any vowels for unaccent syllable
        int numOfUSVowel = pageThreeUS.transform.childCount;

        // If there is no vowel for unaccent syallble, then force to stay on page three
        if (numOfUSVowel <= 1)
        {
            purelyChangePage(2);
        }
        // Otherwise, add the vowels to the manager
        else
        {
            // Get the manager
            Manager manager = Object.FindObjectOfType<Manager>();
            LanguageManager languageManager = manager.languageManager;

            List<Transform> listToDestroy = new List<Transform>();

            // Add the unaccent syllable vowels to the manager
            List<Phoneme> phonemeListForUS = new List<Phoneme>();
            for (int i = 1; i < numOfUSVowel; i++)
            {
                phonemeListForUS.Add(pageThreeUS.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
                listToDestroy.Add(pageThreeUS.transform.GetChild(i));
            }
            languageManager.setUS(phonemeListForUS.ToArray());

            // Check if there any special accent vowel
            int numOfASVowel = pageThreeAS.transform.childCount;
            
            // If there is no accent vowel, then use the unaccent vowel for the accent vowel
            if (numOfASVowel <= 1)
            {
                languageManager.setAS(phonemeListForUS.ToArray());
            }
            // Otherwise, get the accent vowels and add them to the manager
            else
            {
                List<Phoneme> phonemeListForAS = new List<Phoneme>();
                for (int i = 1; i < numOfASVowel; i++)
                {
                    phonemeListForAS.Add(pageThreeAS.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
                    listToDestroy.Add(pageThreeAS.transform.GetChild(i));
                }
                languageManager.setAS(phonemeListForAS.ToArray());
            }


            // Destroy all the over size items
            foreach (Transform tf in listToDestroy)
            {
                Destroy(tf.gameObject);
            }
        }
    }
    private IEnumerator intoPageThree()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);

        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;

        populateLowerPanelWithOversizeItem(pageThreeAS, languageManager.vowelAS, pageTwoOverSizeItem);
        populateLowerPanelWithOversizeItem(pageThreeUS, languageManager.vowelUS, pageTwoOverSizeItem);
    }

    public GameObject pageFourBow, pageFourBos, pageFourEow, pageFourEos, pageFourAs, pageFourUs;
    public GameObject pageFourOverSizeItem;
    private void outOfPageFour()
    {
        // Force to stay on this page
        int a = pageFourBow.transform.childCount, b = pageFourBos.transform.childCount, c = pageFourAs.transform.childCount, d = pageFourUs.transform.childCount;
        if (a + b +c + d < 8)
        {
            purelyChangePage(3);
            return;
        }

        // Get the manager
        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;

        List<GameObject> listToDestroy = new List<GameObject>();

        List<Phoneme> bowList = new List<Phoneme>();
        for (int i = 1; i < pageFourBow.transform.childCount; i++)
        {
            bowList.Add(pageFourBow.transform.GetChild(i).GetComponent<OverSizePageFourScript>().phoneme);
            listToDestroy.Add(pageFourBow.transform.GetChild(i).gameObject);
        }
        languageManager.setBoW(bowList.ToArray());

        List<Phoneme> bosList = new List<Phoneme>();
        for (int i = 1; i < pageFourBos.transform.childCount; i++)
        {
            bosList.Add(pageFourBos.transform.GetChild(i).GetComponent<OverSizePageFourScript>().phoneme);
            listToDestroy.Add(pageFourBos.transform.GetChild(i).gameObject);
        }
        languageManager.setBoS(bosList.ToArray());

        List<Phoneme> eowList = new List<Phoneme>();
        for (int i = 1; i < pageFourEow.transform.childCount; i++)
        {
            eowList.Add(pageFourEow.transform.GetChild(i).GetComponent<OverSizePageFourScript>().phoneme);
            listToDestroy.Add(pageFourEow.transform.GetChild(i).gameObject);
        }
        languageManager.setEoW(eowList.ToArray());

        List<Phoneme> eosList = new List<Phoneme>();
        for (int i = 1; i < pageFourEos.transform.childCount; i++)
        {
            eosList.Add(pageFourEos.transform.GetChild(i).GetComponent<OverSizePageFourScript>().phoneme);
            listToDestroy.Add(pageFourEos.transform.GetChild(i).gameObject);
        }
        languageManager.setEoS(eosList.ToArray());

        List<Phoneme> asList = new List<Phoneme>();
        for (int i = 1; i < pageFourAs.transform.childCount; i++)
        {
            asList.Add(pageFourAs.transform.GetChild(i).GetComponent<OverSizePageFourScript>().phoneme);
            listToDestroy.Add(pageFourAs.transform.GetChild(i).gameObject);
        }
        languageManager.setAS(asList.ToArray());

        List<Phoneme> usList = new List<Phoneme>();
        for (int i = 1; i < pageFourUs.transform.childCount; i++)
        {
            usList.Add(pageFourUs.transform.GetChild(i).GetComponent<OverSizePageFourScript>().phoneme);
            listToDestroy.Add(pageFourUs.transform.GetChild(i).gameObject);
        }
        languageManager.setUS(usList.ToArray());


        // Destroy all the over size items
        foreach (GameObject tf in listToDestroy)
        {
            Destroy(tf);
        }
        // List<Phoneme> phonemeListForBoW = new List<Phoneme>();
        // for (int i = 1; i < numOfBoWConsonent; i++)
        // {
        //     phonemeListForBoW.Add(pageTwoBoW.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
        //     Debug.Log(pageTwoBoW.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme.letters);
        //     listToDestroy.Add(pageTwoBoW.transform.GetChild(i));
        // }
        // languageManager.setBoW(phonemeListForBoW.ToArray());


    }
    private IEnumerator intoPageFour()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);

        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;

        populateLowerPanelWithOversizeItemFour(pageFourBow, languageManager.consonantBoW, pageFourOverSizeItem, 0);
        populateLowerPanelWithOversizeItemFour(pageFourBos, languageManager.consonantBoS, pageFourOverSizeItem, 1);
        populateLowerPanelWithOversizeItemFour(pageFourEow, languageManager.consonantEoW, pageFourOverSizeItem, 2);
        populateLowerPanelWithOversizeItemFour(pageFourEos, languageManager.consonantEoS, pageFourOverSizeItem, 3);
        populateLowerPanelWithOversizeItemFour(pageFourAs, languageManager.vowelAS, pageFourOverSizeItem, 4);
        populateLowerPanelWithOversizeItemFour(pageFourUs, languageManager.vowelUS, pageFourOverSizeItem, 5);

    }

    public GameObject pageFivePanel;
    private void outOfPageFive()
    {
        List<AccentPhone> accentPhones = new List<AccentPhone>();
        for (int i = 0; i < pageFivePanel.transform.childCount; i++)
        {
            AccentItem accentItem = pageFivePanel.transform.GetChild(i).GetComponent<AccentItem>();
            if (accentItem.toggle.isOn)
            {
                accentPhones.Add(accentItem.phone);
            }
        }

        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;
        languageManager.accents = accentPhones.ToArray();
    }
    private void intoPageFive()
    {
        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;
        // languageManager.accents = accentPhones.ToArray();
        for (int i = 0; i < pageFivePanel.transform.childCount; i++)
        {
            AccentItem accentItem = pageFivePanel.transform.GetChild(i).GetComponent<AccentItem>();
            AccentPhone onPage = accentItem.phone;
            bool notFound = true;
            for (int j = 0; j < languageManager.accents.Length; j++)
            {
                AccentPhone inManager = languageManager.accents[j];
                if (inManager.IPA.Equals(onPage.IPA))
                {
                    accentItem.toggle.isOn = true;
                    notFound = false;
                    break;
                }
            }
            if (notFound)
            {
                accentItem.toggle.isOn = false;
            }
        }
    }

    // Purely change page
    private void purelyChangePage(int num)
    {
        panelPointer = num;
        // Show or hide pages correspondingly
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
            if (i == panelPointer)
            {
                panels[i].SetActive(true);
            }
        }

        // Show or hide back button correspondingly
        if (panelPointer == 0)
        {
            backButton.SetActive(false);
        }
        else
        {
            backButton.SetActive(true);
        }

        if (panelPointer == panels.Length - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }

        if (num == 1)
        {
            StartCoroutine(intoPageTwo());
        }
        else if (num == 2)
        {
            StartCoroutine(intoPageThree());
        }
        else if (num == 3)
        {
            StartCoroutine(intoPageFour());
        }
        else if (num == 4)
        {
            intoPageFive();
        }
    }

    // Convert phoneme to oversize item
    private void convertPhonemeToOversizeItem(Phoneme toItem, GameObject fromPhoneme)
    {
        OverSizeItemScript attachedScript = fromPhoneme.GetComponent<OverSizeItemScript>();
        attachedScript.addedToParent = true;
        attachedScript.phoneme = toItem;

        string conIPA = "";
        for (int i = 0; i < toItem.phones.Length; i++)
        {
            conIPA += toItem.phones[i].IPA;
        }
        attachedScript.IPAText.text = conIPA;

        attachedScript.letterText.text = toItem.letters;
        attachedScript.frequencyText.text = toItem.frequency.ToString();
    }

    // Populate lower panel for page two and three
    private void populateLowerPanelWithOversizeItem(GameObject panel, Phoneme[] phonemes, GameObject item)
    {
        foreach (Phoneme phoneInList in phonemes)
        {
            GameObject generatedItmObject = (GameObject)Instantiate(item, panel.transform);
            convertPhonemeToOversizeItem(phoneInList, generatedItmObject);
            generatedItmObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    // Convert phoneme to oversize item
    private void convertPhonemeToOversizeItemFour(Phoneme toItem, GameObject fromPhoneme)
    {
        OverSizePageFourScript attachedScript = fromPhoneme.GetComponent<OverSizePageFourScript>();
        attachedScript.addedToParent = true;
        attachedScript.phoneme = toItem;

        string conIPA = "";
        for (int i = 0; i < toItem.phones.Length; i++)
        {
            conIPA += toItem.phones[i].IPA;
        }
        attachedScript.IPAText.text = conIPA;

        attachedScript.letterText.text = toItem.letters;
        attachedScript.frequencyText.text = toItem.frequency.ToString();
    }

    // Populate lower panel for page two and three
    private void populateLowerPanelWithOversizeItemFour(GameObject panel, Phoneme[] phonemes, GameObject item, int index)
    {
        foreach (Phoneme phoneInList in phonemes)
        {
            GameObject generatedItmObject = (GameObject)Instantiate(item, panel.transform);
            generatedItmObject.GetComponent<OverSizePageFourScript>().parentIndex = index;
            convertPhonemeToOversizeItemFour(phoneInList, generatedItmObject);
            generatedItmObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    // Create the new language and jump to the next page
    public GameObject generalPanel, VerbPanerl, NounPanel, AdjectivePanel;
    public void createLangauge ()
    {
        LanguageFamily newLanguageFamily = new LanguageFamily();

        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;

        newLanguageFamily.Name = languageManager.languageName;

        // Get consonants and vowels into the new language family object
        Phoneme[][] allPhonemes = new Phoneme[6][];
        allPhonemes[0] = languageManager.consonantBoW;
        allPhonemes[1] = languageManager.consonantBoS;
        allPhonemes[2] = languageManager.consonantEoS;
        allPhonemes[3] = languageManager.consonantEoW;
        allPhonemes[4] = languageManager.vowelAS;
        allPhonemes[5] = languageManager.vowelUS;

        for (int i = 0; i < allPhonemes.Length; i++)
        {
            // Create new array of speech sounds
            SpeechSound[] speechSounds;
            if (i == 0)
            {
                newLanguageFamily.WordOnsets = new SpeechSound[allPhonemes[i].Length];
                speechSounds = newLanguageFamily.WordOnsets;
            }
            else if (i == 1)
            {
                newLanguageFamily.SyllableOnsets = new SpeechSound[allPhonemes[i].Length];
                speechSounds = newLanguageFamily.SyllableOnsets;
            }
            else if (i == 2)
            {
                newLanguageFamily.SyllableCodas = new SpeechSound[allPhonemes[i].Length];
                speechSounds = newLanguageFamily.SyllableCodas;
            }
            else if (i == 3)
            {
                newLanguageFamily.WordCodas = new SpeechSound[allPhonemes[i].Length];
                speechSounds = newLanguageFamily.WordCodas;
            }
            else if (i == 4)
            {
                newLanguageFamily.StressedVowels = new SpeechSound[allPhonemes[i].Length];
                speechSounds = newLanguageFamily.StressedVowels;
            }
            else
            {
                newLanguageFamily.UnstressedVowels = new SpeechSound[allPhonemes[i].Length];
                speechSounds = newLanguageFamily.UnstressedVowels;
            }

            for (int j = 0; j < allPhonemes[i].Length; j++)
            {
                Phoneme phoneme = allPhonemes[i][j];
                SpeechSound ss = new SpeechSound();

                if (phoneme.preceding != null)
                {
                    ss.Preceded = true;
                    ss.Successed = false;

                    Phone glide = new Phone();
                    glide.converProtoPhone(phoneme.phones[0]);
                    ss.Glide = glide;

                    Phone[] phones = new Phone[phoneme.phones.Length - 1];
                    for (int ii = 0; ii < phones.Length; ii++)
                    {
                        Phone phone = new Phone();
                        phone.converProtoPhone(phoneme.phones[ii + 1]);
                        phones[ii] = phone;
                    }
                    ss.Phonemes = phones;
                }
                else if (phoneme.successing != null)
                {
                    ss.Preceded = false;
                    ss.Successed = true;

                    Phone glide = new Phone();
                    glide.converProtoPhone(phoneme.phones[phoneme.phones.Length - 1]);
                    ss.Glide = glide;

                    Phone[] phones = new Phone[phoneme.phones.Length - 1];
                    for (int ii = 0; ii < phones.Length; ii++)
                    {
                        Phone phone = new Phone();
                        phone.converProtoPhone(phoneme.phones[ii]);
                        phones[ii] = phone;
                    }
                    ss.Phonemes = phones;
                }
                else
                {
                    ss.Preceded = false;
                    ss.Successed = false;

                    Phone[] phones = new Phone[phoneme.phones.Length];
                    for (int ii = 0; ii < phones.Length; ii++)
                    {
                        Phone phone = new Phone();
                        phone.converProtoPhone(phoneme.phones[ii]);
                        phones[ii] = phone;
                    }
                    ss.Phonemes = phones;
                }

                ss.Transliteration = phoneme.letters;
                ss.Frequency = phoneme.frequency;

                speechSounds[j] = ss;
            }
        }

        // Get accents into the new language family object
        Phone[] accents = new Phone[languageManager.accents.Length];
        for (int i = 0; i < languageManager.accents.Length; i++)
        {
            Phone accent = new Phone();
            accent.converProtoPhone(languageManager.accents[i]);
        }
        newLanguageFamily.Accents = accents;

        // Get all the accent rule
        // generalPanel, VerbPanerl, NounPanel, AdjectivePanel;
        // Nouns, Adjectives;
        newLanguageFamily.Generals = getOnePartOfSpeech(generalPanel);
        newLanguageFamily.Verbs = getOnePartOfSpeech(VerbPanerl);
        newLanguageFamily.Nouns = getOnePartOfSpeech(NounPanel);
        newLanguageFamily.Adjectives = getOnePartOfSpeech(AdjectivePanel);

        // WordOnsets, WordCodas, SyllableOnsets, SyllableCodas, StressedVowels, UnstressedVowels;
        //consonantBoW, consonantBoS, consonantEoS, consonantEoW;
        //public Phoneme[] vowelAS, vowelUS;

        string langaugeData = JsonUtility.ToJson(newLanguageFamily);
        System.IO.File.WriteAllText(Application.dataPath + "/Files/Customization/Languages/" + newLanguageFamily.Name + ".languageFamily", langaugeData);
        System.IO.Directory.CreateDirectory(Application.dataPath + "/" + newLanguageFamily.Name);
        
        // Set the new language to the manager
        manager.currentLanguage = newLanguageFamily;

        // Jump to next scenes
        SceneManager.LoadScene("LanguageScreen");

    }

    private Morphome[] getOnePartOfSpeech(GameObject panel)
    {
        List<Morphome> result = new List<Morphome>();
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            Transform go = panel.transform.GetChild(i);
            RuleBannerPageSix script = go.GetComponent<RuleBannerPageSix>();
            WordFormat format = script.format;
            result.Add(getFormat(format));
        }

        return result.ToArray();
    }

    private Morphome getFormat(WordFormat format)
    {
        Morphome result = new Morphome();
        result.SyllableNumber = format.numOfSyllable;

        // Get info if the format is in arabic style
        if (format.arabicStyle)
        {
            result.AfroAsian = true;
            result.Affixed = false;
            result.SpecialPhone = false;
            result.SemivoweledConsonant = format.consonantWithSemivowel;
            result.ClusteredConsonant = format.consonantCluster;
            result.HolderVowels = convertPhonemes(format.vowelHolders);
        }

        // Get info if the format is not arabic style
        else
        {
            result.AfroAsian = false;
            result.SemivoweledConsonant = false;
            result.ClusteredConsonant = false;
            result.Affixed = false;
            result.SpecialPhone = false;

            // Set affix
            if (format.specialLeading != null && format.specialLeading.phones.Length > 0)
            {
                result.Affixed = true;
                result.Prefix = convertPhoneme(format.specialLeading);
            }
            if (format.specialEnding != null && format.specialEnding.phones.Length > 0)
            {
                result.Affixed = true;
                result.Suffix = convertPhoneme(format.specialEnding);
            }

            // Set special phone
            if (format.specialVowel != null && format.specialVowel.Length > 0)
            {
                result.SpecialPhone = true;
                result.SpecialVowels = convertPhonemes(format.specialVowel);
            }
            if (format.specialConsonant != null && format.specialConsonant.Length > 0)
            {
                result.SpecialPhone = true;
                result.SpecialConsonants = convertPhonemes(format.specialConsonant);
            }

            // Set accent rule
            if (format.accentRules != null && format.accentRules.Length > 0)
            {
                List<Morphome.ToneRequiement> ruleList = new List<Morphome.ToneRequiement>();
                for (int k = 0; k < format.accentRules.Length; k++)
                {
                    WordFormat.AccentRule accentRule = format.accentRules[k];
                    Morphome.ToneRequiement tone = new Morphome.ToneRequiement();

                    if (accentRule.backword)
                    {
                        int position = result.SyllableNumber - accentRule.position;
                        tone.Position = position;
                    }
                    else
                    {
                        tone.Position = accentRule.position;
                    }
                    if (tone.Position <= 0 || tone.Position > result.SyllableNumber)
                    {
                        continue;
                    }

                    Phone[] potentials = new Phone[accentRule.accents.Length];
                    for (int kk = 0; kk < potentials.Length; kk++)
                    {
                        Phone potential = new Phone();
                        potential.converProtoPhone(accentRule.accents[kk]);
                        potentials[kk] = potential;
                    }
                    tone.Accents = potentials;

                    ruleList.Add(tone);
                }
                result.Tones = ruleList.ToArray();
            }
            
        }

        return result;
    }

    private SpeechSound[] convertPhonemes(Phoneme[] arr)
    {
        List<SpeechSound> result = new List<SpeechSound>();
        for (int i = 0; i < arr.Length; i++)
        {
            Phoneme phoneme = arr[i];
            
            result.Add(convertPhoneme(phoneme));
        }
        return result.ToArray();
    }

    private SpeechSound convertPhoneme(Phoneme phoneme)
    {
        SpeechSound ss = new SpeechSound();

        if (phoneme.preceding != null)
        {
            ss.Preceded = true;
            ss.Successed = false;

            Phone glide = new Phone();
            glide.converProtoPhone(phoneme.phones[0]);
            ss.Glide = glide;

            Phone[] phones = new Phone[phoneme.phones.Length - 1];
            for (int ii = 0; ii < phones.Length; ii++)
            {
                Phone phone = new Phone();
                phone.converProtoPhone(phoneme.phones[ii + 1]);
                phones[ii] = phone;
            }
            ss.Phonemes = phones;
        }
        else if (phoneme.successing != null)
        {
            ss.Preceded = false;
            ss.Successed = true;

            Phone glide = new Phone();
            glide.converProtoPhone(phoneme.phones[phoneme.phones.Length - 1]);
            ss.Glide = glide;

            Phone[] phones = new Phone[phoneme.phones.Length - 1];
            for (int ii = 0; ii < phones.Length; ii++)
            {
                Phone phone = new Phone();
                phone.converProtoPhone(phoneme.phones[ii]);
                phones[ii] = phone;
            }
            ss.Phonemes = phones;
        }
        else
        {
            ss.Preceded = false;
            ss.Successed = false;

            Phone[] phones = new Phone[phoneme.phones.Length];
            for (int ii = 0; ii < phones.Length; ii++)
            {
                Phone phone = new Phone();
                phone.converProtoPhone(phoneme.phones[ii]);
                phones[ii] = phone;
            }
            ss.Phonemes = phones;
        }

        ss.Transliteration = phoneme.letters;
        ss.Frequency = phoneme.frequency;
        return ss;
    }
}
