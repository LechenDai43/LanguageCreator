using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class SoundChange
{
    public string Name;
    public string[] Branches;
    public Sandhi[] Rules;
    public string Directory;
    public int Count = 0;

    public Word change(Word input)
    {
        Word output = Word.deepCopy(input);
        foreach (Sandhi rule in Rules)
        {
            output = rule.changeWord(output);
        }

        return output;
    }
}
