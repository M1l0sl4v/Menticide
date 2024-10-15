using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TileManagerFSM;

public class Score : MonoBehaviour
{
    // High score
    public static int highScore;


    // Toggle debug info
    public bool displayDebugInfo;

    private int score;
    private int month;
    private int year;

    private string monthComponent;
    private string commaComponent;
    private string yearComponent;

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

        UpdateScore();
        UpdateDebug();
    }


    private void UpdateScore()
    {
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
        scoreText.text = yearComponent + commaComponent + monthComponent;
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
        return yearComponent + commaComponent + monthComponent;
    }

    public int ScoreAsInt()
    {
        return score;
    }

    public void CheckForHighScore()
    {
        
    }


}