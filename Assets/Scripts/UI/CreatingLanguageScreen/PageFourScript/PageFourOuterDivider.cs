using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageFourOuterDivider : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;

        if (index == 0)
        {
            this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, height / 3);
        }
        else if (index == 1)
        {
            this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3, height * 2 / 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;

        if (index == 0)
        {
            this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, height / 3);
        }
        else if (index == 1)
        {
            this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height / 3, height * 2 / 3);
        }
    }
}
