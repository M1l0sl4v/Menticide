using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    // High score

    public struct HighScoresSimple
    {
        public List<int> scores;
        public List<string> names;
        public int entryCount;

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }
    }
    public static HighScoresSimple highScores;
    private const string scoresName = "HighScores.json";
    public enum LeaderboardType
    {
        Local,
        Global
    }
    public LeaderboardType leaderboardType;
    public static LeaderboardType _leaderboardType;


    public static int highScore = 0;

    
    // Toggle debug info
    public bool displayDebugInfo;

    private int score;
    private int month;
    private int year;

    private int distanceInMonth;
    private int distanceInSeason;
    private int distanceInYear;
   
    // ScoreManager display
    public TMP_Text scoreText;

    // instance
    public static ScoreManager instance;

    // Debug info variables
    public Transform debugInfo;
    TMP_Text debugSeason;
    TMP_Text debugMonthDist;
    TMP_Text debugSeasonDist;
    TMP_Text debugYearDist;
    TMP_Text debugTotalDist;

    // enemy spawners (for spawn rate scaling)
    private enemyspawner[] spawners;

    //public UnityEvent scoreIncrease;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        // Debug info
        debugSeason = debugInfo.Find("season").GetComponent<TMP_Text>();
        debugMonthDist = debugInfo.Find("units in month").GetComponent<TMP_Text>();
        debugSeasonDist = debugInfo.Find("units in season").GetComponent<TMP_Text>();
        debugYearDist = debugInfo.Find("units in year").GetComponent<TMP_Text>();
        debugTotalDist = debugInfo.Find("units total").GetComponent<TMP_Text>();

        spawners = FindObjectsOfType<enemyspawner>();

        _leaderboardType = leaderboardType;
    }

    private void Start()
    {
        LoadHighScore();
    }
    // Update is called once per frame
    void Update()
    {
        // Update months
        if (distanceInMonth >= seasons.monthLength)
        {
            month++;
            distanceInMonth = 0;
            if (month == 12)
            {
                year++;
                distanceInYear = 0;
                month = 0;
            }
        }

        if (distanceInSeason >= seasons.seasonLength)
        {
            seasons.instance.seasonChange();
            distanceInSeason = 0;
        }


        // Toggle debug info display
        debugInfo.gameObject.SetActive(displayDebugInfo);

        // Disable score display during tutorial
        scoreText.transform.parent.gameObject.SetActive(!Tutorial.instance.disabledForTutorial);
    }


    // Increment the score by 1
    public void IncreaseScore()
    {
        IncreaseScore(1);
    }

    // Increase the score by amount
    public void IncreaseScore(int amount)
    {
        score += amount;
        distanceInMonth += amount;
        distanceInSeason += amount;
        distanceInYear += amount;

        scoreText.text = UpdateScore(year, month);
        UpdateDebug();

        if (distanceInSeason == TreeSpawner.treeSwitchPoint)
        {
            foreach (var spawner in FindObjectsOfType<TreeSpawner>())
            {
                spawner.AdvanceTreePool();
            }
        }

        //scoreIncrease.Invoke();

        foreach (var spawner in spawners)
        {
            if (spawner.scalingType == enemyspawner.ScalingType.Constant) spawner.IncreaseChance();
        }
    }


    private static string UpdateScore(int year, int month)
    {
        string yearComponent;
        string monthComponent;
        string commaComponent;

        // Determine year component
        if (year == 0) yearComponent = "";
        else if (year == 1) yearComponent = "1 Year";
        else yearComponent = year + " Years";

        // Determine whether or not to include a comma separator
        if (year > 0 && month > 0) commaComponent = ", ";
        else commaComponent = "";

        // Determine month componenet
        if (month == 0 && year == 0) monthComponent = "0 Months";
        else if (month == 0) monthComponent = "";
        else if (month == 1) monthComponent = "1 Month";
        else monthComponent = month + " Months";

        // Combine all componenets
        return yearComponent + commaComponent + monthComponent;
    }

    private void UpdateDebug()
    {
        // Season
        switch (seasons.instance.currentSeason)
        {
            case 1: debugSeason.text = "Season 1 (Summer)"; break;
            case 2: debugSeason.text = "Season 2 (Fall)"; break;
            case 3: debugSeason.text = "Season 3 (Winter)"; break;
            case 4: debugSeason.text = "Season 4 (Spring)"; break;
        }

        // Units in month
        debugMonthDist.text = distanceInMonth + " dist in month";

        // Units in season
        debugSeasonDist.text = distanceInSeason + " dist in season";

        // Units in year
        debugYearDist.text = distanceInYear + " dist in year";

        // Units total
        debugTotalDist.text = score + " dist total";
    }

    public int DistanceInSeason()
    {
        return distanceInSeason;
    }

    public string ScoreAsString()
    {
        return ScoreToMessage(score);
    }

    public int ScoreAsInt()
    {
        return score;
    }

    public static string ScoreToMessage(int score)
    {
        int month = score / seasons.monthLength;
        int year = month / 12;
        month %= 12;

        return UpdateScore(year, month);
    }


    public static void AddScore(string name, int score)
    {
        switch (_leaderboardType)
        {
            case LeaderboardType.Local:
                // Check for empty list
                if (highScores.entryCount == 0)
                {
                    highScores.scores.Add(score);
                    highScores.names.Add(name);
                    highScores.entryCount++;
                }
                // Check for high score
                else if (score > highScore)
                {
                    highScores.scores.Insert(0, score);
                    highScores.names.Insert(0, name);
                    highScores.entryCount++;
                }
                // Add score in middle
                else
                {
                    for (int i = highScores.scores.Count - 1; i >= 0; i--)
                    {
                        // if score to be added is less than the score to the left, add it to the right
                        if (score <= highScores.scores[i])
                        {
                            highScores.scores.Insert(i + 1, score);
                            highScores.names.Insert(i + 1, name);
                            highScores.entryCount++;
                            break;
                        }
                    }
                }
                // Update new high score
                highScore = highScores.scores[0];
                break;
            case LeaderboardType.Global:
                GlobalLeaderboard.AddFetch(name, score);
                break;
        }
        
    }


    public static string HighScoresToCSV()
    {
        string csv = "";

        for (int i = 0; i < highScores.scores.Count; i++)
        {
            csv += highScores.names[i] + ',' + highScores.scores[i] + '\n';
        }

        return csv;
    }

    public static string HighScoresToJSON()
    {
        return JsonUtility.ToJson(highScores);
    }

    public static void SaveHighScore()
    {
        //File.WriteAllText(Path.Combine(Application.persistentDataPath, scoresName), HighScoresToJSON());
        SaveSystem.SaveData(highScores, scoresName, true);
    }

    public void LoadHighScore()
    {
        switch (_leaderboardType)
        {
            case LeaderboardType.Local:
                string path = Path.Combine(Application.persistentDataPath, scoresName);

                if (File.Exists(path))
                {
                    highScores = JsonUtility.FromJson<HighScoresSimple>(File.ReadAllText(path));
                    highScore = highScores.entryCount > 0 ? highScores.scores[0] : 0;
                }
                else
                {
                    highScores.scores = new();
                    highScores.names = new();
                    highScores.entryCount = 0;
                }
                break;
            case LeaderboardType.Global:
                GlobalLeaderboard.FetchScores();
                Invoke("FillHighScore", 1f);
                break;
        }
        
    }

    private void FillHighScore()
    {
        highScore = highScores.entryCount > 0 ? highScores.scores[0] : 0;
    }


    private void OnApplicationQuit()
    {
        SaveHighScore();
        PlayerPrefs.Save();
    }

    public static void ClearSavedHighScores()
    {
        highScores = new HighScoresSimple();
        SaveSystem.SaveData(highScores, scoresName);
        highScore = 0;
    }

    public static void RemoveEntry(string name)
    {
        while (highScores.names.Contains(name))
        {
            highScores.scores.RemoveAt(highScores.names.IndexOf(name));
            highScores.names.Remove(name);
            highScores.entryCount--;
            SaveSystem.SaveData(highScores, scoresName);
        }
    }
    
}