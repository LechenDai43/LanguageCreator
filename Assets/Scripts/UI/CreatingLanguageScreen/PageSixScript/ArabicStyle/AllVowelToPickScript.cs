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
}
