using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LowerPartPageFour : MonoBehaviour, IDropHandler
{
    public int row, column;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * column / 3, width / 3);
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height * row / 2, height / 2);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            SemivowelItemScript draggedObject = eventData.pointerDrag.GetComponent<SemivowelItemScript>();
            GameObject overSizeObject = draggedObject.instanceOfObject;
            if (overSizeObject != null)
            {
                // SasVToggle, SasCToggle
                int index = row * 3 + column;
                if (index < 4 && draggedObject.SasCToggle.isOn)
                {
                    overSizeObject.transform.parent = this.transform.GetChild(0).transform.GetChild(0).transform;
                    overSizeObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    OverSizePageFourScript overSizeItemScript = overSizeObject.GetComponent<OverSizePageFourScript>();
                    overSizeItemScript.addedToParent = true;
                }
                else if (index > 3 && draggedObject.SasVToggle.isOn)
                {
                    overSizeObject.transform.parent = this.transform.GetChild(0).transform.GetChild(0).transform;
                    overSizeObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    OverSizePageFourScript overSizeItemScript = overSizeObject.GetComponent<OverSizePageFourScript>();
                    overSizeItemScript.addedToParent = true;
                }
            }
        }
    }
}
