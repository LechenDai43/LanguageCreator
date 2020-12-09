using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionContainerScript : MonoBehaviour
{
    public MetaBlockScript correspondingBlock;
    public DescriptionScript child;
    public AddSandhiPanelScript parentScript;

    // Start is called before the first frame update
    void Start()
    {
        child.container = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void childChangeToggle ()
    {
        parentScript.targetMainDropdownChanged();
    }

    public void changeDropdown(List<string> manner, List<string> position, List<string> openness, List<string> roundedness)
    {
        child.mannerSelector.ClearOptions();
        child.mannerSelector.AddOptions(manner);

        child.positionSelector.ClearOptions();
        child.positionSelector.AddOptions(position);

        child.opennessSelector.ClearOptions();
        child.opennessSelector.AddOptions(openness);

        child.roundednessSelector.ClearOptions();
        child.roundednessSelector.AddOptions(roundedness);
    }
}
