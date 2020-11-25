using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerHalfSubCanvas : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        float x = this.transform.parent.GetComponent<RectTransform>().rect.x;
        float y = this.transform.parent.GetComponent<RectTransform>().rect.y;
        // this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y / 2);
        // this.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * index / 4, width / 4);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
