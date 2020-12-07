using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class LanguageScreenManager : MonoBehaviour
{
    public Vocabulary[] verbs, nouns, others, adjectives;
    public GameObject prefabedPanel;
    public List<GameObject> listOfPanel;
    

    // Start is called before the first frame update
    void Start()
    {
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
        LanguageFamily currentLanguage = manager.currentLanguage;
        others = new Vocabulary[currentLanguage.Generals == null? 0: currentLanguage.Generals.Length];
        verbs = new Vocabulary[currentLanguage.Verbs == null ? 0 : currentLanguage.Verbs.Length];
        nouns = new Vocabulary[currentLanguage.Nouns == null ? 0 : currentLanguage.Nouns.Length];
        adjectives = new Vocabulary[currentLanguage.Adjectives == null ? 0 : currentLanguage.Adjectives.Length];

        string directoryPath = Application.dataPath + currentLanguage.Directory;
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        FileInfo[] fileInfos = dir.GetFiles("*.vocabulary");
        foreach (FileInfo ff in fileInfos)
        {
            Debug.Log(ff.FullName);

            string front = ff.Name.Substring(0, ff.Name.LastIndexOf("-"));
            string behind = ff.Name.Substring(ff.Name.LastIndexOf("-") + 1);

            try
            {
                int index = int.Parse(behind);
                Vocabulary[] holder = null;
                if (front.Equals("others"))
                {
                    holder = others;
                }
                else if (front.Equals("verbs"))
                {
                    holder = verbs;
                }
                else if (front.Equals("nouns"))
                {
                    holder = nouns;
                }
                else if (front.Equals("adjectives"))
                {
                    holder = adjectives;
                }

                if (holder != null)
                {
                    string consonantPath = Application.dataPath + ff.FullName;
                    StreamReader sr = new StreamReader(consonantPath);
                    string content = sr.ReadToEnd();
                    sr.Close();
                    Vocabulary vocabulary = JsonUtility.FromJson<Vocabulary>(content);

                    if (currentLanguage.Name.Equals(vocabulary.Name))
                    {
                        holder[index] = vocabulary;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        populateUndefined(others);
        populateUndefined(verbs);
        populateUndefined(nouns);
        populateUndefined(adjectives);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveVocabulary()
    {
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
        LanguageFamily currentLanguage = manager.currentLanguage;
        string directoryPath = Application.dataPath + currentLanguage.Directory;

        for (int i = 0; i < verbs.Length; i++)
        {
            string filePath = directoryPath + "/verbs-" + i.ToString() + ".vocabulary";

            string langaugeData = JsonUtility.ToJson(verbs[i]);
            System.IO.File.WriteAllText(filePath, langaugeData);
        }

        for (int i = 0; i < nouns.Length; i++)
        {
            string filePath = directoryPath + "/nouns-" + i.ToString() + ".vocabulary";

            string langaugeData = JsonUtility.ToJson(nouns[i]);
            System.IO.File.WriteAllText(filePath, langaugeData);
        }

        for (int i = 0; i < others.Length; i++)
        {
            string filePath = directoryPath + "/others-" + i.ToString() + ".vocabulary";

            string langaugeData = JsonUtility.ToJson(others[i]);
            System.IO.File.WriteAllText(filePath, langaugeData);
        }

        for (int i = 0; i < adjectives.Length; i++)
        {
            string filePath = directoryPath + "/adjectives-" + i.ToString() + ".vocabulary";

            string langaugeData = JsonUtility.ToJson(adjectives[i]);
            System.IO.File.WriteAllText(filePath, langaugeData);
        }
    }

    private void populateUndefined(Vocabulary[] input)
    {
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
        LanguageFamily currentLanguage = manager.currentLanguage;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == null)
            {
                Vocabulary temp = new Vocabulary();
                temp.Name = currentLanguage.Name;
                input[i] = temp;
            }
        }
    }


}
