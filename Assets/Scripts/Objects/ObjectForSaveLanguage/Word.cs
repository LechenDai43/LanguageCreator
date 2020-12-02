using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class Word
{
    public SpeechSound[] Phonemes;
    public SpeechSound Suffix, Prefix;
    public bool Suffixed, Prefixed;


    public string ToString()
    {
        string result = "";
        if (Prefixed)
        {
            result += Prefix.Transliteration;
        }
        foreach (SpeechSound p in Phonemes)
        {
            result += p.Transliteration;
        }
        if (Suffixed)
        {
            result += Suffix.Transliteration;
        }
        return result;
    }
}
