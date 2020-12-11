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

        int i = 0;
        while (i < list.Count - Target.Length)
        {
            if (list[i].Level == null || list[i].Level.Equals(""))
            {
                i++;
                continue;
            }

            bool fullMatch = true;
            int length = 0;

            List<List<Phone>> founded = new List<List<Phone>>();
            for (int j = 0; j < Target.Length; j++)
            {
                // If this require an initial position
                if (Target[j].Initial)
                {
                    if (i + length == 0)
                    {
                        founded.Add(new List<Phone>());
                    }
                    else
                    {
                        fullMatch = false;
                        break;
                    }
                }
                // If this requires a terminal position
                else if (Target[j].Terminal)
                {
                    if (i + length == list.Count - 1)
                    {
                        founded.Add(new List<Phone>());
                    }
                    else
                    {
                        fullMatch = false;
                        break;
                    }
                }
                // If this is a single vowel
                else if (Target[j].SingleHolder && Target[j].Type.Contains("w"))
                {
                    List<Phone> tem = new List<Phone>();
                    if (list[i + length].Openness != null && !list[i + length].Openness.Equals(""))
                    {
                        tem.Add(list[i + length]);
                        length++;
                        if (list[i + length].Level != null && !list[i + length].Level.Equals(""))
                        {
                            tem.Add(list[i + length]);
                            length++;
                        }
                        founded.Add(tem);
                    }
                    else
                    {
                        fullMatch = false;
                        break;
                    }
                }
                // If this is a single consonant
                else if (Target[j].SingleHolder && Target[j].Type.Contains("c"))
                {
                    List<Phone> tem = new List<Phone>();
                    if (list[i + length].POA != null && !list[i + length].POA.Equals(""))
                    {
                        tem.Add(list[i + length]);
                        length++;
                        founded.Add(tem);
                    }
                    else
                    {
                        fullMatch = false;
                        break;
                    }
                }
                // If this is a vowel cluster
                else if (Target[j].MultipleHolder && Target[j].Type.Contains("w"))
                {
                    List<Phone> tem = new List<Phone>();
                    int count = 0;
                    while (list[i + length].Openness != null && !list[i + length].Openness.Equals(""))
                    {
                        count++;
                        tem.Add(list[i + length]);
                        length++;
                        if (list[i + length].Level != null && !list[i + length].Level.Equals(""))
                        {
                            tem.Add(list[i + length]);
                            length++;
                        }
                    }
                    if (count != 0)
                    {
                        founded.Add(tem);
                    }
                    else
                    {
                        fullMatch = false;
                        break;
                    }
                    
                }
                // If this is a consonant cluster
                else if (Target[j].MultipleHolder && Target[j].Type.Contains("w"))
                {
                    List<Phone> tem = new List<Phone>();
                    int count = 0;
                    while (list[i + length].POA != null && !list[i + length].POA.Equals(""))
                    {
                        count++;
                        tem.Add(list[i + length]);
                        length++;
                    }
                    if (count != 0)
                    {
                        founded.Add(tem);
                    }
                    else
                    {
                        fullMatch = false;
                        break;
                    }

                }

            }
        }

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
