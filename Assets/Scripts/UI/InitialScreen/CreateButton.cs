using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreateButton : MonoBehaviour
{
    public void redirectToCreatingLanguageScreen()
    {
        SceneManager.LoadScene("CreatingLanguageScreen");
    }
}
