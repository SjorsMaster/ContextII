using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetData : MonoBehaviour
{
    public void DeleteData() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
