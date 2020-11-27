using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleBannerPageSix : MonoBehaviour
{
    public Text description;
    public WordFormat format;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // The functions called when delete button is pressed
    // Destroy this rule banner
    public void handleDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    // The function called when edit button is pressed
    public void handleEditButtonPressed()
    {
        // TODO...
        // Reopen a rule panel and populate the information according to the word format
    }
}
