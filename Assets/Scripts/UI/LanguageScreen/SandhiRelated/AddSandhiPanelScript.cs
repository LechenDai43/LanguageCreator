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
    public GameObject prefabedDescriptionContainer, prefabedMetaBlock, prefabedDescription;


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
        sandhiEditor.SetActive(true);

        GameObject targetDesCon = (GameObject)Instantiate(prefabedDescriptionContainer, upperContent.transform);
        GameObject targetMetBlo = (GameObject)Instantiate(prefabedMetaBlock, lowerContent.transform);
        targetDesCon.GetComponent<DescriptionContainerScript>().correspondingBlock = targetMetBlo.GetComponent<MetaBlockScript>();
    }

    public void editorSaveButton()
    {

    }

    public void editorAddButton()
    {
        GameObject targetDesCon = (GameObject)Instantiate(prefabedDescriptionContainer, upperContent.transform);
        GameObject targetMetBlo = (GameObject)Instantiate(prefabedMetaBlock, lowerContent.transform);
        targetDesCon.GetComponent<DescriptionContainerScript>().correspondingBlock = targetMetBlo.GetComponent<MetaBlockScript>();
    }

    public void editorCancelButton()
    {
        while (upperContent.transform.childCount > 0)
        {
            Transform tem = upperContent.transform.GetChild(0);
            tem.parent = null;
            Destroy(tem.gameObject);
        }

        while (lowerContent.transform.childCount > 0)
        {
            Transform tem = lowerContent.transform.GetChild(0);
            tem.parent = null;
            Destroy(tem.gameObject);
        }
    }
}
