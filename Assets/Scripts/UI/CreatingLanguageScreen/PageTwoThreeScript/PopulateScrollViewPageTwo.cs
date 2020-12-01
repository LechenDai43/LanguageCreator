using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateScrollViewPageTwo : MonoBehaviour
{
    public GameObject prefabedItem;
    public GameObject emptyItem;
    public GameObject tagItem;

    // Start is called before the first frame update
    void Start()
    {
        populate();
    }

    // Add the items onto the screen
    public void populate()
    {
        GameObject newItem;
        Manager manager = Object.FindObjectOfType<Manager>();

        // Get the consonant phone pool from the manager
        PhoneManager phoneManager = manager.phoneManager;
        ConsonantPhone[][] consonantPhones = phoneManager.consonantPool;

        // Try to get any empty row or column
        List<int> emptyRow = new List<int>();
        List<int> emptyColomn = new List<int>();

        // Get empty rows' indeces
        for (int i = 0; i < consonantPhones.Length; i++)
        {
            ConsonantPhone[] row = consonantPhones[i];
            bool empty = true;
            for (int j = 0; j < row.Length; j++)
            {
                if (consonantPhones[i][j] != null)
                {
                    empty = false;
                    break;
                }
            }
            if (empty)
            {
                emptyRow.Add(i);
            }
        }

        // Get empty columns' indeces
        for (int j = 0; j < consonantPhones[0].Length; j++)
        {
            bool empty = true;
            for (int i = 0; i < consonantPhones.Length; i++)
            {
                if (consonantPhones[i][j] != null)
                {
                    empty = false;
                    break;
                }
            }
            if (empty)
            {
                emptyColomn.Add(j);
            }
        }

        // Set the column constraint
        int validColumnNum = consonantPhones[0].Length - emptyColomn.Count;
        GetComponent<GridLayoutGroup>().constraintCount = validColumnNum + 1;

        // Generate the top row
        newItem = (GameObject)Instantiate(prefabedItem, transform);
        newItem.transform.GetChild(0).GetComponent<Text>().text = phoneManager.emptyConsonant.IPA;
        newItem.transform.GetComponent<PhoneElementItem>().phone = phoneManager.emptyConsonant;
        for (int i = 0; i < consonantPhones[0].Length; i++)
        {
            bool inEmptyColumn = false;
            foreach (int index in emptyColomn)
            {
                if (index == i)
                {
                    inEmptyColumn = true;
                    break;
                }
            }

            if (inEmptyColumn)
            {
                continue;
            }

            newItem = (GameObject)Instantiate(tagItem, transform);
                // TODO...
                // Change the text according to the locale
        }

        // Populate the view scroll
        for (int i = 0; i < consonantPhones.Length; i++)
        {
            // Check if the row is an empty row
            bool isEmptyRow = false;
            foreach (int index in emptyRow)
            {
                if (index == i)
                {
                    isEmptyRow = true;
                    break;
                }
            }

            // If the row is an empty row, then skip
            if (isEmptyRow)
            {
                continue;
            }

            // If the row is not an empty rwo, then work on it
            newItem = (GameObject)Instantiate(tagItem, transform);
                // TODO...
                // Change the text according to the locale
            ConsonantPhone[] row = consonantPhones[i];
            for (int j = 0; j < row.Length; j++)
            {
                // Check if the cell is in an empty column
                bool inEmptyColumn = false;
                foreach (int index in emptyColomn)
                {
                    if (index == j)
                    {
                        inEmptyColumn = true;
                        break;
                    }
                }

                // If the cell is in an empty column, then skip
                if (inEmptyColumn)
                {
                    continue;
                }

                // If the cell is not in an empty column, then work on it
                if (row[j] != null)
                {
                    string incomingText = row[j].IPA;
                    newItem = (GameObject)Instantiate(prefabedItem, transform);
                    newItem.transform.GetChild(0).GetComponent<Text>().text = incomingText;
                    newItem.transform.GetComponent<PhoneElementItem>().phone = row[j];
                }
                else
                {
                    newItem = (GameObject)Instantiate(emptyItem, transform);
                }
            }
        }
    }
}
