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
        List<string> last = new List<string>();
        last.Add("any");

        child.mannerSelector.ClearOptions();
        child.mannerSelector.AddOptions(manner);
        child.mannerSelector.AddOptions(last);

        child.positionSelector.ClearOptions();
        child.positionSelector.AddOptions(position);
        child.positionSelector.AddOptions(last);


        child.opennessSelector.ClearOptions();
        child.opennessSelector.AddOptions(openness);
        child.opennessSelector.AddOptions(last);

        child.roundednessSelector.ClearOptions();
        child.roundednessSelector.AddOptions(roundedness);
        child.roundednessSelector.AddOptions(last);
    }

    public void removeComponent()
    {
        if (correspondingBlock != null)
        {
            correspondingBlock.transform.parent = null;
            Destroy(correspondingBlock.transform.gameObject);
        }
        this.transform.parent = null;
        parentScript.changePosition();
        parentScript.targetMainDropdownChanged();
        Destroy(this.transform.gameObject);
    }

    public Sandhi.Description packUp()
    {
        Sandhi.Description result = new Sandhi.Description();

        if (child.mainSelector.value == 0)
        {
            if (child.initialPosition.isOn)
            {
                result.Initial = true;
            }

            if (child.endingPosition.isOn)
            {
                result.Terminal = true;
            }

            if (child.oneConsonent.isOn || child.consonantCluster.isOn)
            {
                result.Type = "Consonant";
            }

            if (child.oneVowel.isOn || child.vowelCluster.isOn)
            {
                result.Type = "Vowel";
            }

            if (child.oneConsonent.isOn || child.oneVowel.isOn)
            {
                result.SingleHolder = true;
            }

            if (child.consonantCluster.isOn || child.vowelCluster.isOn)
            {
                result.MultipleHolder = true;
            }
        }
        else if (child.mainSelector.value == 1)
        {
            result.Type = "Consonant";
            result.POA = child.positionSelector.value;
            result.MOA = child.mannerSelector.value;
        }
        else if (child.mainSelector.value == 3)
        {
            result.Type = "Glide";
            result.GlideIndex = child.glideSelector.value;
        }
        else
        {
            result.Type = "Vowel";
            result.Openness = child.opennessSelector.value;
            result.Roundness = child.roundednessSelector.value;
        }


        return result;
    }
}
