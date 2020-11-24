using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalDropDown : MonoBehaviour
{

    public void ChangeLocale(Dropdown change)
    {
        Manager manager = Object.FindObjectOfType<Manager>();
        manager.changeLocale(change.value);
    }

    
}
