using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OverSizeItemScript : MonoBehaviour, IDropHandler
{

    public bool addedToParent = false;

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

    }
}
