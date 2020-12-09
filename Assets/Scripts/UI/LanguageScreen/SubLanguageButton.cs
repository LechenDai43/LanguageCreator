using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubLanguageButton : MonoBehaviour
{
    public LanguagePanelScript parentScript;
    public Text buttonText;
    public SoundChange soundChange;
    public GameObject prefabedPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleClick()
    {
        GameObject instancePanel = (GameObject)Instantiate(prefabedPanel, parentScript.transform.parent);
        LanguagePanelScript instanceScript = instancePanel.GetComponent<LanguagePanelScript>();
        instanceScript.parent = parentScript.gameObject;
        instanceScript.changingRule = soundChange;

        parentScript.gameObject.SetActive(false);


    }
}
