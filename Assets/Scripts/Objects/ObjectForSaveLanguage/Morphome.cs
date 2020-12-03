using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class Morphome
{
    public int SyllableNumber;
    public bool AfroAsian, Affixed, SpecialPhone;
    public SpeechSound Prefix, Suffix;
    public SpeechSound[] SpecialVowels, SpecialConsonants;
    public SpeechSound[] HolderVowels;
    public bool SemivoweledConsonant, ClusteredConsonant;
    public ToneRequiement[] Tones;

    [Serializable]
    public class ToneRequiement
    {
        public int Position;
        public Phone[] Accents;
    }

    public string getDescription()
    {
        string result = "";
        if (AfroAsian)
        {
            result += SyllableNumber.ToString() + " syllables Arabic verb style word";
            if (SemivoweledConsonant)
            {
                result += " semivoweled consonant allowed";
            }
            if (ClusteredConsonant)
            {
                result += " clustered consonant allowed";
            }
        }
        else
        {
            result += SyllableNumber.ToString() + " syllables word ";
            if (Tones != null && Tones.Length > 0)
            {
                result += "with " + Tones.Length + " accents";
            }
            if ((Prefix != null && Prefix.Phonemes.Length > 0) || (Suffix != null && Suffix.Phonemes.Length > 0))
            {
                result += " affix: ";
                if ((Prefix != null && Prefix.Phonemes.Length > 0))
                {
                    result += Prefix.Transliteration + "; ";
                }
                if ((Suffix != null && Suffix.Phonemes.Length > 0))
                {
                    result += Suffix.Transliteration;
                }

            }
            if ((SpecialVowels != null && SpecialVowels.Length > 0) || (SpecialConsonants != null && SpecialConsonants.Length > 0))
            {
                result += " ";
                if ((SpecialVowels != null && SpecialVowels.Length > 0))
                {
                    result += "with " + SpecialVowels.Length.ToString() + " special vowels; ";
                }
                if ((SpecialConsonants != null && SpecialConsonants.Length > 0))
                {
                    result += "with " + SpecialVowels.Length.ToString() + " special consonants";
                }
            }
        }
        return result;
    }
}
