using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemWithStats : MonoBehaviour
{
    public InventoryItem self;

    [SerializeField]
    private Image icon;

    [SerializeField]
    TextMeshProUGUI label;

    public void SetStats(InventoryItem me) {
        self = me;
        icon.sprite = me.icon;
        label.text = me.displayName;
    }
}
