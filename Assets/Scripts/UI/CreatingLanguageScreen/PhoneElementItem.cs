using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneElementItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ProtoPhone phone;
    private RectTransform rectTransform;
    private Vector2 delta;
    public GameObject generatedObject;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        delta = new Vector2(0.0f, 0.0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (phone != null)
        {
            
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        delta += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        delta = new Vector2(0.0f, 0.0f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition -= delta;
    }
}
