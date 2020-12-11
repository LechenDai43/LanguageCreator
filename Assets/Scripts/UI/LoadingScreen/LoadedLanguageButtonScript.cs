using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadedLanguageButtonScript : MonoBehaviour
{
    public Text displayedText;
    public LanguageFamily language;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickThisButton()
    {
        if (language != null) {
            Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
            manager.currentLanguage = language;
            SceneManager.LoadScene("LanguageScreen");
        }
    }
}
