using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvUi : MonoBehaviour
{

    public InventorySystem a;
    public GameObject slotPrefab;
    // Start is called before the first frame update
    void Start()
    {
       // a.onInventoryChangedEvent += OnUpdateInventory;   
    }

    public void OnUpdateInventory() {
        foreach(Transform t in transform) {
            Destroy(t.gameObject);
        }
    }

    public void DrawInventory() {
        foreach(Item item in a.inventory) {
            AddInventorySlot(item);
        }
    }

    private void AddInventorySlot(Item item) {
        GameObject obj = Instantiate(slotPrefab);
        obj.transform.SetParent(transform, false);

        ItemSlot slot = obj.GetComponent<ItemSlot>();
        slot.Set(item);
    }

}
