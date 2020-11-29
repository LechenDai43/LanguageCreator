using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllVowelToPickScript : MonoBehaviour
{
    public List<Toggle> allToggles = new List<Toggle>();
    public Phoneme[] allVowelPhoneme;
    public GameObject prefabedItem;
    public GameObject content;
    public ToBePickedScript parentEle;

    // Start is called before the first frame update
    void Start()
    {
        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;

        List<Phoneme> vowelPhonemesList = new List<Phoneme>();

        // TODO...
        // Add the empty vowel

        foreach (Phoneme pnm in languageManager.vowelAS)
        {
            Debug.Log(pnm.preceding);

            bool hasSornority = false;
            foreach (ProtoPhone pp in pnm.phones)
            {
                hasSornority = hasSornority || pp.Sornority != null || pp.POA != null;
            }

            if (pnm.preceding != null || pnm.successing != null || hasSornority)
            {
                continue;
            }
            else
            {
                Phoneme newPnm = new Phoneme();
                newPnm.addPhone(pnm);
                vowelPhonemesList.Add(newPnm);
            }
        }

        foreach (Phoneme pnm in languageManager.vowelUS)
        {
            Debug.Log(pnm.preceding);

            bool hasSornority = false;
            foreach (ProtoPhone pp in pnm.phones)
            {
                hasSornority = hasSornority || pp.Sornority != null || pp.POA != null;
            }

            if (pnm.preceding != null || pnm.successing != null || hasSornority)
            {
                continue;
            }
            else
            {
                Phoneme newPnm = new Phoneme();
                newPnm.addPhone(pnm);
                vowelPhonemesList.Add(newPnm);
            }
        }

        allVowelPhoneme = vowelPhonemesList.ToArray();

        int count = 0;
        foreach (Phoneme toCreate in vowelPhonemesList)
        {

            GameObject instancePhonePicker = (GameObject)Instantiate(prefabedItem, content.transform);
            PickVowelForArabicStylePageSix script = instancePhonePicker.GetComponent<PickVowelForArabicStylePageSix>();
            allToggles.Add(script.toggle);
            script.index = count;
            script.parent = this;
            script.IPA.text = toCreate.getIPA();
            script.letter.text = toCreate.letters;
            count++;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCheckButtonPressed()
    {
        int count = 0;
        foreach (Toggle t in allToggles)
        {
            if (t.isOn)
            {
                Phoneme selectedPhoneme = allVowelPhoneme[count];
                parentEle.IPA.text = selectedPhoneme.getIPA();
                parentEle.letter.text = selectedPhoneme.letters;
                parentEle.aVowelPhoneme = selectedPhoneme;
                Destroy(this.transform.gameObject);
            }
            count++;
        }
    }

    public void onCancelButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    public void onOneOFToggleChange(int index, bool isToggleOn)
    {
        if (isToggleOn)
        {
            int count = 0;
            foreach (Toggle t in allToggles)
            {
                if (count != index)
                {
                    t.isOn = false;
                }
                count++;
            }
        }
    }
}
