using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LowerHalfSubCanvas : MonoBehaviour, IDropHandler
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * index / 4, width / 4);

    }

    // Update is called once per frame
    void Update()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * index / 4, width / 4);
    }

    public void OnDrop (PointerEventData eventData)
    {
        // Debug.Log("Lalala");
        if (eventData.pointerDrag != null)
        {
            PhoneElementItem draggedObject = eventData.pointerDrag.GetComponent<PhoneElementItem>();
            GameObject overSizeObject = draggedObject.instanceOfObject;
            overSizeObject.transform.parent = this.transform.GetChild(0).transform.GetChild(0).transform;
            overSizeObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
            OverSizeItemScript overSizeItemScript = overSizeObject.GetComponent<OverSizeItemScript>();
            overSizeItemScript.addedToParent = true;
            Debug.Log(eventData.pointerDrag);
        }
    }
}
