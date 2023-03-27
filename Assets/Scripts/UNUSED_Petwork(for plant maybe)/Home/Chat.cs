using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script for the chatbubble
/// </summary>
public class Chat : MonoBehaviour
{
    [SerializeField]
    TextMesh bubble;

    [SerializeField]
    List<string> tutmessages, messages;
    public int count = 0;

    //Get a starting message
    private void Start()
    {
        newChatMessage();
    }

    //Get a new message when interacted
    public void newChatMessage()
    {
        //randomise message on state and replace names and such
        if (count >= tutmessages.Count) PlayerPrefs.SetInt("SeenTut", 1);
        bubble.text = (PlayerPrefs.GetInt("SeenTut", 0) == 1 ? messages[Random.Range(0, messages.Count)] : tutmessages[count++])
            .Replace("{name}", $"{PlayerPrefs.GetString("Name")}")
            .Replace("\\n", $"{System.Environment.NewLine}")
            .Replace("{petname}", $"{PlayerPrefs.GetString("PetName")}");
    }
}
