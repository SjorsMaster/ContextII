using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class InventorySaver : MonoBehaviour {
    public List<InventoryItem> inventoryItems;
    public string json; 
    public StoreItems SerializedinventoryItems;
    public StoreItems DeSerializedinventoryItems;
    public GameObject theInventoryContent;
    public QRCodeScanner qRCodeScanner;

    private void Awake() {
        LoadItems();
    }

    public void SaveItems() {
        print("Saving all items..");
        
        inventoryItems = new List<InventoryItem>();
        inventoryItems.Clear();

        foreach (Transform child in theInventoryContent.transform) {
            if (child != null) {
                inventoryItems.Add(child.gameObject.GetComponent<ItemWithStats>().GiveSelf());
            }
        }
        SerializedinventoryItems.itemList = inventoryItems;
        json = JsonUtility.ToJson(SerializedinventoryItems);
        PlayerPrefs.SetString("Inventory", json);
        PlayerPrefs.Save();
        print("Saved");
    }

    public void LoadItems() {
        print("Loading old items..");

        DeSerializedinventoryItems = JsonUtility.FromJson<StoreItems>(PlayerPrefs.GetString("Inventory", json));
        try {
            foreach (InventoryItem item in DeSerializedinventoryItems.itemList) {
                GameObject tmp = Instantiate(qRCodeScanner.itemObject, qRCodeScanner.inventorySpot.transform);
                tmp.GetComponent<ItemWithStats>().SetStats(item);
            }
            print("Loaded");
        }
        catch { print("No items found.."); }
    }

}
[System.Serializable]
public class StoreItems {

    public List<InventoryItem> itemList;
}
