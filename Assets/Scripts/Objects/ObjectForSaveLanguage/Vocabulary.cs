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
    public Word[] General = new Word[0], Verb = new Word[0], Noun = new Word[0], Adjective = new Word[0];

    public void addWords(int key, Word[] newWord)
    {
        Word[] old = null;
        if (key == 0)
        {
            old = General;
        }
        else if (key == 1) {
            old = Verb;
        }
        else if (key == 2)
        {
            old = Noun;
        }
        else
        {
            old = Adjective;
        }

        List<Word> total = new List<Word>();
        foreach (Word w in old)
        {
            total.Add(w);
        }
        foreach (Word w in newWord)
        {
            total.Add(w);
        }

        if (key == 0)
        {
            General = total.ToArray();
        }
        else if (key == 1)
        {
            Verb = total.ToArray();
        }
        else if (key == 2)
        {
            Noun = total.ToArray();
        }
        else
        {
            Adjective = total.ToArray();
        }
    }
}
