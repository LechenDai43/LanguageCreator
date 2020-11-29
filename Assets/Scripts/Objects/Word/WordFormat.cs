using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordFormat
{
    public int numOfSyllable;
    public AccentRule[] accentRules;
    public Phoneme specialLeading, specialEnding;
    public Phoneme[] specialVowel, specialConsonant;
    public bool arabicStyle, consonantWithSemivowel, consonantCluster;
    public Phoneme[] vowelHolders;

    public string getDescription()
    {
        string result = "";
        if (arabicStyle)
        {
            result += numOfSyllable.ToString() + " syllables Arabic verb style word";
            if (consonantWithSemivowel)
            {
                result += " semivoweled consonant allowed";
            }
            if (consonantCluster)
            {
                result += " clustered consonant allowed";
            }
        }
        else
        {
            result += numOfSyllable.ToString() + " syllables word ";
            if (accentRules != null)
            {
                result += "with " + accentRules.Length + " accents"; 
            }
            if (specialLeading != null || specialEnding != null)
            {
                result += " affix: ";
                if (specialLeading != null)
                {
                    result += specialLeading.getIPA() + "; ";
                }
                if (specialEnding != null)
                {
                    result += specialEnding.getIPA();
                }

            }
            if (specialVowel != null || specialConsonant != null)
            {
                result += " ";
                if (specialVowel != null)
                {
                    result += "with " + specialVowel.Length.ToString() + " special vowels; ";
                }
                if (specialConsonant != null)
                {
                    result += "with " + specialConsonant.Length.ToString() + " special consonants";
                }
            }
        }
        return result;
    }

    public class AccentRule
    {
        public int position;
        public bool backword;
        public AccentPhone[] accents;
    }
}
