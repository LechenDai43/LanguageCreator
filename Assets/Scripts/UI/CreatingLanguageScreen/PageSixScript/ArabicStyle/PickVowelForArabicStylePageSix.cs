﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickVowelForArabicStylePageSix : MonoBehaviour
{
    public Toggle toggle;
    public Text IPA, letter;
    public AllVowelToPickScript parent;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onToggleChange()
    {
        parent.onOneOFToggleChange(index, toggle.isOn);
    }
}
