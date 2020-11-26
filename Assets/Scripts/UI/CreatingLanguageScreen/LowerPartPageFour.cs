using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerPartPageFour : MonoBehaviour
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
}
