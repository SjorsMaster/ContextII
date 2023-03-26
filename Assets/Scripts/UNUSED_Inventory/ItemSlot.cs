using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour {
    [SerializeField]
    private Image icon;

    [SerializeField]
    TextMeshProUGUI label;

    [SerializeField]
    GameObject stackObj;

    [SerializeField]
    TextMeshProUGUI stackNo;

    public void Set(Item item) {
        icon.sprite = item.data.icon;
        label.text = item.data.name;
        if(item.stackSize <= 1) {
            stackObj.SetActive(false);
            return;
        }

        stackNo.text = item.stackSize.ToString();

    
    }
}