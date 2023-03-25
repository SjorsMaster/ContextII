using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {
    private void Start() {
        if (PlayerPrefs.HasKey("Personality")) {
            NextScene();
        }
    }
    public void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Dept(int input) {
        PlayerPrefs.SetInt("Department", input);
    }
    public void Pers(int input) {
        PlayerPrefs.SetInt("Personality", input);
    }
}
