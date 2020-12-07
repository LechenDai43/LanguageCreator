using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class Phone
{
    public string IPA;
    public string FCB;
    public string Openness;
    public string Roundness;
    public string Sornority;
    public string POA;
    public string MOA;
    public string Aspiration;
    public string Voiceness;
    public string Contour, Level;

    public void converProtoPhone (ProtoPhone pp)
    {
        this.IPA = pp.IPA;
        this.FCB = pp.FCB;
        this.Openness = pp.Openness;
        this.Roundness = pp.Roundness;
        this.Sornority = pp.Sornority;
        this.POA = pp.POA;
        this.MOA = pp.MOA;
        this.Aspiration = pp.Aspiration;
        this.Voiceness = pp.Voiceness;
        this.Contour = pp.Contour;
        this.Level = pp.Level;
    }

    public static Phone deepCopy (Phone other)
    {
        Phone result = new Phone();
        if (other != null)
        {
            result.IPA = other.IPA;
            result.FCB = other.FCB;
            result.Openness = other.Openness;
            result.Roundness = other.Roundness;
            result.Sornority = other.Sornority;
            result.POA = other.POA;
            result.MOA = other.MOA;
            result.Aspiration = other.Aspiration;
            result.Voiceness = other.Voiceness;
            result.Contour = other.Contour;
            result.Level = other.Level;
        }
        return result;
    }

    public bool Equals(Phone other)
    {
        return IPA.Equals(other.IPA) && Level.Equals(other.Level) && 
            Openness.Equals(other.Openness) && Roundness.Equals(other.Roundness) &&
            Sornority.Equals(other.Sornority) && POA.Equals(other.POA) &&
            MOA.Equals(other.MOA) && Aspiration.Equals(other.Aspiration) &&
            Voiceness.Equals(other.Voiceness) && Contour.Equals(other.Contour);
    }
}
