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
            // Debug.Log(manager.languageManager.languageName);
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
                    Debug.Log(pageTwoBoW.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme.letters);
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
        Debug.Log(asList.Count);
        languageManager.setAS(asList.ToArray());

        List<Phoneme> usList = new List<Phoneme>();
        for (int i = 1; i < pageFourUs.transform.childCount; i++)
        {
            usList.Add(pageFourUs.transform.GetChild(i).GetComponent<OverSizePageFourScript>().phoneme);
            listToDestroy.Add(pageFourUs.transform.GetChild(i).gameObject);
        }
        Debug.Log(usList.Count);
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
                AccentPhone inManager = languageManager.accents[i];
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
}
