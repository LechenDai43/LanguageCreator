using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager
{
    public Phoneme[] consonantBoW, consonantBoS, consonantEoS, consonantEoW;
    public Phoneme[] vowelAS, vowelUS;
    public AccentRule[] accentRules;
    private double sumBow, sumBos, sumEoS, sumEow, sumAS, sumUS;
    public string languageName;

    public LanguageManager()
    {
        consonantBoW = new Phoneme[0];
        consonantBoS = new Phoneme[0];
        consonantEoS = new Phoneme[0];
        consonantEoW = new Phoneme[0];
        vowelAS = new Phoneme[0]; 
        vowelUS = new Phoneme[0];
    }

    public void setBoW(Phoneme[] arr)
    {
        consonantBoW = arr;
        sumBow = 0.0;
        foreach (Phoneme p in arr)
        {
            sumBow += p.frequency;
        }
    }

    public void setBoS(Phoneme[] arr)
    {
        consonantBoS = arr;
        sumBow = 0.0;
        foreach (Phoneme p in arr)
        {
            sumBow += p.frequency;
        }
    }

    public void setEoW(Phoneme[] arr)
    {
        consonantEoW = arr;
        sumBow = 0.0;
        foreach (Phoneme p in arr)
        {
            sumBow += p.frequency;
        }
    }

    public void setEoS(Phoneme[] arr)
    {
        consonantEoS = arr;
        sumBow = 0.0;
        foreach (Phoneme p in arr)
        {
            sumBow += p.frequency;
        }
    }

    public void setAS(Phoneme[] arr)
    {
        vowelAS = arr;
        sumBow = 0.0;
        foreach (Phoneme p in arr)
        {
            sumBow += p.frequency;
        }
    }

    public void setUS(Phoneme[] arr)
    {
        vowelUS = arr;
        sumBow = 0.0;
        foreach (Phoneme p in arr)
        {
            sumBow += p.frequency;
        }
    }
}
