using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class SpeechSound : MonoBehaviour
{
    public Phone[] Phonemes;
    public Phone Glide;
    public bool Preceded, Successed;
    public string Transliteration;
    public double Frequency;
}
