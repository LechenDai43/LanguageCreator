using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoneme
{
    public ProtoPhone[] phones;
    public string letters;
    public string preceding = null, successing = null;
    public double frequency;

    public Phoneme()
    {
        phones = new ProtoPhone[0];
        letters = "";
    }

    public void addPhone(ProtoPhone newPhone, string newLetter)
    {
        ProtoPhone[] newArr = new ProtoPhone[phones.Length + 1];
        int i = 0;
        for (i = 0; i < phones.Length; i++)
        {
            newArr[i] = phones[i];
        }
        newArr[i] = newPhone;
        phones = newArr;
        letters += newLetter;
    }

    public void addPhone(Phoneme newPhone)
    {
        if (newPhone == null || newPhone.phones == null)
        {
            return;
        }
        ProtoPhone[] newArr = new ProtoPhone[phones.Length + newPhone.phones.Length];
        int i = 0;
        for (i = 0; i < phones.Length; i++)
        {
            newArr[i] = phones[i];
        }
        for (int j = 0; j < newPhone.phones.Length; j++)
        {
            newArr[i + j] = newPhone.phones[j];
        }
        phones = newArr;
        letters += newPhone.letters;
    }

    public string getIPA()
    {
        string result = "";
        foreach (ProtoPhone pp in phones)
        {
            result += pp.IPA;
        }

        return result;
    }

    public void clear()
    {
        letters = "";
        phones = new ProtoPhone[0];
        preceding = null;
        successing = null;
        frequency = 0.0;
    }
}
