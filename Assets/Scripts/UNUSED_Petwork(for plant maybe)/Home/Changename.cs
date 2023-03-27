using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A script to get to the home screen without having to reset your data
/// This was the quickest way I could manage both the player name and pet name within time
/// </summary>
public class Changename : SceneMNGR
{
    public override void sceneRedirect(string scene)
    {
        PlayerPrefs.SetInt("InitialSetup", 1);
        base.sceneRedirect(scene);
    }
}
