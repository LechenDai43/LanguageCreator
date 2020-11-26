using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScrollViewPageFour : MonoBehaviour
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

    // Add the items onto the screen
    public void populate()
    {
        GameObject newItem;
        Manager manager = Object.FindObjectOfType<Manager>();

        // Get the semivowel phone pool from the manager
        PhoneManager phoneManager = manager.phoneManager;
        SemivowelPhone[] semivowelPhones = phoneManager.semivowelPool;
        Debug.Log(semivowelPhones.Length);

        // Populate the scroll view
        for (int i = 0; i < semivowelPhones.Length; i++)
        {
            GameObject oneItem = (GameObject)Instantiate(prefabedItem, transform);
            oneItem.transform.GetChild(0).GetComponent<Text>().text = semivowelPhones[i].IPA;
            oneItem.transform.GetComponent<SemivowelItemScript>().phone = semivowelPhones[i];
        }
    }
}
