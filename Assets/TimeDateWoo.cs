using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDateWoo : MonoBehaviour {
    float requiredPass = .1f;
    // Start is called before the first frame update
    void Awake() {
        DateTime localDate = DateTime.Now;
        DateTime tmp = DateTime.Parse(PlayerPrefs.GetString("LastDate", "" + localDate));
        print(localDate);
        print(tmp);
        double hours = (localDate - tmp).TotalHours;
        if (hours > requiredPass) {
            PlayerPrefs.SetString("LastDate", "" + localDate);
            print("WEEP WEEP BONUS!!");
        } //Horribly innificient //also only do this once player already received potion?
        print(hours);
        print(hours > requiredPass ? requiredPass + " hours passed" : requiredPass + " hours have not passed.");
        print(Mathf.Abs((float)(hours - requiredPass)) + (hours - requiredPass > 0 ? " passed!!" : " left!!"));
    }
}
