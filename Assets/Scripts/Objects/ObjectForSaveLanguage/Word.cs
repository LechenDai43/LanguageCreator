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

    public static Word deepCopy (Word other)
    {
        Word copy = new Word();
        copy.Phonemes = new SpeechSound[other.Phonemes.Length];
        for (int i = 0; i < copy.Phonemes.Length; i++)
        {
            copy.Phonemes[i] = SpeechSound.deepCopy(other.Phonemes[i]);
        }
        if (other.Prefix != null)
        {
            copy.Prefix = SpeechSound.deepCopy(other.Prefix);
        }
        if (other.Suffix != null)
        {
            copy.Suffix = SpeechSound.deepCopy(other.Suffix);
        }
        copy.Suffixed = other.Suffixed;
        copy.Prefixed = other.Prefixed;
        return copy;
    }
}
