using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phoneme
{
    public ProtoPhone[] phones;
    public string letters;
    public double frequency;

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
}
