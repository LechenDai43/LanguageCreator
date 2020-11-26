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

            // Get the consonants for begin of word
            List<Phoneme> phonemeListForBoW = new List<Phoneme>();
            for (int i = 1; i < numOfBoWConsonent; i++)
            {
                phonemeListForBoW.Add(pageTwoBoW.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
            }
            languageManager.setBoW(phonemeListForBoW.ToArray());

            // Get the consonants for begin of syllable
            List<Phoneme> phonemeListForBoS = new List<Phoneme>();
            for (int i = 1; i < numOfBoSConsonent; i++)
            {
                phonemeListForBoS.Add(pageTwoBoS.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
            }
            languageManager.setBoS(phonemeListForBoS.ToArray());

            // Get the consonants for end of word
            int numOfEoWConsonent, numOfEoSConsonent;
            numOfEoWConsonent = pageTwoEoW.transform.childCount;
            List<Phoneme> phonemeListForEoW = new List<Phoneme>();
            for (int i = 1; i < numOfEoWConsonent; i++)
            {
                phonemeListForEoW.Add(pageTwoEoW.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
            }
            languageManager.setEoW(phonemeListForEoW.ToArray());

            // Get the consonants for end of syllable
            numOfEoSConsonent = pageTwoEoS.transform.childCount;
            List<Phoneme> phonemeListForEoS = new List<Phoneme>();
            for (int i = 1; i < numOfEoSConsonent; i++)
            {
                phonemeListForEoS.Add(pageTwoEoS.transform.GetChild(i).GetComponent<OverSizeItemScript>().phoneme);
            }
            languageManager.setEoS(phonemeListForEoS.ToArray());
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
    }
}
