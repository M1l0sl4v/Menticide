using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticDebugTools : MonoBehaviour
{
    public static StaticDebugTools instance;

    [Header("Player")]
    public bool playerInvincibility;
    public bool playerMoveSpeedOverride;
    public float overrideValue;
    public int damagePlayer;

    [Header("Game")]
    public bool pauseAfterEveryRow;
    public enum Algorithm
    {
        Cache,
        Picker
    }
    public Algorithm tileManagerAlgorithm;

    [Header("HighScore")]
    public List<string> topNames;
    public List<int> topScores;
    public bool resetHighScores;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType<StaticDebugTools>().Length > 1) Destroy(GameObject.FindGameObjectWithTag("Static"));
        gameObject.tag = "Untagged";
        if (!instance) instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoveSpeedOverride) { playermovement.instance.speed = overrideValue; }

        if (damagePlayer > 0)
        {
            bool before = playerInvincibility;
            playerInvincibility = false;
            playermovement.instance.TakeDamage(damagePlayer);
            playerInvincibility = before;
            damagePlayer = 0;
        }

        if (resetHighScores)
        {
            ScoreManager.ClearSavedHighScores();
            resetHighScores = false;
        }

        topNames = ScoreManager.topNames;
        topScores = ScoreManager.topScores;
    }

    public void ChangeAlgorithm(int algorithm)
    {
        tileManagerAlgorithm = (Algorithm)algorithm;
    }
}
