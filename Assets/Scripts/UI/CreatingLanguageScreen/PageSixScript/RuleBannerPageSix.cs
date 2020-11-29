using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleBannerPageSix : MonoBehaviour
{
    public Text description;
    public WordFormat format;
    public GameObject prefabedEditor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The functions called when delete button is pressed
    // Destroy this rule banner
    public void handleDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    // The function called when edit button is pressed
    public void handleEditButtonPressed()
    {
        GameObject instanceEditor = (GameObject)Instantiate(prefabedEditor, transform.parent.parent.parent);
        CreateRulePageSix instanceScript = instanceEditor.GetComponent<CreateRulePageSix>();

        instanceScript.oldBanner = this;

        instanceScript.syllableNum = format.numOfSyllable;
        instanceScript.holderOfSylNum.text = format.numOfSyllable.ToString();

        if (format.accentRules != null && format.accentRules.Length > 0)
        {
            instanceScript.accentNum = format.accentRules.Length;
            instanceScript.holderOfAccNum.text = format.accentRules.Length.ToString();


        }
    }
}
