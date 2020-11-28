using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AffixMakerUpperLeftCornerScript : MonoBehaviour, IDropHandler
{
    public AffixMakerScript parentScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Phoneme draggedPhoneme = eventData.pointerDrag.GetComponent<UneditibleItemScript>().phoneme;
            parentScript.affixPhoneme.addPhone(draggedPhoneme);
            parentScript.thisIPA.text = parentScript.affixPhoneme.getIPA();
            parentScript.thisLetters.text = parentScript.affixPhoneme.letters;
        }
    }
}
