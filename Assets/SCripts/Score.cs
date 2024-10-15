using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // High score
    public static int highScore = 0;

    public static List<int> topScores = new List<int>();
    public static List<string> topNames = new List<string>();

    
    // Toggle debug info
    public bool displayDebugInfo;

    private int score;
    private int month;
    private int year;

    private int distanceInMonth;
    private int distanceInSeason;
    private int distanceInYear;
   
    // Score display
    public TMP_Text scoreText;

    // instance
    public static Score instance;

    // Debug info variables
    public Transform debugInfo;
    TMP_Text debugSeason;
    TMP_Text debugMonthDist;
    TMP_Text debugSeasonDist;
    TMP_Text debugYearDist;
    TMP_Text debugTotalDist;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // Debug info
        debugSeason = debugInfo.Find("season").GetComponent<TMP_Text>();
        debugMonthDist = debugInfo.Find("units in month").GetComponent<TMP_Text>();
        debugSeasonDist = debugInfo.Find("units in season").GetComponent<TMP_Text>();
        debugYearDist = debugInfo.Find("units in year").GetComponent<TMP_Text>();
        debugTotalDist = debugInfo.Find("units total").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update months
        if (distanceInMonth >= seasons.monthLength)
        {
            month++;
            distanceInMonth = 0;
        }

        if (distanceInSeason >= seasons.seasonLength)
        {
            seasons.instance.seasonChange();
            distanceInSeason = 0;
        }

        if (distanceInYear >= seasons.seasonLength * 4)
        {
            year++;
            distanceInYear = 0;
            month = 1;
        }

        // Toggle debug info display
        debugInfo.gameObject.SetActive(displayDebugInfo);
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
        int year = month / (seasons.seasonLength * 4);
        month = month % 12;

        return UpdateScore(year, month);
    }


    public static void AddScore(string name, int score)
    {
        // Check for empty list
        if (topScores.Count == 0)
        {
            topScores.Add(score);
            topNames.Add(name);
        }
        // Check for high score
        else if (score > highScore)
        {
            topScores.Insert(0, score);
            topNames.Insert(0, name);
        }
        // Add score in middle
        else
        {
            for (int i = topScores.Count - 1; i >= 0; i--)
            {
                // if score to be added is less than the score to the left, add it to the right
                if (score <= topScores[i])
                {
                    topScores.Insert(i+1, score);
                    topNames.Insert(i+1, name);
                    break;
                }
            }
        }
        // Update new high score
        highScore = topScores[0];
    }

}