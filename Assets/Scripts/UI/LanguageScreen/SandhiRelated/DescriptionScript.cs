using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionScript : MonoBehaviour
{
    public Dropdown mainSelector;
    public GameObject generic, consonant, vowel;

    // for generic
    public Toggle initialPosition, endingPosition;
    public Toggle oneConsonent, oneVowel;
    public Toggle consonantCluster, vowelCluster;

    // for consonant
    public Dropdown mannerSelector, positionSelector;

    // for vowel
    public Dropdown opennessSelector, roundednessSelector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mainSelectorChanged()
    {
        if (mainSelector.value == 0)
        {
            generic.SetActive(true);
            consonant.SetActive(false);
            vowel.SetActive(false);
        } 
        else if (mainSelector.value == 1)
        {
            generic.SetActive(false);
            consonant.SetActive(true);
            vowel.SetActive(false);
        }
        else
        {
            generic.SetActive(false);
            consonant.SetActive(false);
            vowel.SetActive(true);
        }
    }

    public void toggleChanged(int index)
    {

        initialPosition.isOn = index == 0;
        endingPosition.isOn = index == 1;
        oneConsonent.isOn = index == 2;
        oneVowel.isOn = index == 3;
        consonantCluster.isOn = index == 4;
        vowelCluster.isOn = index == 5;
    }
}
