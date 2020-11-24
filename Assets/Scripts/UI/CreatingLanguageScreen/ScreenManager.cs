using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        changePage();

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
        changePage();
    }

    private void changePage()
    {
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

        // Show or hide next button correspondingly
        if (panelPointer == panels.Length - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }
    }
}
