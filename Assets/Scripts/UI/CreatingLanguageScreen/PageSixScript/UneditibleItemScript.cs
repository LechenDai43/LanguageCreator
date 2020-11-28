using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UneditibleItemScript : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public Text IPA, letters;
    public Phoneme phoneme;
    public GameObject prefabedDragged, instanceDragged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        instanceDragged.GetComponent<RectTransform>().anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Create an instantiation of the oversize item
        instanceDragged = (GameObject)Instantiate(prefabedDragged, transform.parent.parent.parent.parent);
        instanceDragged.transform.position = transform.position;
        instanceDragged.transform.GetComponent<UneditibleItemScript>().IPA.text = this.IPA.text;
        instanceDragged.transform.GetComponent<UneditibleItemScript>().letters.text = this.letters.text;
        instanceDragged.transform.GetComponent<CanvasGroup>().blocksRaycasts = false;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        StartCoroutine(waitToDestroy());

    }

    private IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(instanceDragged);

    }
}
