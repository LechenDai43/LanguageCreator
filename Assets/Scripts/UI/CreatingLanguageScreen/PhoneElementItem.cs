using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneElementItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ProtoPhone phone; 
    public GameObject generatedObject;
    public GameObject instanceOfObject;
    public RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (phone != null)
        {
            
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        instanceOfObject.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create an instantiation of the oversize item
        instanceOfObject = (GameObject)Instantiate(generatedObject, transform.parent.parent.parent.parent);
        instanceOfObject.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

        // Add informations to the oversize item

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(waitToDestroy());
        
    }

    private IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        OverSizeItemScript overSizeItemScript = instanceOfObject.GetComponent<OverSizeItemScript>();
        if (!overSizeItemScript.addedToParent)
        {
            Destroy(instanceOfObject);
        }
        
    }
}
