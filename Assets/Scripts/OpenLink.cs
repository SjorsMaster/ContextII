using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void Uri(string input) { 
    Application.OpenURL(input);
    }
}
