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
        
    }

    public void OnDrop (PointerEventData eventData)
    {
        Debug.Log("Lalala");
        if (eventData.pointerDrag != null)
        {
            Debug.Log(eventData.pointerDrag);
        }
    }
}
