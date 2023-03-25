using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Petwork.Additions.petDefinitions;


/// <summary>
/// This is a simple rock paper scissors script
/// If I had more time I'd make the numbers a bit less hardcoded
/// </summary>
public class RockPaperScissors : MonoBehaviour
{
    //To make the code more readable I dropped an enum for the choices
    public enum choice
    {
        Rock,
        Paper,
        Scissors
    }

    [SerializeField] List<Sprite> Items;

    int lastOutcome = -1;

    //Required sources for player feedback
    [SerializeField] TextMesh text;
    [SerializeField] ParticleSystem particlesHappy, particlesSad;
    [SerializeField] SpriteRenderer mouth, eyes, computerItem;
    [SerializeField] Animator newItem;

    [SerializeField] Expressions expressions;

    [SerializeField] GameObject body;

    //Check if the character isn't already tired and adjust the body on health
    private void Start()
    {
        if(PlayerPrefs.GetFloat("Energy") <= 0f) Tired();
        body.transform.localScale = new Vector3((1 + Mathf.Abs(1 - (PlayerPrefs.GetFloat("Health")/4)) * 4), body.transform.localScale.y, body.transform.localScale.z);
    }

    //If the player plays, check if he won or lost against the CPU, but before that see if there's enough energy.
    public void PlayerInput(int input)
    {
        if (PlayerPrefs.GetFloat("Energy") >= 0f){
            //Convert and pick the items for both players
            choice playerChoice = (choice)input;
            choice computerChoice = (choice)Random.Range(0, System.Enum.GetNames(typeof(choice)).Length);

            computerItem.sprite = Items[(int)computerChoice];
            newItem.Play("Wiggle");

            //Maybe put it in a function?

            //If the player has the stronger item
            if (playerChoice == choice.Rock && computerChoice == choice.Scissors ||
                playerChoice == choice.Paper && computerChoice == choice.Rock ||
                playerChoice == choice.Scissors && computerChoice == choice.Paper)
            {
                Win();
            }
            //If the computer has the stronger item
            else if (computerChoice == choice.Rock && playerChoice == choice.Scissors ||
                     computerChoice == choice.Paper && playerChoice == choice.Rock ||
                     computerChoice == choice.Scissors && playerChoice == choice.Paper)
            {
                Lose();
            }
            //If there's a draw
            else
            {
                text.text = $"It's {(0.Equals(lastOutcome) ? "another " : "a ")}draw!";
                mouth.sprite = expressions.mouth_neutral;
                eyes.sprite = expressions.eyes_neutral;
                lastOutcome = 0;
            }
            PlayerPrefs.SetFloat("Energy", PlayerPrefs.GetFloat("Energy") <= 0 ? 0 : PlayerPrefs.GetFloat("Energy") - 0.05f);
        }
        else
        {
            //Character is tired
            Tired();
        }
    }

    //Change on tired
    void Tired()
    {
        text.text = $"I'm too tired to play!";
        mouth.sprite = expressions.mouth_sad;
        eyes.sprite = expressions.eyes_sad;
        computerItem.sprite = null;
    }

    //If the player won
    void Win()
    {
        text.text = $"Seems like.. " +
        $"\nYou've won.." +
        $"{ (1.Equals(lastOutcome) ? "\nAgain.." : "")}";
        PlayerPrefs.SetFloat("Happiness", PlayerPrefs.GetFloat("Happiness") <= 0 ? 0 : PlayerPrefs.GetFloat("Happiness") - (1.Equals(lastOutcome) ? .025f : .045f));
        mouth.sprite = expressions.mouth_sad;
        eyes.sprite = expressions.eyes_sad;
        particlesSad.Play();
        PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + 0.25f);
        lastOutcome = 1;
        return;
    }

    //If the player lost
    void Lose()
    {
        PlayerPrefs.SetFloat("Happiness", PlayerPrefs.GetFloat("Happiness") >= 1 ? 1 : PlayerPrefs.GetFloat("Happiness") + (1.Equals(lastOutcome) ? .05f : .075f));
        text.text = $"I won,\n<b>Woohoo!</b>" +
        $"{ (2.Equals(lastOutcome) ? "\nI'm on a roll!" : "")}";
        mouth.sprite = expressions.mouth_happy;
        eyes.sprite = expressions.eyes_happy;
        particlesHappy.Play();
        PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + 0.10f);
        lastOutcome = 2;
        return;
    }
}
