﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LowerHalfSubCanvas : MonoBehaviour, IDropHandler
{
    public int index;
    public int divisor;
    // Start is called before the first frame update
    void Start()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * index / divisor, width / divisor);

    }

    // Update is called once per frame
    void Update()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * index / divisor, width / divisor);
    }

    public void OnDrop (PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            PhoneElementItem draggedObject = eventData.pointerDrag.GetComponent<PhoneElementItem>();
            GameObject overSizeObject = draggedObject.instanceOfObject;
            if (overSizeObject != null)
            {
                overSizeObject.transform.parent = this.transform.GetChild(0).transform.GetChild(0).transform;
                overSizeObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                OverSizeItemScript overSizeItemScript = overSizeObject.GetComponent<OverSizeItemScript>();
                overSizeItemScript.addedToParent = true;
            }
        }
    }
}
