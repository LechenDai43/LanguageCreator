using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartOfSpeechPageSix : MonoBehaviour
{
    public int index;
    public GameObject thisPanel;

    // Start is called before the first frame update
    void Start()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * (index % 2) / 2, width / 2);
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height * (index / 2) / 2, height / 2);
    }

    // Update is called once per frame
    void Update()
    {
        float height = this.transform.parent.GetComponent<RectTransform>().rect.height;
        float width = this.transform.parent.GetComponent<RectTransform>().rect.width;
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, width * (index % 2) / 2, width / 2);
        this.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, height * (index / 2) / 2, height / 2);
    }

    // Add new rule button function
    public GameObject prefabedPanel;
    public void addNewRuleButtonPressed()
    {
        GameObject instancePanel = (GameObject)Instantiate(prefabedPanel, transform.parent);
    }

    // Handle the button pressed to import rules
    public GameObject[] otherPartOfSpeech; // This is the immediate parent of the rule items
    public void importRuleButtonPressed(int buttonIndex)
    {
        if (buttonIndex >= otherPartOfSpeech.Length)
        {
            return;
        }

        // Todo...
        // Copy the items from the other part of speech to this panel
    }
}
