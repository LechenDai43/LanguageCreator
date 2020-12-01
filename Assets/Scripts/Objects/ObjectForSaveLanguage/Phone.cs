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
}
