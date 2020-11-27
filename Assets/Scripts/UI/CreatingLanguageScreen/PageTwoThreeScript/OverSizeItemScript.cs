using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OverSizeItemScript : MonoBehaviour, IDropHandler
{

    public bool addedToParent = false;
    public Phoneme phoneme;
    public Text IPAText, letterText, frequencyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !this.transform.GetChild(0).GetComponent<Text>().text.Equals("'"))
        {
            PhoneElementItem draggedObject = eventData.pointerDrag.GetComponent<PhoneElementItem>();
            GameObject overSizeObject = draggedObject.instanceOfObject;
            if (overSizeObject != null)
            {
                string nextIPA = overSizeObject.transform.GetChild(0).GetComponent<Text>().text;
                string nextLetter = overSizeObject.transform.GetChild(1).GetChild(1).GetComponent<Text>().text;
                if (!overSizeObject.transform.GetChild(1).GetChild(2).GetComponent<Text>().text.Equals(""))
                {
                    nextLetter = overSizeObject.transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
                }

                this.transform.GetChild(0).GetComponent<Text>().text += nextIPA;
                string overallLetter = "";
                if (this.transform.GetChild(1).GetChild(2).GetComponent<Text>().text.Equals(""))
                {
                    this.transform.GetChild(1).GetChild(1).GetComponent<Text>().text += nextLetter;
                    overallLetter = this.transform.GetChild(1).GetChild(1).GetComponent<Text>().text;
                }
                else
                {
                    this.transform.GetChild(1).GetChild(2).GetComponent<Text>().text += nextLetter;
                    overallLetter = this.transform.GetChild(1).GetChild(1).GetComponent<Text>().text;
                }

                phoneme.addPhone(overSizeObject.GetComponent<OverSizeItemScript>().phoneme);
                Destroy(overSizeObject);
            }
        }
    }

    public void handleDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    public void handleEnterLetters()
    {
        StartCoroutine(enterLetterHelper());
    }

    public void handleEnterNumbers()
    {
        StartCoroutine(enterNumberHelper());
    }

    private IEnumerator enterNumberHelper()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);
        string inputNum = transform.GetChild(2).GetChild(2).GetComponent<Text>().text;
        Debug.Log(phoneme.frequency);
        if (!inputNum.Equals(""))
        {
            try
            {
                phoneme.frequency = Double.Parse(inputNum);
            }
            catch (Exception)
            {

            }
        }
        else
        {
            phoneme.frequency = 1;
        }
    }

    private IEnumerator enterLetterHelper()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);
        phoneme.letters = transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
    }
}
