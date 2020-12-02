using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class SpeechSound
{
    public Phone[] Phonemes;
    public Phone Glide;
    public bool Preceded, Successed;
    public string Transliteration;
    public double Frequency;

    public static SpeechSound deepCopy (SpeechSound other)
    {
        SpeechSound result = new SpeechSound();
        result.Phonemes = other.Phonemes;
        result.Glide = other.Glide;
        result.Preceded = other.Preceded;
        result.Successed = other.Successed;
        result.Transliteration = other.Transliteration;
        result.Frequency = other.Frequency;
        return result;
    }
}
