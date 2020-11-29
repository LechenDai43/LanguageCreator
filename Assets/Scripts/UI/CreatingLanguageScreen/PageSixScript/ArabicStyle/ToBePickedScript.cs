using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToBePickedScript : MonoBehaviour
{
    public GameObject prefabedPanel;
    public Text IPA, letter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonPressed()
    {
        GameObject instancePhonePicker = (GameObject)Instantiate(prefabedPanel, transform.parent.parent.parent.parent.parent);
        AllVowelToPickScript script = instancePhonePicker.GetComponent<AllVowelToPickScript>();
        script.parentEle = this;
    }
}
