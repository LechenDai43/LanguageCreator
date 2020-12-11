using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class LanguageFamily
{
    public string Name;
    public SpeechSound[] WordOnsets, WordCodas, SyllableOnsets, SyllableCodas, StressedVowels, UnstressedVowels;
    public Phone[] Accents;
    public Morphome[] Generals, Verbs, Nouns, Adjectives;
    public SoundChange Root;
    public string Directory;
    
    public Word[] generateGenralWord(int type, int num)
    {
        if (type < 0 || type >= Generals.Length)
        {
            return new Word[0];
        }
        Morphome chosen = Generals[type];
        return generateVocabulary(chosen, num);
    }

    public Word[] generateVerbWord(int type, int num)
    {
        if (type < 0 || type >= Verbs.Length)
        {
            return new Word[0];
        }
        List<Word> result = new List<Word>();
        Morphome chosen = Verbs[type];
        return generateVocabulary(chosen, num);
    }

    public Word[] generateNounWord(int type, int num)
    {
        if (type < 0 || type >= Nouns.Length)
        {
            return new Word[0];
        }
        List<Word> result = new List<Word>();
        Morphome chosen = Nouns[type];
        return generateVocabulary(chosen, num);
    }

    public Word[] generateAdjectiveWord(int type, int num)
    {
        if (type < 0 || type >= Adjectives.Length)
        {
            return new Word[0];
        }
        List<Word> result = new List<Word>();
        Morphome chosen = Adjectives[type];
        return generateVocabulary(chosen, num);
    }

    public Word[] generateVocabulary(Morphome chosen, int num)
    {
        List<Word> result = new List<Word>();
        for (int i = 0; i < num; i++)
        {
            Word word = null;
            bool dup = true;
            while (dup)
            {
                word = generateWord(chosen);
                bool duplicated = false;
                foreach (Word old in result)
                {
                    if (word.ToString().Equals(old.ToString()))
                    {
                        duplicated = true;
                    }
                }
                dup = duplicated;
            }
            result.Add(word);
        }
        return result.ToArray();
    }

    private Word generateWord(Morphome rule)
    {
        Word result = new Word();
        System.Random rnd = new System.Random();
        List<SpeechSound> list = new List<SpeechSound>();
        int maxLoop = 50;
        
        // Pure word
        if (!rule.AfroAsian)
        {
            int specialSyllable = -1;
            if (rule.SpecialPhone)
            {
                specialSyllable = rnd.Next(0, rule.SyllableNumber);
                if (specialSyllable >= rule.SyllableNumber)
                {
                    specialSyllable = rule.SyllableNumber - 1;
                }
            }

            for (int i = 0; i < rule.SyllableNumber; i++)
            {
                // Check if this is accent syllable
                bool accented = false;
                Phone[] accents = null;
                if (rule.Tones == null || rule.Tones.Length < 1)
                {
                    accented = false;
                }
                else
                {
                    foreach(Morphome.ToneRequiement tr in rule.Tones)
                    {
                        if (i == tr.Position)
                        {
                            accented = true;
                            accents = tr.Accents;
                            break;
                        }
                    }
                }
                accented = accented && accents != null && accents.Length > 0;

                // If this is the first syllable
                SpeechSound[] onsets = null;
                if (i == specialSyllable && rule.SpecialConsonants != null && rule.SpecialConsonants.Length > 0)
                {
                    onsets = rule.SpecialConsonants;
                }
                else if (i == 0)
                {
                    onsets = WordOnsets;
                }
                else
                {
                    onsets = SyllableOnsets;
                }
                double maxOnsetFreq = 0;
                foreach (SpeechSound ss in onsets)
                {
                    maxOnsetFreq += ss.Frequency;
                }
                bool notOk = true;

                // If this is the last syllable
                SpeechSound[] codas = null;
                if (i == rule.SyllableNumber - 1)
                {
                    codas = WordCodas;
                }
                else
                {
                    codas = SyllableCodas;
                }
                double maxCodaFreq = 0;
                foreach (SpeechSound ss in codas)
                {
                    maxCodaFreq += ss.Frequency;
                }

                // Repeatively try to make the syllable until it saticify all the requirement
                int loopNum = 0;
                while (notOk && loopNum < maxLoop)
                {
                    loopNum++;
                    // Try to get the onset consonant
                    double checkOnsetFreq = rnd.NextDouble() * maxOnsetFreq;
                    double count = 0.0;
                    SpeechSound chosenOnset = null;
                    foreach (SpeechSound ss in onsets)
                    {
                        count += ss.Frequency;
                        if (count >= checkOnsetFreq)
                        {
                            chosenOnset = SpeechSound.deepCopy(ss);
                            break;
                        }
                    }
                    if (chosenOnset == null)
                    {
                        continue;
                    }

                    // Checkout the list of vowels
                    SpeechSound[] potential = null;
                    if (i == specialSyllable && rule.SpecialVowels != null && rule.SpecialVowels.Length > 0)
                    {
                        potential = rule.SpecialVowels;
                    }
                    if (accented)
                    {
                        potential = StressedVowels;
                    }
                    else
                    {
                        potential = UnstressedVowels;
                    }

                    // Check if the onset require semivowel
                    if (chosenOnset.Successed)
                    {
                        List<SpeechSound> alter = new List<SpeechSound>();
                        foreach (SpeechSound toCheck in potential)
                        {
                            if (toCheck.Preceded)
                            {
                                if (chosenOnset.Glide.Equals(toCheck.Glide))
                                {
                                    alter.Add(toCheck);
                                }
                            }
                        }
                        if (alter.Count < 1)
                        {
                            continue;
                        }
                        potential = alter.ToArray();
                    }

                    // Get the vowel
                    SpeechSound vowel = null;
                    double maxVowelFreq = 0;
                    foreach (SpeechSound ss in potential)
                    {
                        maxVowelFreq += ss.Frequency;
                    }
                    double checkVowelFreq = rnd.NextDouble() * maxVowelFreq;
                    count = 0.0;
                    foreach (SpeechSound ss in potential)
                    {
                        count += ss.Frequency;
                        if (count >= checkVowelFreq)
                        {
                            vowel = SpeechSound.deepCopy(ss);
                            break;
                        }
                    }
                    if (vowel == null)
                    {
                        continue;
                    }

                    // Add accent if needed
                    if (accented)
                    {
                        string accentTrans = accents[rnd.Next(0, accents.Length)].IPA;
                        vowel.Transliteration += accentTrans;
                    }


                    list.Add(chosenOnset);
                    list.Add(vowel);

                    // Get the coda consonant
                    if (codas != null && codas.Length > 0)
                    {
                        double checkCodaFreq = rnd.NextDouble() * maxCodaFreq;
                        count = 0.0;
                        SpeechSound chosenCoda = null;
                        foreach (SpeechSound ss in codas)
                        {
                            count += ss.Frequency;
                            if (count >= checkCodaFreq)
                            {
                                chosenCoda = SpeechSound.deepCopy(ss);
                                break;
                            }
                        }
                        if (chosenCoda == null)
                        {
                            continue;
                        }
                        list.Add(chosenCoda);
                    }
                    notOk = false;
                }
                
                // If the loop number excede the max loop number
                if (loopNum >= maxLoop)
                {
                    list.Add(SpeechSound.deepCopy(SyllableOnsets[rnd.Next(0, SyllableOnsets.Length - 1)]));
                    list.Add(SpeechSound.deepCopy(UnstressedVowels[rnd.Next(0, UnstressedVowels.Length - 1)]));
                    if (SyllableCodas != null && SyllableCodas.Length > 0)
                    {
                        list.Add(SpeechSound.deepCopy(SyllableCodas[rnd.Next(0, SyllableCodas.Length - 1)]));
                    }
                }
            }
            
            if (rule.Affixed)
            {
                if (rule.Prefix != null && rule.Prefix.Phonemes.Length > 0)
                {
                    result.Prefixed = true;
                    result.Prefix = SpeechSound.deepCopy(rule.Prefix);
                }
                if (rule.Suffix != null && rule.Suffix.Phonemes.Length > 0)
                {
                    result.Suffixed = true;
                    result.Suffix = SpeechSound.deepCopy(rule.Suffix);
                }
            }
        }
        else
        {
            // Get all the consonants ready
            List<SpeechSound> potentialConsonant = new List<SpeechSound>();
            SpeechSound[][] allsounds = new SpeechSound[4][];
            allsounds[0] = WordCodas;
            allsounds[1] = WordOnsets;
            allsounds[2] = SyllableCodas;
            allsounds[3] = SyllableOnsets;

            // Pick valid consonants
            foreach (SpeechSound[] row in allsounds)
            {
                foreach (SpeechSound ss in row)
                {
                    if (ss.Phonemes.Length == 1)
                    {
                        potentialConsonant.Add(SpeechSound.deepCopy(ss));
                    }
                    else if (ss.Phonemes.Length == 2)
                    {
                        if (ss.Phonemes[ss.Phonemes.Length - 1].Sornority != null && !ss.Phonemes[ss.Phonemes.Length - 1].Sornority.Equals(""))
                        {
                            if (rule.SemivoweledConsonant)
                            {
                                potentialConsonant.Add(SpeechSound.deepCopy(ss));
                            }
                        }
                        else if (rule.ClusteredConsonant)
                        {
                            potentialConsonant.Add(SpeechSound.deepCopy(ss));
                        }
                    }
                    else if (ss.Phonemes.Length > 2)
                    {
                        if (ss.Phonemes[ss.Phonemes.Length - 1].Sornority != null && !ss.Phonemes[ss.Phonemes.Length - 1].Sornority.Equals(""))
                        {
                            if (rule.SemivoweledConsonant && rule.ClusteredConsonant)
                            {
                                potentialConsonant.Add(SpeechSound.deepCopy(ss));
                            }
                        }
                        else if (rule.ClusteredConsonant)
                        {
                            potentialConsonant.Add(SpeechSound.deepCopy(ss));
                        }
                    }
                }
            }

            // If there is no valid consonants
            foreach (SpeechSound[] row in allsounds)
            {
                foreach (SpeechSound ss in row)
                {
                    potentialConsonant.Add(SpeechSound.deepCopy(ss));
                }
            }

            // Make the word
            double maxFreq = 0;
            foreach (SpeechSound ss in potentialConsonant)
            {
                maxFreq += ss.Frequency;
            }
            for (int i = 0; i < rule.HolderVowels.Length; i++)
            {
                double checkFreq = rnd.NextDouble() * maxFreq;
                double count = 0.0;
                SpeechSound chosen = null;
                foreach (SpeechSound ss in potentialConsonant)
                {
                    count += ss.Frequency;
                    if (count >= checkFreq)
                    {
                        chosen = SpeechSound.deepCopy(ss);
                        break;
                    }
                }
                if (chosen == null)
                {
                    chosen = SpeechSound.deepCopy(potentialConsonant.ToArray()[0]);
                }

                list.Add(chosen);
                list.Add(SpeechSound.deepCopy(rule.HolderVowels[i]));
            }
        }

        SpeechSound previous = list[0];
        for (int i = 1; i < list.Count; i++)
        {
            SpeechSound current = list[i];
            if (current.Phonemes.Length == 1 && current.Phonemes[0].IPA != null && current.Phonemes[0].IPA.Equals("'"))
            {
                if (previous.Phonemes.Length > 0 && (previous.Phonemes[previous.Phonemes.Length - 1].Openness == null || previous.Phonemes[previous.Phonemes.Length - 1].Openness.Equals("")))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }

        result.Phonemes = list.ToArray();
        return result;
    }

    public void saveSelf() 
    {
        string langaugeData = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Application.dataPath + "/Files/Customization/Languages/" + Name + ".languageFamily", langaugeData);
    }
}
