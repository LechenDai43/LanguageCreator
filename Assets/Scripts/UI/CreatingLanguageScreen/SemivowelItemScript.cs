using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SemivowelItemScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Text IPAtext, letterText;
    public Toggle SasVToggle, SasCToggle, LallVToggle, FallCToogle;
    public GameObject generatedObject;
    public GameObject instanceOfObject;
    public ProtoPhone phone;
    public string letter;

    // Start is called before the first frame update
    void Start()
    {

        letter = this.transform.GetChild(0).GetComponent<Text>().text;
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

    public void OnDrag(PointerEventData eventData)
    {
        instanceOfObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        instanceOfObject = (GameObject)Instantiate(generatedObject, transform.parent.parent.parent.parent);
        instanceOfObject.transform.position = transform.position;
        StartCoroutine(waitToGetContent());
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(waitToDestroy());
    }

    private IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        if (instanceOfObject != null)
        {
            OverSizePageFourScript overSizeItemScript = instanceOfObject.GetComponent<OverSizePageFourScript>();
            if (!overSizeItemScript.addedToParent)
            {
                Destroy(instanceOfObject);
            }
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
}
