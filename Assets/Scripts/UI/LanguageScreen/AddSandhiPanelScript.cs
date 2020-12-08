using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSandhiPanelScript : MonoBehaviour
{
    public Text name;
    public GameObject sandhiConent, sandhiEditor;
    public GameObject prefabedSandhiBanner;
    
    public List<Sandhi> sandhis;

    public LanguagePanelScript parentScript;

    // objects in editor
    public GameObject upperContent, lowerContent;


    // Start is called before the first frame update
    void Start()
    {
        sandhis = new List<Sandhi>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveButton()
    {

    }

    public void cancelButton()
    {
        Destroy(this.gameObject);
    }

    public void addSandhiButton()
    {

    }

    public void editorSaveButton()
    {

    }

    public void editorAddButton()
    {

    }

    public void editorCancelButton()
    {

    }
}
