using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordFormat
{
    public int numOfSyllable;
    public AccentRule[] accentRules;
    public Phoneme specialLeading, specialEnding;
    public Phoneme[] specialVowel, specialConsonant;
    public bool arabicStyle, consonantWithSemivowel;
    public Phoneme[] vowelHolders;
    public PartOfSpeech wordType;


    public class AccentRule
    {
        public int position;
        public bool backword;
        public AccentPhone[] accents;
    }

    public enum PartOfSpeech
    {
        Verb,
        Noun,
        Adjective,
        Other
    }
}
