using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This manages all the sporting
/// If I had more time I'd split this script up probably
/// It manages more things than I'd like
/// </summary>
public class Fitness : MonoBehaviour
{
    [SerializeField] TextMesh textCloud;
    [SerializeField] Text textEnergy;
    [SerializeField] int count;
    [SerializeField] Animator clip;
    [SerializeField] GameObject body;
    [SerializeField] ParticleSystem sadParticles;

    //Avoid the text from showing the wrong value upon entering
    private void Start()
    {
        textEnergy.text = $"Energy: {Mathf.CeilToInt(PlayerPrefs.GetFloat("Energy") * 100)}";
    }

    //Check for taps, if there is being tapped, and there's enough energy, While he isn't currently jumping
    //Then deplete energy, add health, etc.
    //Otherwise if there's no energy then tell the player that's the case
    void Update()
    {
        body.transform.localScale = new Vector3((1 + Mathf.Abs(1 - (PlayerPrefs.GetFloat("Health") / 4)) * 4), body.transform.localScale.y, body.transform.localScale.z);
        if (Input.GetMouseButtonDown(0) && !clip.GetCurrentAnimatorStateInfo(0).IsName("Jump") && PlayerPrefs.GetFloat("Energy") > 0)
        {
            count++;
            textCloud.text = $"That's {count}!";
            PlayerPrefs.SetFloat("Health", PlayerPrefs.GetFloat("Health") >= 1 ? 1 : PlayerPrefs.GetFloat("Health") + .015f);
            PlayerPrefs.SetFloat("Energy", PlayerPrefs.GetFloat("Energy") <= 0 ? 0 : PlayerPrefs.GetFloat("Energy") - .01f);
            PlayerPrefs.SetFloat("Happiness", PlayerPrefs.GetFloat("Happiness") <= 0 ? 0 : PlayerPrefs.GetFloat("Happiness") - .005f);
            textEnergy.text = $"Energy: {Mathf.CeilToInt(PlayerPrefs.GetFloat("Energy") * 100)}";
            sadParticles.Play();
            clip.Play("Jump");

        }
        else if(PlayerPrefs.GetFloat("Energy") <= 0)
        {
            textCloud.text = "I'm too tired to jump!";
        }
    }
}
