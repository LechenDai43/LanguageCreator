using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

public class PhoneManager
{
    // Four axis that determines where a phone locates
    static public List<StringIndexVector> consonantHorizontal = new List<StringIndexVector>();
    static public List<StringIndexVector> consonantVertical = new List<StringIndexVector>();
    static public List<StringIndexVector> vowelHorizontal = new List<StringIndexVector>();
    static public List<StringIndexVector> vowelVertical = new List<StringIndexVector>();

    // Phone pool
    public ConsonantPhone[][] consonantPool;
    public VowelPhone[][] vowelPool;
    public SemivowelPhone[] semivowelPool;
    public AccentPhone[] accentPool;
    public ConsonantPhone emptyConsonant;
    public VowelPhone emptyVowel;

    // Initialize the four axis
    public PhoneManager()
    {
        emptyConsonant = new ConsonantPhone();
        emptyConsonant.IPA = "'";
        emptyConsonant.POA = "null";
        emptyConsonant.MOA = "null";
        emptyConsonant.FCB = "back";
        emptyConsonant.Aspiration = "no";
        emptyConsonant.Voiceness = "no";

        emptyVowel = new VowelPhone();
        emptyVowel.IPA = "ɿ";
        emptyVowel.Openness = "close";
        emptyVowel.Roundness = "unrounded";
        emptyVowel.FCB = "front";

        consonantHorizontal.Clear();
        consonantVertical.Clear();
        vowelHorizontal.Clear();
        vowelVertical.Clear();

        consonantHorizontal.Add(new StringIndexVector("nasal", 0));
        consonantHorizontal.Add(new StringIndexVector("plosive", 2));
        consonantHorizontal.Add(new StringIndexVector("fricative", 6));
        consonantHorizontal.Add(new StringIndexVector("sibilant", 8));
        consonantHorizontal.Add(new StringIndexVector("flap", 10));
        consonantHorizontal.Add(new StringIndexVector("trill", 11));
        consonantHorizontal.Add(new StringIndexVector("lateral", 12));
        consonantHorizontal.Add(new StringIndexVector("ejective plosive", 13));
        consonantHorizontal.Add(new StringIndexVector("ejective fricative", 14));
        consonantHorizontal.Add(new StringIndexVector("ejective sibilant", 15));
        consonantHorizontal.Add(new StringIndexVector("click", 16));
        consonantHorizontal.Add(new StringIndexVector("implosive", 18));

        consonantVertical.Add(new StringIndexVector("bilabial", 0));
        consonantVertical.Add(new StringIndexVector("labiodental", 1));
        consonantVertical.Add(new StringIndexVector("linguolabial", 2));
        consonantVertical.Add(new StringIndexVector("dental", 3));
        consonantVertical.Add(new StringIndexVector("alveolar", 4));
        consonantVertical.Add(new StringIndexVector("postalveolar", 5));
        consonantVertical.Add(new StringIndexVector("retroflex", 6));
        consonantVertical.Add(new StringIndexVector("palatal", 7));
        consonantVertical.Add(new StringIndexVector("velar", 8));
        consonantVertical.Add(new StringIndexVector("uvular", 9));
        consonantVertical.Add(new StringIndexVector("glottal", 10));

        vowelHorizontal.Add(new StringIndexVector("open", 0));
        vowelHorizontal.Add(new StringIndexVector("near-open", 1));
        vowelHorizontal.Add(new StringIndexVector("half-open", 2));
        vowelHorizontal.Add(new StringIndexVector("middle", 3));
        vowelHorizontal.Add(new StringIndexVector("half-close", 4));
        vowelHorizontal.Add(new StringIndexVector("near-close", 5));
        vowelHorizontal.Add(new StringIndexVector("close", 6));

        vowelVertical.Add(new StringIndexVector("front", 0));
        vowelVertical.Add(new StringIndexVector("central", 2));
        vowelVertical.Add(new StringIndexVector("back", 4));
    }

    public void loadIn(bool advanced)
    {
        // Get the root path
        string path = Application.dataPath;

        // Get the stream reader ready
        StreamReader sr;

        // Get the file path
        if (advanced)
        {
            
        }
        else
        {
            // Read the consonant file
            string consonantPath = path + "/Files/Utility/Phone/SimpleConsonant.phonePool";
            sr = new StreamReader(consonantPath);
            string content = sr.ReadToEnd();
            ConsonantGroup consonantGroup = JsonUtility.FromJson<ConsonantGroup>(content);
            consonantPool = consonantGroup.getConsonantPool();
            sr.Close();

            // Read the vowel file
            string vowelPath = path + "/Files/Utility/Phone/SimpleVowel.phonePool";
            sr = new StreamReader(vowelPath);
            content = sr.ReadToEnd();
            VowelGroup vowelGroup = JsonUtility.FromJson<VowelGroup>(content);
            vowelPool = vowelGroup.getVowelPool();
            sr.Close();

            // Read the semivowel file
            string semivowelPath = path + "/Files/Utility/Phone/SimpleSemivowel.phonePool";
            sr = new StreamReader(semivowelPath);
            content = sr.ReadToEnd();
            SemivowelGroup semivowelGroup = JsonUtility.FromJson<SemivowelGroup>(content);
            semivowelPool = semivowelGroup.SimpleSemivowels;
            sr.Close();

            // Read the accent file
            string accentPath = path + "/Files/Utility/Phone/SimpleAccent.phonePool";
            sr = new StreamReader(accentPath);
            content = sr.ReadToEnd();
            AccentGroup accentGroup = JsonUtility.FromJson<AccentGroup>(content);
            accentPool = accentGroup.SimpleAccents;
            sr.Close();
        }
    }


    [Serializable]
    public class ConsonantGroup
    {
        public ConsonantPhone[] SimpleConsonants;

        public ConsonantPhone[][] getConsonantPool()
        {
            // Initialize the returning value
            ConsonantPhone[][] result = new ConsonantPhone[11][];
            for (int i = 0; i < 11; i++)
            {
                result[i] = new ConsonantPhone[20];
            }

            // Populate the returning value with the simple consonants
            foreach (ConsonantPhone cp in SimpleConsonants)
            {
                int h = 0, v = 0;

                // Get the horizontal index
                foreach (StringIndexVector siv in consonantHorizontal) {
                    if (cp.MOA.Equals(siv.stringKey))
                    {
                        h = siv.index;
                        break;
                    }
                }

                // Change the index for special case
                if (h == 0 || h == 6 || h == 8)
                {
                    if (cp.Voiceness.Equals("yes"))
                    {
                        h++;
                    }
                }
                if (h == 2)
                {
                    if (cp.Voiceness.Equals("yes"))
                    {
                        h += 2;
                    }
                    if (cp.Aspiration.Equals("yes"))
                    {
                        h++;
                    }
                }

                // Get the vertical index
                foreach (StringIndexVector siv in consonantVertical)
                {
                    if (cp.POA.Equals(siv.stringKey))
                    {
                        v = siv.index;
                        break;
                    }
                }

                // Put the phone to its place
                result[v][h] = cp;
            }

            return result;
        }
    }

    [Serializable]
    public class VowelGroup
    {
        public VowelPhone[] SimpleVowels;

        public VowelPhone[][] getVowelPool()
        {
            VowelPhone[][] result = new VowelPhone[6][];
            for (int i = 0; i < 6; i++)
            {
                result[i] = new VowelPhone[7];
            }

            // Populate the returning value with the simple vowel
            foreach (VowelPhone vp in SimpleVowels)
            {
                int h = 0, v = 0;

                // Get the horizontal index
                foreach (StringIndexVector siv in vowelHorizontal)
                {
                    if (vp.Openness.Equals(siv.stringKey))
                    {
                        h = siv.index;
                        break;
                    }
                }

                // Get the vertical index
                foreach (StringIndexVector siv in vowelVertical)
                {
                    if (vp.FCB.Equals(siv.stringKey))
                    {
                        v = siv.index;
                        break;
                    }
                }

                // Change vertical index for special case
                if (vp.Roundness.Equals("unrounded"))
                {
                    v++;
                }

                // Put the phone to its place
                result[v][h] = vp;
            }

            return result;
        }
    }

    [Serializable]
    public class SemivowelGroup
    {
        public SemivowelPhone[] SimpleSemivowels;
    }

    [Serializable]
    public class AccentGroup
    {
        public AccentPhone[] SimpleAccents;
    }

    public class StringIndexVector
    {
        public int index;
        public string stringKey;

        public StringIndexVector()
        {

        }

        public StringIndexVector(string a, int b)
        {
            index = b;
            stringKey = a;
        }
    }
}
