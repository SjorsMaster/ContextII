using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItem, Item> m_itemDictionary;
    public List<Item> inventory {get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        inventory = new List<Item>();
        m_itemDictionary = new Dictionary<InventoryItem, Item>();
    }

    // Update is called once per frame
    public void Add(InventoryItem referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out Item value)){
            value.AddToStack();
        }
        else{
            Item newItem = new Item(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
    }

    public void Remove(InventoryItem referenceData){
        if(m_itemDictionary.TryGetValue(referenceData, out Item value)){
            value.RemoveFromStack();

            if(value.stackSize == 0){
            inventory.Remove(value);
            m_itemDictionary.Remove(referenceData);
            }
        }
    }
}
