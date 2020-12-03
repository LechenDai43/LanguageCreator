using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class OverSizePageFourScript : MonoBehaviour, IDropHandler
{
    public bool addedToParent = false;
    public Phoneme phoneme;
    public Text IPAText, letterText, frequencyText;
    public GameObject anotherItem;
    public int parentIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleDeleteButtonPressed()
    {
        Destroy(this.transform.gameObject);
    }

    public void handleEnterLetters()
    {
        phoneme.letters = transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
    }

    public void handleEnterNumbers()
    {
        StartCoroutine(enterNumberHelper());
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && !this.transform.GetChild(0).GetComponent<Text>().text.Equals(""))
        {
            SemivowelItemScript draggedObject = eventData.pointerDrag.GetComponent<SemivowelItemScript>();
            GameObject overSizeObject = draggedObject.instanceOfObject;
            if (overSizeObject != null)
            {
                // Generate a combined item
                SemivowelItemScript semivowelItem = draggedObject;
                GameObject combinedItem = (GameObject)Instantiate(anotherItem, this.transform.parent);
                combinedItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
                OverSizePageFourScript combinedScript = combinedItem.GetComponent<OverSizePageFourScript>();
                Phoneme combinedPhoneme = new Phoneme();
                int newLength = phoneme.phones.Length + 1;
                combinedPhoneme.phones = new ProtoPhone[newLength];
                Debug.Log(semivowelItem);

                // Fill the combined item according to the panel and toggle
                if (parentIndex < 4)
                {
                    for (int i = 0; i < phoneme.phones.Length; i++)
                    {
                        combinedPhoneme.phones[i] = phoneme.phones[i];
                    }
                    combinedPhoneme.phones[newLength - 1] = semivowelItem.phone;

                    // Combine the semivowel as a part of consonant cluster
                    if (semivowelItem.LallVToggle.isOn)
                    {
                        combinedPhoneme.letters = phoneme.letters + semivowelItem.letter;
                    }
                    else
                    {
                        
                        combinedPhoneme.letters = phoneme.letters;
                        combinedPhoneme.successing = semivowelItem.letter;
                        if (phoneme.successing != null)
                        {
                            Destroy(combinedItem);
                        }
                    }
                }
                else
                {
                    combinedPhoneme.phones[0] = semivowelItem.phone;
                    for (int i = 0; i < phoneme.phones.Length; i++)
                    {
                        combinedPhoneme.phones[i + 1] = phoneme.phones[i];
                    }

                    if (semivowelItem.FallCToggle.isOn )
                    {
                        combinedPhoneme.letters = semivowelItem.letter + phoneme.letters;
                    }
                    else
                    {
                        combinedPhoneme.letters = phoneme.letters;
                        combinedPhoneme.preceding = semivowelItem.letter;
                        if (phoneme.preceding != null)
                        {
                            Destroy(combinedItem);
                        }
                    }
                }

                // Fille the text
                string conIPA = "";
                for (int i = 0; i < combinedPhoneme.phones.Length; i++)
                {
                    conIPA += combinedPhoneme.phones[i].IPA;
                }
                combinedScript.IPAText.text = conIPA;
                combinedScript.letterText.text = combinedPhoneme.letters;
                combinedScript.frequencyText.text = phoneme.frequency.ToString();
                combinedScript.phoneme = combinedPhoneme;
                Destroy(overSizeObject);
            }
        }
    }

    private IEnumerator enterNumberHelper()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);
        string inputNum = transform.GetChild(2).GetChild(2).GetComponent<Text>().text;
        Debug.Log(phoneme.frequency);
        if (!inputNum.Equals(""))
        {
            try
            {
                phoneme.frequency = Double.Parse(inputNum);
            }
            catch (Exception)
            {

            }
        }
        else
        {
            phoneme.frequency = 1;
        }
    }

    private IEnumerator enterLetterHelper()
    {
        yield return new WaitForSeconds(Time.deltaTime * 4);
        phoneme.letters = transform.GetChild(1).GetChild(2).GetComponent<Text>().text;
    }
}
