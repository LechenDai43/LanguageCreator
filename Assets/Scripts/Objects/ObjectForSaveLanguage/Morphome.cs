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
}
