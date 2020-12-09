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
}
