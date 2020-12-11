using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionScript : MonoBehaviour
{
    public Dropdown mainSelector;
    public GameObject generic, consonant, vowel, glide;
    public DescriptionContainerScript container = null;
    public MetaBlockScript block = null;

    // for generic
    public Toggle initialPosition, endingPosition;
    public Toggle oneConsonent, oneVowel;
    public Toggle consonantCluster, vowelCluster;

    // for consonant
    public Dropdown mannerSelector, positionSelector;

    // for vowel
    public Dropdown opennessSelector, roundednessSelector;

    // for glide
    public Dropdown glideSelector;

    // Start is called before the first frame update
    void Start()
    {
        if (block != null)
        {
            block.childChangeToggle(this);
        }

        List<string> glideString = new List<string>();
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
        PhoneManager phoneManager = manager.phoneManager;
        foreach (SemivowelPhone sp in phoneManager.semivowelPool)
        {
            glideString.Add(sp.IPA);
        }
        glideString.Add("any");
        glideSelector.AddOptions(glideString);
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
            glide.SetActive(false);
        } 
        else if (mainSelector.value == 1)
        {
            generic.SetActive(false);
            consonant.SetActive(true);
            vowel.SetActive(false);
            glide.SetActive(false);
        }
        else if (mainSelector.value == 2)
        {
            generic.SetActive(false);
            consonant.SetActive(false);
            vowel.SetActive(true);
            glide.SetActive(false);
        }
        else
        {
            generic.SetActive(false);
            consonant.SetActive(false);
            vowel.SetActive(false);
            glide.SetActive(true);
        }

        if (container != null)
        {
            container.childChangeToggle();
        }

        if (block != null)
        {
            block.childChangeToggle(this);
        }
    }

    public void toggleChanged(int index)
    {
        Debug.Log(index);
        
        if (index == 0)
        {
            endingPosition.isOn = false;
            oneConsonent.isOn = false;
            oneVowel.isOn = false;
            consonantCluster.isOn = false;
            vowelCluster.isOn = false;
        }
        else if (index == 1)
        {
            initialPosition.isOn = false;
            oneConsonent.isOn = false;
            oneVowel.isOn = false;
            consonantCluster.isOn = false;
            vowelCluster.isOn = false;
        }
        else if (index == 2)
        {
            initialPosition.isOn = false;
            endingPosition.isOn = false;
            oneVowel.isOn = false;
            consonantCluster.isOn = false;
            vowelCluster.isOn = false;
        }
        else if (index == 3)
        {
            initialPosition.isOn = false;
            endingPosition.isOn = false;
            oneConsonent.isOn = false;
            consonantCluster.isOn = false;
            vowelCluster.isOn = false;
        }
        else if (index == 4)
        {
            initialPosition.isOn = false;
            endingPosition.isOn = false;
            oneConsonent.isOn = false;
            oneVowel.isOn = false;
            vowelCluster.isOn = false;
        }
        else if (index == 5)
        {
            initialPosition.isOn = false;
            endingPosition.isOn = false;
            oneConsonent.isOn = false;
            oneVowel.isOn = false;
            consonantCluster.isOn = false;
        }
    }
}
