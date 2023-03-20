using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Petwork.Additions.petDefinitions;

public class PetBase : MonoBehaviour
{
    [SerializeField] petState myState;
    [SerializeField] [Range(0, 1)] float neutralFrom = .75f, sadFrom = .25f;

    [SerializeField] SpriteRenderer mouth, eyes;
    [SerializeField] Expressions expressions;

    //Subscribe to stattracker and check if setup has been done. Also assign the values for reference
    void Start()
    {
        StatTracker.myStats += UpdateStats;
        if (PlayerPrefs.GetInt("InitialSetup", 1) == 1) Setup();

        Petwork.Additions.PetParameters tmp = new Petwork.Additions.PetParameters
        {
            Happiness = PlayerPrefs.GetFloat("Happiness"),
            Hunger = PlayerPrefs.GetFloat("Hunger"),
            Health = PlayerPrefs.GetFloat("Health"),
            Energy = PlayerPrefs.GetFloat("Energy")
        };
        UpdateStats(tmp);
    }

    //Initial setup
    //Apparently this doesn't go through properly
    void Setup()
    {
        myState = petState.Neutral;
        PlayerPrefs.SetFloat("Happiness", .5f);
        PlayerPrefs.SetFloat("Hunger", .5f);
        PlayerPrefs.SetFloat("Health", .5f);
        PlayerPrefs.SetFloat("Energy", .5f);
        PlayerPrefs.SetInt("InitialSetup", 0);
        PlayerPrefs.SetString("PetName", "Bob");
    }

    [SerializeField] GameObject body;

    //Check the parameters and set the mood based on that
    private void UpdateStats(Petwork.Additions.PetParameters stats)
    {
        body.transform.localScale = new Vector3((1 + Mathf.Abs(1 - (PlayerPrefs.GetFloat("Health") / 4)) * 4), body.transform.localScale.y, body.transform.localScale.z);

        if (stats.Happiness > neutralFrom)
        {
            myState = petState.Happy;
        }
        else if (stats.Happiness <= neutralFrom && stats.Happiness > sadFrom)
        {
            myState = petState.Neutral;
        }
        else
        {
            myState = petState.Sad;
        }
    }

    //Execute the current mood
    private void Update()
    {
        switch (myState)
        {
            case petState.Sad:
                Sad();
                break;
            case petState.Neutral:
                Neutral();
                break;
            case petState.Happy:
                Happy();
                break;
                /*case petState.Hungry:
                    //Hungry
                    break;
                case petState.Angry:
                    //Angry
                    break;*/
                //Due to time constraints, these other options will not be added
        }
    }

    void Happy()
    {
        mouth.sprite = expressions.mouth_happy;
        eyes.sprite = expressions.eyes_happy;
        //Add behaviour for happy
    }

    void Neutral()
    {
        mouth.sprite = expressions.mouth_neutral;
        eyes.sprite = expressions.eyes_neutral;
        //Add behaviour for neutral
    }

    void Sad()
    {
        mouth.sprite = expressions.mouth_sad;
        eyes.sprite = expressions.eyes_sad;
        //Add behaviour for sad
    }

}
