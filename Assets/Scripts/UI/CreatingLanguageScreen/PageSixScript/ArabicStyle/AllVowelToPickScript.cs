using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllVowelToPickScript : MonoBehaviour
{
    public List<Toggle> allToggles = new List<Toggle>();

    // Start is called before the first frame update
    void Start()
    {
        Manager manager = Object.FindObjectOfType<Manager>();
        LanguageManager languageManager = manager.languageManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onCheckButtonPressed()
    {

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
