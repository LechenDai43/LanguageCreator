using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class SoundChange
{
    public SoundChange[] Branches;

    public Word change(Word input)
    {
        Word output = Word.deepCopy(input);

        //TODO ...
        //change the word according to some rules

        return output;
    }
}
