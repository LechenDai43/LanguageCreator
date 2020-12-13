using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaBlockScript : MonoBehaviour
{
    public GameObject scrollView, removeButton;
    public GameObject prefabedItemt;
    public AddSandhiPanelScript parent;
    public int position = -1;

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
        if (scrollView.transform.childCount > 0)
        {
            Transform tem = scrollView.transform.GetChild(scrollView.transform.childCount - 1);
            tem.parent = null;
            Destroy(tem.gameObject);

            if (scrollView.transform.childCount == 0)
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
            DescriptionScript temp = scrollView.transform.GetChild(i).GetComponent<DescriptionScript>();
            Sandhi.Description desc = new Sandhi.Description();

            if (temp.mainSelector.value == 0)
            {
                desc.Unchanged = true;
            }
            else if (temp.mainSelector.value == 1)
            {
                desc.Type = "Consonant";

                if (temp.mannerSelector.value == 0)
                {
                    desc.MOA = position;
                    desc.DimensionOne = true;
                }
                else if (temp.mannerSelector.value <= 20)
                {
                    desc.MOA = temp.mannerSelector.value - 1;
                }
                else
                {
                    string strKey = temp.mannerSelector.options[temp.mannerSelector.value].text;
                    int intKey = int.Parse(strKey.Substring(strKey.LastIndexOf(" ") + 1));
                    desc.MOA = intKey - 1;
                    desc.DimensionOne = true;
                }


                if (temp.positionSelector.value == 0)
                {
                    desc.POA = position;
                    desc.DimensionTwo= true;
                }
                else if (temp.positionSelector.value <= 11)
                {
                    desc.POA = temp.positionSelector.value - 1;
                }
                else
                {
                    string strKey = temp.positionSelector.options[temp.positionSelector.value].text;
                    int intKey = int.Parse(strKey.Substring(strKey.LastIndexOf(" ") + 1));
                    // desc.MOA = intKey - 1;
                    desc.POA = intKey - 1;
                    desc.DimensionTwo = true;
                }
            }
            else if (temp.mainSelector.value == 2)
            {
                desc.Type = "Vowel";

                if (temp.opennessSelector.value == 0)
                {
                    desc.Openness = position;
                    desc.DimensionOne = true;
                }
                else if (temp.opennessSelector.value <= 7)
                {
                    desc.Openness = temp.opennessSelector.value - 1;
                }
                else
                {
                    string strKey = temp.opennessSelector.options[temp.opennessSelector.value].text;
                    int intKey = int.Parse(strKey.Substring(strKey.LastIndexOf(" ") + 1));
                    // desc.MOA = intKey - 1;
                    desc.Openness = intKey - 1;
                    desc.DimensionOne = true;
                }


                if (temp.roundednessSelector.value == 0)
                {
                    desc.Roundness = position;
                    desc.DimensionTwo = true;
                }
                else if (temp.roundednessSelector.value <= 6)
                {
                    desc.Roundness = temp.roundednessSelector.value - 1;
                }
                else
                {
                    string strKey = temp.roundednessSelector.options[temp.roundednessSelector.value].text;
                    int intKey = int.Parse(strKey.Substring(strKey.LastIndexOf(" ") + 1));
                    // desc.MOA = intKey - 1;
                    desc.Roundness = intKey - 1;
                    desc.DimensionTwo = true;
                }
            }
            else if (temp.mainSelector.value == 3)
            {
                desc.Type = "Glide";

                desc.GlideIndex = temp.glideSelector.value;
            }

            result.Descriptions[i] = desc;
        }

        return result;
    }
}
