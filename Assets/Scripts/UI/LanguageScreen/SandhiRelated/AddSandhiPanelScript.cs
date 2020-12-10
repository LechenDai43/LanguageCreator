using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddSandhiPanelScript : MonoBehaviour
{
    public Text name;
    public GameObject sandhiConent, sandhiEditor;
    public GameObject prefabedSandhiBanner;
    public GameObject prefabedButton;
    
    // public List<Sandhi> sandhis;

    public LanguagePanelScript parentScript;

    // objects in editor
    public GameObject upperContent, lowerContent;
    public GameObject prefabedDescriptionContainer, prefabedMetaBlock, prefabedDescription;

    public List<string> mannersU = new List<string>(), positionsU = new List<string>(), opennessU = new List<string>(), roundednessU = new List<string>();
    public List<string> mannersL = new List<string>(), positionsL = new List<string>(), opennessL = new List<string>(), roundednessL = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        mannersL.Add("unchanged");
        positionsL.Add("unchanged");
        opennessL.Add("unchanged");
        roundednessL.Add("unchanged");

        populateManner(mannersL);
        populateManner(mannersU);

        populatePosition(positionsL);
        populatePosition(positionsU);

        populateOpen(opennessL);
        populateOpen(opennessU);

        populateRound(roundednessL);
        populateRound(roundednessU);
    }

    private void populateManner(List<string> list)
    {
        list.Add("nasal");
        list.Add("nasal asp");
        list.Add("plosive");
        list.Add("plosive asp");
        list.Add("plosive voi");
        list.Add("plosive a&v");
        list.Add("fricative");
        list.Add("fricative voi");
        list.Add("sibilant");
        list.Add("sibilant voi");
        list.Add("flap");
        list.Add("trill");
        list.Add("lateral");
        list.Add("ejective plosive");
        list.Add("ejective fricative");
        list.Add("ejective sibilant");
        list.Add("click");
        list.Add("click voi");
        list.Add("implosive");
        list.Add("implosive voi");
    }

    private void populatePosition (List<string> list)
    {
        list.Add("bilabial");
        list.Add("labiodental");
        list.Add("linguolabial");
        list.Add("dental");
        list.Add("alveolar");
        list.Add("postalveolar");
        list.Add("retroflex");
        list.Add("palatal");
        list.Add("velar");
        list.Add("uvular");
        list.Add("glottal");
    }

    private void populateOpen (List<string> list)
    {
        list.Add("open");
        list.Add("near-open");
        list.Add("half-open");
        list.Add("middle");
        list.Add("half-close");
        list.Add("near-close");
        list.Add("close");
    }

    private void populateRound(List<string> list)
    {
        list.Add("rounded front");
        list.Add("unrounded front");
        list.Add("rounded central");
        list.Add("unrounded central");
        list.Add("rounded back");
        list.Add("unrounded back");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveButton()
    {
        if (name.text == null || name.text.Equals(""))
        {
            return;
        }
        string subName = name.text;

        SoundChange parentNode = parentScript.changingRule;
        SoundChange thisNode = new SoundChange();

        thisNode.Name = subName;
        thisNode.Branches = new string[0];

        Sandhi[] sandhiList = new Sandhi[sandhiConent.transform.childCount];
        for (int i = 0; i < sandhiConent.transform.childCount; i++)
        {
            SandhiBannerScript temp = sandhiConent.transform.GetChild(i).GetComponent<SandhiBannerScript>();
            sandhiList[i] = temp.sandhi;
        }

        thisNode.Rules = sandhiList;
        thisNode.Directory = parentNode.Directory + "-" + parentNode.Count.ToString();
        thisNode.Count = 0;

        string langaugeData = JsonUtility.ToJson(thisNode);
        System.IO.File.WriteAllText(Application.dataPath + thisNode.Directory + ".soundChange", langaugeData);

        parentNode.addBranches(thisNode.Directory + ".soundChange");
        parentNode.Count++;

        GameObject instanceButton = (GameObject)Instantiate(prefabedButton, parentScript.subdivisionView.transform);
        SubLanguageButton buttonScript = instanceButton.GetComponent<SubLanguageButton>();
        buttonScript.buttonText.text = thisNode.Name;
        buttonScript.parentScript = parentScript;
        buttonScript.soundChange = thisNode;


        cancelButton();

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
        targetDesCon.GetComponent<DescriptionContainerScript>().parentScript = this;
        targetMetBlo.GetComponent<MetaBlockScript>().parent = this;
        targetDesCon.GetComponent<DescriptionContainerScript>().changeDropdown(mannersU, positionsU, opennessU, roundednessU);
        targetMetBlo.GetComponent<MetaBlockScript>().changeDropdown(mannersL, positionsL, opennessL, roundednessL);
    }

    public void editorSaveButton()
    {
        GameObject banner = (GameObject)Instantiate(prefabedSandhiBanner, sandhiConent.transform);
        SandhiBannerScript bannerScript = banner.transform.GetComponent<SandhiBannerScript>();

        List<Sandhi.Description> upperList = new List<Sandhi.Description>();
        List<Sandhi.MetaBlock> lowerList = new List<Sandhi.MetaBlock>();

        for (int i = 0; i < upperContent.transform.childCount; i++)
        {
            DescriptionContainerScript upper = upperContent.transform.GetChild(i).GetComponent<DescriptionContainerScript>();
            upperList.Add(upper.packUp());

            MetaBlockScript lower = lowerContent.transform.GetChild(i).GetComponent<MetaBlockScript>();
            lowerList.Add(lower.packUp());
        }

        bannerScript.sandhi = new Sandhi();
        bannerScript.sandhi.Target = upperList.ToArray();
        bannerScript.sandhi.Result = lowerList.ToArray();

        editorCancelButton();
    }

    public void editorAddButton()
    {
        GameObject targetDesCon = (GameObject)Instantiate(prefabedDescriptionContainer, upperContent.transform);
        GameObject targetMetBlo = (GameObject)Instantiate(prefabedMetaBlock, lowerContent.transform);
        targetDesCon.GetComponent<DescriptionContainerScript>().correspondingBlock = targetMetBlo.GetComponent<MetaBlockScript>();
        targetDesCon.GetComponent<DescriptionContainerScript>().parentScript = this;
        targetMetBlo.GetComponent<MetaBlockScript>().parent = this;
        targetDesCon.GetComponent<DescriptionContainerScript>().changeDropdown(mannersU, positionsU, opennessU, roundednessU);
        targetMetBlo.GetComponent<MetaBlockScript>().changeDropdown(mannersL, positionsL, opennessL, roundednessL);
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

        sandhiEditor.SetActive(false);
    }

    public void targetMainDropdownChanged()
    {
        while (mannersL.Count > mannersU.Count + 1)
        {
            mannersL.RemoveAt(mannersL.Count - 1);
        }

        while (positionsL.Count > positionsU.Count + 1)
        {
            positionsL.RemoveAt(positionsL.Count - 1);
        }

        while (opennessL.Count > opennessU.Count + 1)
        {
            opennessL.RemoveAt(opennessL.Count - 1);
        }

        while (roundednessL.Count > roundednessU.Count + 1)
        {
            roundednessL.RemoveAt(roundednessL.Count - 1);
        }

        for (int i = 0; i < upperContent.transform.childCount; i++)
        {
            DescriptionScript tem = upperContent.transform.GetChild(i).GetComponent<DescriptionContainerScript>().child;
            string text = "same as " + (i + 1).ToString();


            if (tem.mainSelector.value == 1)
            {
                mannersL.Add(text);
                positionsL.Add(text);
            }
            else if (tem.mainSelector.value == 2)
            {
                opennessL.Add(text);
                roundednessL.Add(text);
            }
            else if (tem.mainSelector.value == 0)
            {
                if (tem.oneConsonent.isOn || tem.consonantCluster.isOn)
                {
                    mannersL.Add(text);
                    positionsL.Add(text);
                }
                else if (tem.oneVowel.isOn || tem.vowelCluster.isOn)
                {
                    opennessL.Add(text);
                    roundednessL.Add(text);
                }
            }
        }

        for (int i = 0; i < lowerContent.transform.childCount; i++)
        {
            MetaBlockScript tem = lowerContent.transform.GetChild(i).GetComponent<MetaBlockScript>();

            tem.changeDropdown(mannersL, positionsL, opennessL, roundednessL);
        }
    }

    public void changePosition()
    {
        for (int i = 0; i < upperContent.transform.childCount; i++)
        {
            DescriptionContainerScript tem = upperContent.transform.GetChild(i).GetComponent<DescriptionContainerScript>();
            tem.correspondingBlock.position = i;
        }
    }
}
