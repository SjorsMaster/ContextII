using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class StatsTracker : MonoBehaviour {
    public float requiredPass = .1f;
    public int multiplier = 1;
    // Start is called before the first frame update
    public TMP_Text stats;
    public TMP_Text TradePots;
    public float removal = 0.001f;
    public float defaultStat = 10;
    public float max = 15;
    public float canTradePotions = 15;
    public Button potionButt;
    double hours;
    public InventorySaver inventorySaver;
    public float interval = 15;

    public GameObject plant1;
    public GameObject plant2;
    public GameObject plant3;
    public GameObject plant4;
    public float offset = 10;
    void Start() {
        //GetStat();
        plantOvertime();
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop() {
        while (true) {
            UpdateStat();
            print("done!");
            yield return new WaitForSeconds(interval);
            print("saving...");
        }
    }

    void GetStat() {
        stats.text = $"These are your stats:\n" +
    $"Stat 1: {PlayerPrefs.GetFloat("stat1", 15).ToString("0.00")}\n" +
    $"Stat 2: {PlayerPrefs.GetFloat("stat2", 15).ToString("0.00")}\n" +
    $"Stat 3: {PlayerPrefs.GetFloat("stat3", 15).ToString("0.00")}\n" +
    $"Stat 4: {PlayerPrefs.GetFloat("stat4", 15).ToString("0.00")}\n" +
    $"Stat 5: {PlayerPrefs.GetFloat("stat5", 15).ToString("0.00")}\n" +
    $"Stat 6: {PlayerPrefs.GetFloat("stat6", 15).ToString("0.00")}\n" +
    $"Stat 7: {PlayerPrefs.GetFloat("stat7", 15).ToString("0.00")}\n" +
    $"Stat 8: {PlayerPrefs.GetFloat("stat8", 15).ToString("0.00")}";
        canTradePotions = PlayerPrefs.GetFloat("pots", 15);

        plant1.transform.localScale = Vector3.one * ((PlayerPrefs.GetFloat("stat1") + PlayerPrefs.GetFloat("stat2")) / 2 / offset);
        plant2.transform.localScale = Vector3.one * ((PlayerPrefs.GetFloat("stat3") + PlayerPrefs.GetFloat("stat4")) / 2 / offset);
        plant3.transform.localScale = Vector3.one * ((PlayerPrefs.GetFloat("stat5") + PlayerPrefs.GetFloat("stat6")) / 2 / offset);
        plant4.transform.localScale = Vector3.one * ((PlayerPrefs.GetFloat("stat7") + PlayerPrefs.GetFloat("stat8")) / 2 / offset);
    }
    public void usepot() {
        UpdateStat(); //this will cause issues lol
    }
    public void sharepot() {
        canTradePotions--;
        UpdateStat(); //this will cause issues lol
    }
    void plantOvertime() { //overtime removal of plant maybe someday

        DateTime localDate = DateTime.Now;
        DateTime tmp = DateTime.Parse(PlayerPrefs.GetString("LastDate", "" + localDate));
        hours = (localDate - tmp).TotalHours;
        float def = removal;
        removal = (float)hours / 2;
        //removal = (float)hours * 0.00027f * interval * removal;
        UpdateStat();
        removal = def;
    }
    void UpdateStat() {
        if (canTradePotions > 0)
            potionButt.interactable = true;
        else
            potionButt.interactable = false;
        TradePots.text = canTradePotions + " trades left. " + Mathf.CeilToInt(Mathf.Abs((float)hours - requiredPass)*60) + "m left til next drop."; //This might not be 100% accurate from testing?

        DateTime localDate = DateTime.Now;
        DateTime tmp = DateTime.Parse(PlayerPrefs.GetString("LastDate", "" + localDate));
        hours = (localDate - tmp).TotalHours;

        //my god use a forloop
        PlayerPrefs.SetFloat("stat1", Mathf.Clamp(PlayerPrefs.GetFloat("stat1", defaultStat) - removal, 0, max));
        PlayerPrefs.SetFloat("stat2", Mathf.Clamp(PlayerPrefs.GetFloat("stat2", defaultStat) - removal, 0, max));
        PlayerPrefs.SetFloat("stat3", Mathf.Clamp(PlayerPrefs.GetFloat("stat3", defaultStat) - removal, 0, max));
        PlayerPrefs.SetFloat("stat4", Mathf.Clamp(PlayerPrefs.GetFloat("stat4", defaultStat) - removal, 0, max));
        PlayerPrefs.SetFloat("stat5", Mathf.Clamp(PlayerPrefs.GetFloat("stat5", defaultStat) - removal, 0, max));
        PlayerPrefs.SetFloat("stat6", Mathf.Clamp(PlayerPrefs.GetFloat("stat6", defaultStat) - removal, 0, max));
        PlayerPrefs.SetFloat("stat7", Mathf.Clamp(PlayerPrefs.GetFloat("stat7", defaultStat) - removal, 0, max));
        PlayerPrefs.SetFloat("stat8", Mathf.Clamp(PlayerPrefs.GetFloat("stat8", defaultStat) - removal, 0, max));
        
        if (hours > requiredPass) {
            PlayerPrefs.SetString("LastDate", "" + localDate);
            canTradePotions += Mathf.CeilToInt(requiredPass) * multiplier;
            print("WEEP WEEP BONUS!!");
        } //Horribly innificient lol
        PlayerPrefs.SetFloat("pots", canTradePotions);
        inventorySaver.SaveItems();
        GetStat();
    }
}
