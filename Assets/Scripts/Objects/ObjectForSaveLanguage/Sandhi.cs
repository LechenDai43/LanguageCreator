using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class Sandhi
{
    public Description[] Target;
    public MetaBlock[] Result;

    public Word changeWord(Word input)
    {
        if (input == null)
        {
            return null;
        }
        List<Phone> list = new List<Phone>();
        foreach(SpeechSound ss in input.Phonemes)
        {
            if (ss.Preceded)
            {
                list.Add(ss.Glide);
            }
            foreach(Phone p in ss.Phonemes)
            {
                if (p.Contour != null && !p.Contour.Equals(""))
                {
                    list.Add(p);
                }
            }
            if (ss.Successed)
            {
                list.Add(ss.Glide);
            }
        }

        // TODO...
        // find the target pattern
        // change the target pattern to the desired pattern

        return null;
    }

    [Serializable]
    public class MetaBlock
    {
        public Description[] Descriptions;
    }

    [Serializable]
    public class Description
    {
        public int Openness = -1;
        public int Roundness = -1;
        public int POA = -1;
        public int MOA = -1;
        public string Type = "";
        public bool Initial = false, Terminal = false;
        public bool SingleHolder = false, MultipleHolder = false;
        public bool DimensionOne = false, DimensionTwo = false;
        public bool Unchanged = false;
    }
}
