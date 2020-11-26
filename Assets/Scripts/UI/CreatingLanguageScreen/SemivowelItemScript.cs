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
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        

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
}
