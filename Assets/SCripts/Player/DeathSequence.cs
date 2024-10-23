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
    public TMP_Text highScoreText;
    private bool isHighScore;
    public float highScoreMinSize;
    public float highScoreMaxSize;
    public float highScorePulseSpeed;
    private float highScoreCurSize;
    private bool growing;
    public TMP_InputField inputField;
    public GameObject leaderboard;

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

    private List<DeathScreenElement> deathScreenElements;
    private float timeElapsed;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        controlLock = false;
        highScoreCurSize = highScoreMinSize;
    }

    void FixedUpdate()
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
            if (isHighScore) highScoreText.color = new Color(1, 0, 0, scoreTextAlpha);

            scoreTextAlpha += fadeInSpeed;
        }

        //timeElapsed += Time.deltaTime;
        //foreach (var element in deathScreenElements)
        //{
        //    if (!element.complete && timeElapsed >= element.delay) element.FadeIn();
        //}

        if (isHighScore)
        {
            if (growing)
            {
                highScoreCurSize += highScorePulseSpeed;
                if (highScoreCurSize >= highScoreMaxSize) growing = false;
            }
            else
            {
                highScoreCurSize -= highScorePulseSpeed;
                if (highScoreCurSize <= highScoreMinSize) growing = true;
            }
            highScoreText.rectTransform.localScale = new Vector3(highScoreCurSize, highScoreCurSize, highScoreCurSize);
        }
    }

    public void StartDeathSequence()
    {
        StartCoroutine(_StartDeathSequence());
    }

    private IEnumerator _StartDeathSequence()
    {
        // Ready elements for fade-in
        controlLock = true;
        UIStack.Push(blackoutPanel);
        foreach (TMP_Text button in buttons)
        {
            button.color = Color.black;
        }
        deathMessageObject.color = Color.black;
        scoreText.color = Color.black;
        highScoreText.color = Color.black;
        PopulateLeaderboard();

        // Show death message
        yield return new WaitForSeconds(deathMessageDelay);
        deathMessageActive = true;
        deathMessageObject.text = deathMessages[Random.Range(0, deathMessages.Length)];

        // Show score
        yield return new WaitForSeconds(scoreDelay);
        scoreTextActive = true;
        // Check for high score
        if (Score.instance.ScoreAsInt() > Score.highScore) isHighScore = true;
        scoreText.text = "You survived " + Score.instance.ScoreAsString();

        // Show menu buttons
        yield return new WaitForSeconds(endScreenDelay);
        UIStack.Push(menuButtons);
        menuActive = true;
        inputField.Select();


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

    public void SaveScore()
    {
        Score.AddScore(inputField.text, Score.instance.ScoreAsInt());
    }

    public void PopulateLeaderboard()
    {
        int curPos = 0;
        foreach (Transform t in leaderboard.transform)
        {
            t.Find("Name").GetComponent<TMP_Text>().text = curPos < Score.topNames.Count ? Score.topNames[curPos] : "";
            t.Find("Score").GetComponent<TMP_Text>().text = curPos < Score.topScores.Count ? Score.ScoreToMessage(Score.topScores[curPos]) : "";
            
            curPos++;
        }
    }

    // Used for debugging, made static in case anyone else would find it helpful for debugging
    public static void LogCollection<T>(List<T> list)
    {
        string output = "";
        foreach (T t in list) { output += t.ToString() + " "; }
        Debug.LogWarning(list.Count + " items: " + output);
    }

    public static void LogCollection<T>(T[] array)
    {
        string output = "";
        foreach (T t in array) { output += t.ToString() + " "; }
        Debug.LogWarning(array.Length + " items: " + output);
    }

}
