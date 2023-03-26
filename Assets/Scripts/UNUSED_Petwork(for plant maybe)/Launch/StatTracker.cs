using UnityEngine;
using UnityEngine.UI;
using Petwork.Additions;
using System;

/// <summary>
/// This is what manages the overtime stats
/// It would have more tooltips if I had a little more time.
/// </summary>

public class StatTracker : MonoBehaviour
{
    public delegate void MyStats(PetParameters stats);
    public static event MyStats myStats;

    [Tooltip("[?] This exists out of the current missed cycles based on last playtime, " +
        "\nThis way we can calculate on how the character is feeling after a long time of abondonment." +
        "\nA cycle is defined in seconds, and the missed time will be devided in it." +
        "\n\n[!] This value will never reach zero on play.")]
    [SerializeField] int currentMissedCycles;


    PetParameters param = new PetParameters();

    //Additions per cycles
    [SerializeField]
    Petwork.Additions.petDefinitions.CycleAdditions cycleAdditions = new Petwork.Additions.petDefinitions.CycleAdditions
    {
        Happiness = -.01f,
        Hunger = -.05f,
        Health = -.015f,
        Energy = +.15f,
        Money = +.25f
    };

    [Tooltip("How long it takes before the stats will drain.\n[!] CAUTION: this will only be in effect launch.")]
    [SerializeField] float cycleSeconds = 60;

    //Make sure this object doesn't dissapear or duplicate, and start the cycle
    void Start()
    {
        if (GameObject.Find(this.gameObject.name + "_orig")) Destroy(this.gameObject);
        gameObject.name = gameObject.name + "_orig";
        DontDestroyOnLoad(this.gameObject);
        InvokeRepeating("updateStats", 0f, cycleSeconds);
    }

    //Update the stats based on missed times.
    void updateStats()
    {
        param.lastActive = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString("dateTime", DateTime.Now.ToBinary().ToString())));
        PlayerPrefs.SetString("dateTime", DateTime.Now.ToBinary().ToString());

        currentMissedCycles = Mathf.CeilToInt(DateTime.Now.Subtract(param.lastActive).Seconds / cycleSeconds);

        for (int count = 0; count < currentMissedCycles; count++)
        {
            PlayerPrefs.SetFloat("Happiness", PlayerPrefs.GetFloat("Happiness") <= 0 ? 0 : PlayerPrefs.GetFloat("Happiness", .8f) + cycleAdditions.Happiness);
            PlayerPrefs.SetFloat("Hunger", PlayerPrefs.GetFloat("Hunger") <= 0 ? 0 : PlayerPrefs.GetFloat("Hunger", 1f) + cycleAdditions.Hunger);
            PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("Health") <= 0 ? 0 : PlayerPrefs.GetFloat("Health", 1f) + cycleAdditions.Health);
            PlayerPrefs.SetFloat("Energy", PlayerPrefs.GetFloat("Energy") >= 1 ? 1 : PlayerPrefs.GetFloat("Energy", 1f) + cycleAdditions.Energy);
            PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money", 25f) + cycleAdditions.Money);
            //Debug.Log($"Missed cycles:{cycles}, on {count+1}");
        }

        param.Happiness = PlayerPrefs.GetFloat("Happiness");
        param.Hunger = PlayerPrefs.GetFloat("Hunger");
        param.Health = PlayerPrefs.GetFloat("Health");
        param.Energy = PlayerPrefs.GetFloat("Energy");

        /*if (currentMissedCycles > 1)
        {
            //Maybe invoke popup like a welcome back?
        }*/

        myStats.Invoke(param);
    }

}
