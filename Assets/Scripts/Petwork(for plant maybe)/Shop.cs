using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Petwork.Food;

/// <summary>
/// This is the script for the shop
/// </summary>
public class Shop : MonoBehaviour
{
    //Components for the UI
    [SerializeField] List<Food> shopFoods;
    [SerializeField] int selected;
    public Text description, buyText;
    [SerializeField] Image displayFood;
    [SerializeField] Animator clip;
    [SerializeField] Text cashA, cashB;

    //Required for the checkup to make sure we're within bounds
    List<string> Foods = new List<string>
    {
        "Happiness",
        "Hunger",
        "Health",
        "Energy"
    };

    //Making sure the UI is on there properly
    private void Start()
    {
        NextFood(0);
        UpdateCash();
    }

    //Browse through items, and stay within bounds
    public void NextFood(int dir)
    {
        if (dir + selected < 0) selected = shopFoods.Count - 1;
        else if (dir + selected > shopFoods.Count - 1) selected = 0;
        else { selected = selected + dir; }
        displayFood.sprite = shopFoods[selected].Sprite;
        description.text = shopFoods[selected].Description;
        buyText.text = $"FEED FOR ${shopFoods[selected].Price.ToString("F2")}";
    }

    //Update the money text
    void UpdateCash()
    {
        cashA.text = $"${PlayerPrefs.GetFloat("Money", 25f).ToString("F2")}";
        cashB.text = $"${PlayerPrefs.GetFloat("Money", 25f).ToString("F2")}";
    }

    //See if the user has enough money
    //If not then wiggle the money
    //If they do then adjust the variables based upon the selected food
    //Afterwards make sure everything is within bounds
    //And update the cash
    public void Buy()
    {
        if (PlayerPrefs.GetFloat("Money") < shopFoods[selected].Price)
        {
            clip.Play("Broke");
        }
        else
        {
            PlayerPrefs.SetFloat("Happiness", PlayerPrefs.GetFloat("Happiness") + shopFoods[selected].Happiness);
            PlayerPrefs.SetFloat("Hunger", PlayerPrefs.GetFloat("Hunger") + shopFoods[selected].Feeds);
            PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("Health") + shopFoods[selected].Health);
            PlayerPrefs.SetFloat("Energy", PlayerPrefs.GetFloat("Energy") + shopFoods[selected].Energy);
            PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") - shopFoods[selected].Price);

            foreach(string Food in Foods)
            {
                if (PlayerPrefs.GetFloat(Food) <= 0) PlayerPrefs.SetFloat(Food, 0);
                if (PlayerPrefs.GetFloat(Food) >= 1) PlayerPrefs.SetFloat(Food, 1);
            }
        }
        UpdateCash();
    }
}
