using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathSequence : MonoBehaviour
{
    public static DeathSequence instance;
    public static bool controlLock;

    [Header("Text Fade In Speed")]
    public float fadeInSpeed;

    [Header("Blackout")]
    public GameObject blackoutPanel;

    [Header("Death Message")]
    public TMP_Text deathMessageObject; // text object to display the death message
    public string[] deathMessages; // possible death messages to choose from
    public float deathMessageDelay;

    [Header("Score")]
    public TMP_Text scoreText; // score display
    public float scoreDelay;

    [Header("Buttons")]
    public GameObject menuButtons;
    public TMP_Text[] buttons; // menu button text
    public float endScreenDelay;


    private float deathMessageAlpha;
    private bool deathMessageActive;

    private bool menuActive;
    private float buttonAlpha;

    private float scoreTextAlpha;
    private bool scoreTextActive;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuActive && buttonAlpha < 1)
        {
            foreach (TMP_Text button in buttons)
            {
                button.color = new Color(1, 0, 0, buttonAlpha);
            }
            buttonAlpha += fadeInSpeed;
        }

        if (deathMessageActive && deathMessageAlpha < 1)
        {
            deathMessageObject.color = new Color(1, 0, 0, deathMessageAlpha);
            deathMessageAlpha += fadeInSpeed;
        }

        if (scoreTextActive && scoreTextAlpha < 1)
        {
            scoreText.color = new Color(1, 0, 0, scoreTextAlpha);
            scoreTextAlpha += fadeInSpeed;
        }
    }

    public void StartDeathSequence()
    {
        StartCoroutine(_StartDeathSequence());
    }

    private IEnumerator _StartDeathSequence()
    {
        controlLock = true;
        UIStack.Push(blackoutPanel);
        foreach (TMP_Text button in buttons)
        {
            button.color = Color.black;
        }
        deathMessageObject.color = Color.black;
        scoreText.color = Color.black;

        yield return new WaitForSeconds(deathMessageDelay);
        deathMessageActive = true;
        deathMessageObject.text = deathMessages[Random.Range(0, deathMessages.Length)];

        yield return new WaitForSeconds(scoreDelay);
        scoreTextActive = true;
        scoreText.text = "You survived " + Score.instance.ScoreAsString();
        yield return new WaitForSeconds(endScreenDelay);
        UIStack.Push(menuButtons);
        menuActive = true;
        yield return null;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        controlLock = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
        controlLock = false;
    }


}
