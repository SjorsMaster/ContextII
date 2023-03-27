using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Petwork.Additions;
using UnityEngine.UI;

/// <summary>
/// This script is used for checkups at the start of the games
/// I should probably change the script name, but then I need to re-assign things,
/// and I'm already running out of time :^)
/// </summary>
public class Launch : MonoBehaviour
{
    [SerializeField] Object TargetScene;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("InitialSetup", 1) == 0)
        {
            FinishUp();
        }

    }

    public void FinishUp()
    {
        PlayerPrefs.SetInt("InitialSetup", 0);
        Tools.ChangeScene("Pet");
    }

    public void SetValue(GameObject input)
    {
        PlayerPrefs.SetString(input.name, input.GetComponent<Text>().text);
    }


}
