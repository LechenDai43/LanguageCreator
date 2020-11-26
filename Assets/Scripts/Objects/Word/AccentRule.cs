using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccentRule
{
    public AccentPhone accent;
    public AccentPair[] pairs;
    

    public class AccentPair
    {
        public bool reciprocal;
        public int syllable;
        public double frequency;
        public int type;
    }
}
