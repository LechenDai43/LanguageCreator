using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PhonemePickerDropPanel : MonoBehaviour, IDropHandler
{
    public GameObject prefabedPickedItem;
    public GameObject panel;

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
            GameObject instancePickedPhone = (GameObject)Instantiate(prefabedPickedItem, panel.transform);
            instancePickedPhone.GetComponent<PickedPhoneItem>().phoneme = new Phoneme();
            instancePickedPhone.GetComponent<PickedPhoneItem>().phoneme.addPhone(draggedPhoneme);
            instancePickedPhone.GetComponent<PickedPhoneItem>().IPA.text = draggedPhoneme.getIPA();
            instancePickedPhone.GetComponent<PickedPhoneItem>().letters.text = draggedPhoneme.letters;
            // parentScript.affixPhoneme.addPhone(draggedPhoneme);
            // Debug.Log(parentScript.affixPhoneme.phones.Length);
            // parentScript.thisIPA.text = parentScript.affixPhoneme.getIPA();
            // parentScript.thisLetters.text = parentScript.affixPhoneme.letters;
        }
    }
}
