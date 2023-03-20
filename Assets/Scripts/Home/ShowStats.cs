using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This is for the UI to show all the stats
/// </summary>
public class ShowStats : MonoBehaviour
{
    //Get the values at launch and subscribe to delegate
    void Start()
    {
        petName.text = PlayerPrefs.GetString("PetName");
        happiness.value = PlayerPrefs.GetFloat("Happiness", .8f);
        health.value = PlayerPrefs.GetFloat("Health",1f);
        hunger.value = PlayerPrefs.GetFloat("Hunger",1f);
        energy.value = PlayerPrefs.GetFloat("Energy", 1f);
        cashA.text = $"${PlayerPrefs.GetFloat("Money", 25f).ToString("F2")}";
        cashB.text = $"${PlayerPrefs.GetFloat("Money", 25f).ToString("F2")}";

        StatTracker.myStats += UpdateStats;
    }

    [SerializeField] Slider health, happiness, hunger, energy;
    [SerializeField] Text petName, cashA, cashB;

    // Update stats upon new values
    void UpdateStats(Petwork.Additions.PetParameters stats)
    {
        happiness.value = stats.Happiness;
        health.value = stats.Health;
        hunger.value = stats.Hunger;
        energy.value = stats.Energy;
    }
}
