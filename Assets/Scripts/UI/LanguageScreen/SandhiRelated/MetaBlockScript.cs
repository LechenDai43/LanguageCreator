using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBlockScript : MonoBehaviour
{
    public GameObject scrollView;

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
}
