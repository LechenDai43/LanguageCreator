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
        if (eventData.pointerDrag != null)
        {
            PhoneElementItem draggedObject = eventData.pointerDrag.GetComponent<PhoneElementItem>();
            GameObject overSizeObject = draggedObject.instanceOfObject;
            if (overSizeObject != null)
            {

                string nextIPA = overSizeObject.transform.GetChild(0).GetComponent<Text>().text;
                string nextLetter = overSizeObject.transform.GetChild(1).GetChild(1).GetComponent<Text>().text;
                Debug.Log(nextIPA);
                if (!overSizeObject.transform.GetChild(1).GetChild(2).GetComponent<Text>().text.Equals(""))
                {
                    nextLetter = overSizeObject.transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
                }

                this.transform.GetChild(0).GetComponent<Text>().text += nextIPA;
                if (this.transform.GetChild(1).GetChild(2).GetComponent<Text>().text.Equals(""))
                {
                    this.transform.GetChild(1).GetChild(1).GetComponent<Text>().text += nextLetter;
                }
                else
                {
                    this.transform.GetChild(1).GetChild(2).GetComponent<Text>().text += nextLetter;
                }
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
        phoneme.letters = transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
        Debug.Log(phoneme.letters);
    }

    public void handleEnterNumbers()
    {
        string inputNum = transform.GetChild(2).GetChild(2).GetComponent<Text>().text;
        if (!inputNum.Equals(""))
        {
            phoneme.frequency = Double.Parse(inputNum);
        }
        Debug.Log(phoneme.frequency);
    }
}
