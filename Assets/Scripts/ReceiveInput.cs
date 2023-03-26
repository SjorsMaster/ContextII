using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReceiveInput : MonoBehaviour {
    public GameObject refference,display;
    public InventoryItem stats;
    public StatsTracker tracker;
    public Image preview;
    public TMP_Text title, desc;
    public InventorySaver inventorySaver;

    public void GetNewObject(GameObject reff, InventoryItem inputStats) {
        refference = reff;
        stats = inputStats; //for whatever reason I have to do it like this and it's driving me insane, I just wanna get component
        preview.sprite = stats.icon;
        title.text = stats.displayName;
        desc.text = stats.description;
        display.SetActive(true);
    }

    public void useItem() {
        PlayerPrefs.SetFloat("stat1", Mathf.Clamp(PlayerPrefs.GetFloat("stat1") + stats.stat1, 0, tracker.max));
        PlayerPrefs.SetFloat("stat2", Mathf.Clamp(PlayerPrefs.GetFloat("stat2") + stats.stat2, 0, tracker.max));
        PlayerPrefs.SetFloat("stat3", Mathf.Clamp(PlayerPrefs.GetFloat("stat3") + stats.stat3, 0, tracker.max));
        PlayerPrefs.SetFloat("stat4", Mathf.Clamp(PlayerPrefs.GetFloat("stat4") + stats.stat4, 0, tracker.max));
        PlayerPrefs.SetFloat("stat5", Mathf.Clamp(PlayerPrefs.GetFloat("stat5") + stats.stat5, 0, tracker.max));
        PlayerPrefs.SetFloat("stat6", Mathf.Clamp(PlayerPrefs.GetFloat("stat6") + stats.stat6, 0, tracker.max));
        PlayerPrefs.SetFloat("stat7", Mathf.Clamp(PlayerPrefs.GetFloat("stat7") + stats.stat7, 0, tracker.max));
        PlayerPrefs.SetFloat("stat8", Mathf.Clamp(PlayerPrefs.GetFloat("stat8") + stats.stat8, 0, tracker.max));
        PlayerPrefs.SetFloat("stat9", Mathf.Clamp(PlayerPrefs.GetFloat("stat9") + stats.stat9, 0, tracker.max));
        Destroy(refference);
        inventorySaver.SaveItems();
    }
}
