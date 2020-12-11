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
    public bool IncludeAffix;

    public Word changeWord(Word input)
    {
        if (input == null)
        {
            return null;
        }
        List<Phone> list = new List<Phone>();
        if (IncludeAffix && input.Prefixed && input.Prefix != null)
        {
            if (input.Prefix.Phonemes != null)
            {
                list.AddRange(input.Prefix.Phonemes);
            }
        }
        foreach(SpeechSound ss in input.Phonemes)
        {
            if (ss.Preceded)
            {
                list.Add(ss.Glide);
            }
            foreach(Phone p in ss.Phonemes)
            {
                list.Add(p);
            }
            if (ss.Successed)
            {
                list.Add(ss.Glide);
            }
        }
        if (IncludeAffix && input.Suffixed && input.Suffix != null)
        {
            if (input.Suffix.Phonemes != null)
            {
                list.AddRange(input.Suffix.Phonemes);
            }
        }

        // Debug.Log(list.Count);
        // TODO...
        // find the target pattern
        // change the target pattern to the desired pattern
        Manager manager = UnityEngine.Object.FindObjectOfType<Manager>();
        PhoneManager phoneManager = manager.phoneManager;
        int i = 0;
        while (i < list.Count - Target.Length + 1)
        {

            if (list[i].Level != null && !list[i].Level.Equals(""))
            {
                i++;
                continue;
            }

            bool fullMatch = true;
            int length = 0;

            // Find the target parttern
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
                    if (i + length >= list.Count)
                    {
                        fullMatch = false;
                        break;
                    }
                    if (list[i + length].Openness != null && !list[i + length].Openness.Equals(""))
                    {
                        tem.Add(list[i + length]);
                        length++;

                        if (i + length < list.Count && list[i + length].Level != null && !list[i + length].Level.Equals(""))
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
                else if (Target[j].SingleHolder && Target[j].Type.Contains("n"))
                {
                    List<Phone> tem = new List<Phone>();

                    if (i + length >= list.Count)
                    {
                        fullMatch = false;
                        break;
                    }
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

                    if (i + length >= list.Count)
                    {
                        fullMatch = false;
                        break;
                    }
                    while (i + length < list.Count && list[i + length].Openness != null && !list[i + length].Openness.Equals(""))
                    {
                        count++;

                        tem.Add(list[i + length]);
                        length++;
                        if (i + length < list.Count && list[i + length].Level != null && !list[i + length].Level.Equals(""))
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
                else if (Target[j].MultipleHolder && Target[j].Type.Contains("n"))
                {
                    List<Phone> tem = new List<Phone>();
                    int count = 0;

                    if (i + length >= list.Count)
                    {
                        fullMatch = false;
                        break;
                    }
                    while (i + length < list.Count && list[i + length].POA != null && !list[i + length].POA.Equals(""))
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
                // If this is a specified vowel
                else if (Target[j].Type.Contains("w"))
                {
                    List<Phone> tem = new List<Phone>();


                    if (i + length >= list.Count || list[i + length].Openness == null || list[i + length].Openness.Equals(""))
                    {
                        fullMatch = false;
                        break;
                    }
                    int row = Target[j].Roundness;
                    int column = Target[j].Openness;

                    bool isOpen = false, isRound = false;

                    if (row >= phoneManager.vowelPool.Length)
                    {
                        isRound = true;
                    }
                    else if (row >= 0)
                    {
                        int poolIndex = 0;
                        while (poolIndex < phoneManager.vowelPool[row].Length)
                        {
                            if (phoneManager.vowelPool[row][poolIndex] != null)
                            {
                                if (list[i + length].Roundness.Equals(phoneManager.vowelPool[row][poolIndex].Roundness))
                                {
                                    if (list[i + length].FCB.Equals(phoneManager.vowelPool[row][poolIndex].FCB))
                                    {
                                        isRound = true;
                                    }
                                }
                                break;
                            }
                            poolIndex++;
                        }
                    }

                    if (column >= phoneManager.vowelPool[0].Length)
                    {
                        isOpen = true;
                    }
                    else if (column >= 0)
                    {
                        int poolIndex = 0;
                        while (poolIndex < phoneManager.vowelPool.Length)
                        {
                            if (phoneManager.vowelPool[poolIndex][column] != null)
                            {
                                if (list[i + length].Openness.Equals(phoneManager.vowelPool[poolIndex][column].Openness))
                                {
                                    isOpen = true;                                    
                                }
                                break;
                            }
                            poolIndex++;
                        }
                    }

                    if (isRound && isOpen)
                    {
                        tem.Add(list[i + length]);
                        length++;
                        if (i + length < list.Count && list[i + length].Level != null && !list[i + length].Level.Equals(""))
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
                // If this is a pecified consonant
                else if (Target[j].Type.Contains("n"))
                {
                    List<Phone> tem = new List<Phone>();


                    if (i + length >= list.Count || list[i + length].POA == null || list[i + length].POA.Equals(""))
                    {
                        fullMatch = false;
                        break;
                    }
                    int row = Target[j].POA;
                    int column = Target[j].MOA;

                    bool isPOA = false, isMOA = false;

                    if (row >= phoneManager.consonantPool.Length)
                    {
                        isPOA = true;
                    }
                    else if (row >= 0)
                    {
                        int poolIndex = 0;
                        while (poolIndex < phoneManager.consonantPool[row].Length)
                        {
                            if (phoneManager.consonantPool[row][poolIndex] != null)
                            {
                                if (list[i + length].POA.Equals(phoneManager.consonantPool[row][poolIndex].POA))
                                {
                                    isPOA = true;
                                }
                                break;
                            }
                            poolIndex++;
                        }
                    }

                    if (column >= phoneManager.consonantPool[0].Length)
                    {
                        isMOA = true;
                    }
                    else if (column >= 0)
                    {
                        int poolIndex = 0;
                        while (poolIndex < phoneManager.consonantPool.Length)
                        {
                            if (phoneManager.consonantPool[poolIndex][column] != null)
                            {
                                if (list[i + length].MOA.Equals(phoneManager.consonantPool[poolIndex][column].MOA))
                                {
                                    if (list[i + length].Aspiration.Equals(phoneManager.consonantPool[poolIndex][column].Aspiration))
                                    {
                                        if (list[i + length].Voiceness.Equals(phoneManager.consonantPool[poolIndex][column].Voiceness))
                                        {
                                            isMOA = true;
                                        }
                                    }
                                }
                                break;
                            }
                            poolIndex++;
                        }
                    }

                    if (isPOA && isMOA)
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
                // Back up case, should not be used
                else
                {
                    fullMatch = false;
                    break;
                }
            }


            // If the pattern is found, then process it
            if (fullMatch)
            {
                Debug.Log("matched");
                // First, parse the original list to have the first part and the last part
                List<Phone> head = new List<Phone>(), tail = new List<Phone>();
                for (int j = 0; j < i; j++)
                {
                    head.Add(list[j]);
                }
                for (int j = i + length; j < list.Count; j++)
                {
                    tail.Add(list[j]);
                }

                // Then build up the middle part
                List<List<Phone>> middle = new List<List<Phone>>();
                for (int j = 0; j < Result.Length; j++)
                {
                    MetaBlock block = Result[j];

                    // if the meta block is empty, then make the corresponding part empty
                    if (block.Descriptions.Length == 0)
                    {
                        middle.Add(new List<Phone>());
                    }
                    // if the meta block is not empty, then build up the list
                    else
                    {
                        List<Phone> tem = new List<Phone>();
                        bool getAccent = false;

                        foreach (Description description in block.Descriptions)
                        {
                            // if the description is unchanged
                            if (description.Unchanged)
                            {
                                tem.AddRange(founded[j]);
                            }
                            // if the description is a specified vowel
                            else if (description.Type.Contains("w"))
                            {
                                int row = -1, column = -1;

                                // check out the row/roundedness of the vowel
                                // if the vowel is copying old roundness
                                if (description.DimensionTwo)
                                {
                                    string roundedness = founded[description.Roundness][0].Roundness;
                                    Debug.Log(founded[description.Roundness][0]);
                                    for (int k = 0; k < phoneManager.vowelPool.Length; k++)
                                    {
                                        if (roundedness.Equals(phoneManager.vowelPool[k][0].Roundness))
                                        {
                                            row = k;
                                            break;
                                        }
                                    }
                                }
                                // if the vowel specifying new roundness
                                else
                                {
                                    row = description.Roundness;
                                }

                                // check out the column/openness of the vowel
                                if (description.DimensionOne)
                                {
                                    string openness = founded[description.Openness][0].Openness;
                                    for (int k = 0; k < phoneManager.vowelPool[0].Length; k++)
                                    {
                                        if (openness.Equals(phoneManager.vowelPool[0][k].Openness))
                                        {
                                            if (founded[description.Openness][0].FCB.Equals(phoneManager.vowelPool[0][k].FCB))
                                            {
                                                column = k;
                                                break;
                                            }
                                        }
                                    }
                                }
                                // if the vowel specifying new roundness
                                else
                                {
                                    column = description.Openness;
                                }

                                // check out the phone from phone pool   
                                if (description.DimensionTwo && description.DimensionOne && description.Roundness == description.Openness) 
                                {
                                    tem.Add(founded[description.Roundness][0]);
                                }
                                else if (!(row < 0 || column < 0 || row >= phoneManager.vowelPool.Length || column >= phoneManager.vowelPool[0].Length))
                                {
                                    Debug.Log(phoneManager.vowelPool[row][column]);
                                    if (phoneManager.vowelPool[row][column] != null)
                                    {
                                        Phone newPhone = new Phone();
                                        newPhone.converProtoPhone(phoneManager.vowelPool[row][column]);
                                        tem.Add(newPhone);
                                    }
                                }
                                Debug.Log(tem.Count);
                            }
                            // if the description is a specified consonant
                            else if (description.Type.Contains("n"))
                            {
                                int row = -1, column = -1;

                                // check out the row/roundedness of the vowel
                                // if the vowel is copying old roundness
                                if (description.DimensionTwo)
                                {
                                    string poa = founded[description.POA][0].POA;
                                    for (int k = 0; k < phoneManager.consonantPool.Length; k++)
                                    {
                                        if (poa.Equals(phoneManager.consonantPool[k][0].POA))
                                        {
                                            row = k;
                                            break;
                                        }
                                    }
                                }
                                // if the vowel specifying new roundness
                                else
                                {
                                    row = description.POA;
                                }

                                // check out the column/openness of the vowel
                                if (description.DimensionTwo && description.DimensionOne && description.MOA == description.POA) 
                                {
                                    tem.Add(founded[description.MOA][0]);
                                }
                                else if (description.DimensionOne)
                                {
                                    string moa = founded[description.MOA][0].MOA;
                                    for (int k = 0; k < phoneManager.consonantPool[0].Length; k++)
                                    {
                                        if (moa.Equals(phoneManager.consonantPool[0][k].MOA))
                                        {
                                            if (founded[description.MOA][0].Aspiration.Equals(phoneManager.consonantPool[0][k].Aspiration))
                                            {
                                                if (founded[description.MOA][0].Voiceness.Equals(phoneManager.consonantPool[0][k].Voiceness))
                                                {
                                                    column = k;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                // if the vowel specifying new roundness
                                else
                                {
                                    column = description.MOA;
                                }

                                // check out the phone from phone pool
                                if (!(row < 0 || column < 0 || row >= phoneManager.consonantPool.Length || column >= phoneManager.consonantPool[0].Length))
                                {
                                    if (phoneManager.consonantPool[row][column] != null)
                                    {
                                        Phone newPhone = new Phone();
                                        newPhone.converProtoPhone(phoneManager.consonantPool[row][column]);
                                        tem.Add(newPhone);
                                    }
                                }
                                Debug.Log(tem.Count);

                            }
                            // back up case, this should not be used
                            else
                            {
                                tem.Clear();
                                tem.AddRange(founded[j]);
                                break;
                            }
                            
                            foreach(Phone p in tem)
                            {
                                if (p.Level != null && !p.Level.Equals(""))
                                {
                                    getAccent = true;
                                }
                            }

                            bool originalHasAccent = false;
                            Phone accent = null;
                            foreach (Phone p in founded[j])
                            {
                                if (p.Level != null && !p.Level.Equals(""))
                                {
                                    originalHasAccent = true;
                                    accent = p;
                                }
                            }

                            if (!getAccent && originalHasAccent)
                            {
                                for (int k = 0; k < tem.Count; k++)
                                {
                                    if (tem[k].Openness != null && !tem[k].Openness.Equals(""))
                                    {
                                        tem.Insert(k + 1, accent);
                                        break;
                                    }
                                }
                            }
                        }

                        middle.Add(tem);
                    }
                }


                // Now, all three parts has been built, it is time to create a new list
                List<Phone> newList = new List<Phone>();
                newList.AddRange(head);
                for (int j = 0; j < middle.Count; j++)
                {
                    newList.AddRange(middle[j]);
                }
                int newIndex = newList.Count;
                i = newIndex - 1 > i + 1 ? newIndex - 1 : i + 1;
                newList.AddRange(tail);
                list = newList;
            }
            i++;
        }

        Word result = new Word();
        if (IncludeAffix)
        {
            result.Suffix = null;
            result.Prefix = null;
            result.Suffixed = false;
            result.Prefixed = false;
        }
        else
        {
            result.Suffix = input.Suffix;
            result.Prefix = input.Prefix;
            result.Suffixed = input.Suffixed;
            result.Prefixed = input.Prefixed;
        }

        result.Phonemes = new SpeechSound[list.Count];

        for (i = 0; i < list.Count; i++)
        {
            SpeechSound ss = new SpeechSound();
            ss.Phonemes = new Phone[1];
            ss.Phonemes[0] = list[i];

            ss.Glide = null;
            ss.Preceded = false;
            ss.Successed = false;
            ss.Transliteration = list[i].IPA;
            ss.Frequency = 0;

            result.Phonemes[i] = ss;
        }

        return result;
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
