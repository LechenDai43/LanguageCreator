using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class OverSizePageFourScript : MonoBehaviour, IDropHandler
{
    public bool addedToParent = false;
    public Phoneme phoneme;
    public Text IPAText, letterText, frequencyText;
    public GameObject anotherItem;
    public int parentIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    public void handleEnterLetters()
    {
        phoneme.letters = transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
    }

    public void handleEnterNumbers()
    {
        string inputNum = transform.GetChild(2).GetChild(2).GetComponent<Text>().text;
        if (!inputNum.Equals(""))
        {
            phoneme.frequency = Double.Parse(inputNum);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !this.transform.GetChild(0).GetComponent<Text>().text.Equals("'"))
        {
            SemivowelItemScript draggedObject = eventData.pointerDrag.GetComponent<SemivowelItemScript>();
            GameObject overSizeObject = draggedObject.instanceOfObject;
            if (overSizeObject != null)
            {
                
                Destroy(overSizeObject);
            }
        }
    }
}
