using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Since there seems to be an issue sometimes when setting the variables
/// There is this script to set them all proper
/// Now, this was meant to be a temp script,
/// but due to time constraints it will have to stay
/// </summary>
public class InitialPrefs : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetFloat("Happiness", PlayerPrefs.GetFloat("Happiness", .8f));
        PlayerPrefs.SetFloat("Hunger", PlayerPrefs.GetFloat("Hunger", .75f));
        PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("Health", 1f));
        PlayerPrefs.SetFloat("Energy", PlayerPrefs.GetFloat("Energy", 1f));
        PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money", 25f));
    }

}
