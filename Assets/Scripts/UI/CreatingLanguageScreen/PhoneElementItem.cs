using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PhoneElementItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ProtoPhone phone; 
    public GameObject generatedObject;
    public GameObject instanceOfObject;
    public RectTransform rectTransform;
    public string letter;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        letter = this.transform.GetChild(0).GetComponent<Text>().text;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        instanceOfObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create an instantiation of the oversize item
        Debug.Log(instanceOfObject == null);
        instanceOfObject = (GameObject)Instantiate(generatedObject, transform.parent.parent.parent.parent);
        instanceOfObject.transform.position = transform.position;
        StartCoroutine(waitToGetContent());

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(waitToDestroy());
        
    }

    public void handleInputValueChange()
    {
        if (this.transform.GetChild(1).GetChild(2).GetComponent<Text>().text.Equals(""))
        {
            letter = this.transform.GetChild(0).GetComponent<Text>().text;
        }
        else
        {
            letter = this.transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
        }
    }

    private IEnumerator waitToGetContent()
    {
        yield return new WaitForSeconds(Time.deltaTime);
        // Add informations to the oversize item
        // The IPA
        instanceOfObject.transform.GetChild(0).GetComponent<Text>().text = this.transform.GetChild(0).GetComponent<Text>().text;
        // The letter
        instanceOfObject.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = letter;        
        // The frequency
        instanceOfObject.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = "1";

        Phoneme aPhoneme = new Phoneme();
        aPhoneme.phones = new ProtoPhone[1];
        aPhoneme.phones[0] = phone;
        aPhoneme.letters = letter;
        aPhoneme.frequency = 1;
        instanceOfObject.GetComponent<OverSizeItemScript>().phoneme = aPhoneme;
    }

    private IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        if (instanceOfObject != null)
        {
            OverSizeItemScript overSizeItemScript = instanceOfObject.GetComponent<OverSizeItemScript>();
            if (!overSizeItemScript.addedToParent)
            {
                Destroy(instanceOfObject);
            }
        }
        
    }
}
