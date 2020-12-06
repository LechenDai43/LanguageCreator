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
        others = new Vocabulary[currentLanguage.Generals.Length == null? 0: currentLanguage.Generals.Length];
        verbs = new Vocabulary[currentLanguage.Verbs.Length == null ? 0 : currentLanguage.Verbs.Length];
        nouns = new Vocabulary[currentLanguage.Nouns.Length == null ? 0 : currentLanguage.Nouns.Length];
        adjectives = new Vocabulary[currentLanguage.Adjectives.Length == null ? 0 : currentLanguage.Adjectives.Length];

        string directoryPath = Application.dataPath + currentLanguage.Directory;
        DirectoryInfo dir = new DirectoryInfo(directoryPath);
        FileInfo[] fileInfos = dir.GetFiles("*.*");
        Debug.Log(fileInfos.Length);
        foreach (FileInfo ff in fileInfos)
        {
            Debug.Log(ff.Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
