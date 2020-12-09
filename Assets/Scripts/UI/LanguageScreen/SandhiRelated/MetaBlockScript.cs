using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBlockScript : MonoBehaviour
{
    public GameObject scrollView, removeButton;
    public GameObject prefabedItemt;
    public AddSandhiPanelScript parent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeDropdown(List<string> manner, List<string> position, List<string> openness, List<string> roundedness)
    {
        for (int i = 0; i < scrollView.transform.childCount; i++)
        {
            DescriptionScript tem = scrollView.transform.GetChild(i).GetComponent<DescriptionScript>();

            tem.mannerSelector.ClearOptions();
            tem.mannerSelector.AddOptions(manner);

            tem.positionSelector.ClearOptions();
            tem.positionSelector.AddOptions(position);

            tem.opennessSelector.ClearOptions();
            tem.opennessSelector.AddOptions(openness);

            tem.roundednessSelector.ClearOptions();
            tem.roundednessSelector.AddOptions(roundedness);
        }
    }

    public void childChangeToggle(DescriptionScript source)
    {
        source.generic.SetActive(false);
    }

    public void addItem()
    {
        GameObject description = (GameObject)Instantiate(prefabedItemt, scrollView.transform);
        DescriptionScript tem = description.GetComponent<DescriptionScript>();
        tem.block = this;

        tem.mannerSelector.ClearOptions();
        tem.mannerSelector.AddOptions(parent.mannersL);

        tem.positionSelector.ClearOptions();
        tem.positionSelector.AddOptions(parent.positionsL);

        tem.opennessSelector.ClearOptions();
        tem.opennessSelector.AddOptions(parent.opennessL);

        tem.roundednessSelector.ClearOptions();
        tem.roundednessSelector.AddOptions(parent.roundednessL);

        if (scrollView.transform.childCount > 1)
        {
            removeButton.SetActive(true);
        }
    }

    public void removeItem()
    {
        if (scrollView.transform.childCount > 1)
        {
            Transform tem = scrollView.transform.GetChild(scrollView.transform.childCount - 1);
            tem.parent = null;
            Destroy(tem.gameObject);

            if (scrollView.transform.childCount == 1)
            {
                removeButton.SetActive(false);
            }
        }
    }

    public Sandhi.MetaBlock packUp()
    {
        Sandhi.MetaBlock result = new Sandhi.MetaBlock();

        result.Descriptions = new Sandhi.Description[scrollView.transform.childCount];
        for (int i = 0; i < scrollView.transform.childCount; i++)
        {

        }
    }
}
