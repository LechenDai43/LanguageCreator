using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class LanguageFamily
{
    public string Name;
    public SpeechSound[] WordOnsets, WordCodas, SyllableOnsets, SyllableCodas, StressedVowels, UnstressedVowels;
    public Phone[] Accents;
    public Morphome[] Generals, Verbs, Nouns, Adjectives;
    public SoundChange[] Branches;
    
    
    
}
