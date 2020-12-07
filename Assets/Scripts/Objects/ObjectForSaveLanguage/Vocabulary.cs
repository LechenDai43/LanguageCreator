using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class Vocabulary
{
    public string Name;
    public Word[] Word = new Word[0];

    public void addWords(Word[] newWord)
    {
        List<Word> total = new List<Word>();
        foreach (Word w in Word)
        {
            total.Add(w);
        }
        foreach (Word w in newWord)
        {
            total.Add(w);
        }

        Word = total.ToArray();
    }
}
