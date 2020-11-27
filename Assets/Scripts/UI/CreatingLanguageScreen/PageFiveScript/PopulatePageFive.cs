using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulatePageFive : MonoBehaviour
{
    public GameObject prefabedItem;

    // Start is called before the first frame update
    void Start()
    {
        populate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void populate()
    {
        GameObject newItem;
        Manager manager = Object.FindObjectOfType<Manager>();

        // Get the semivowel phone pool from the manager
        PhoneManager phoneManager = manager.phoneManager;
        AccentPhone[] accentPhones = phoneManager.accentPool;

        // Populate the scroll view
        for (int i = 0; i < accentPhones.Length; i++)
        {
            GameObject oneItem = (GameObject)Instantiate(prefabedItem, transform);
            oneItem.transform.GetChild(0).GetComponent<Text>().text = accentPhones[i].IPA;
            oneItem.transform.GetComponent<AccentItem>().phone = accentPhones[i];
        }
    }
}
