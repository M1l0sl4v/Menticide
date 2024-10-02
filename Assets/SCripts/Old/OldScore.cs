using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class OldScore : MonoBehaviour
{

    public TMP_Text scoreText;

    public bool displayDebugInfo;
    public GameObject debugInfo;
    private int totalDistance;
    private int distanceInMonth;
    private int distanceInSeason;
    private int distanceInYear;

    private playermovement player;

    private string yearComponent;
    private string monthComponent;
    private string commaComponent;

    public int seasonLength;
    public int monthLength;

    private int month;
    private int year;
    private int season;

    private TMP_Text debugSeason;
    private TMP_Text debugUnitsInMonth;
    private TMP_Text debugUnitsInSeason;
    private TMP_Text debugUnitsInYear;
    private TMP_Text debugTotalUnits;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playermovement>();

        // Debug info
        debugSeason = debugInfo.transform.Find("season").GetComponent<TMP_Text>();
        debugUnitsInMonth = debugInfo.transform.Find("units in month").GetComponent<TMP_Text>();
        debugUnitsInSeason = debugInfo.transform.Find("units in season").GetComponent<TMP_Text>();
        debugUnitsInYear = debugInfo.transform.Find("units in year").GetComponent<TMP_Text>();
        debugTotalUnits = debugInfo.transform.Find("units total").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Display/hide debug info
        debugInfo.SetActive(displayDebugInfo);
        CalculateDebug();

        // Update the score display
        UpdateScore();

        // Display main score
        CalculateDistance();

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

    private void CalculateDistance()
    {
        distanceInSeason = (int) player.transform.position.y;

        distanceInYear = season * seasonLength + distanceInSeason;
        totalDistance = (year * seasonLength * 4) + distanceInYear;
        distanceInMonth = totalDistance % monthLength;

        if (distanceInSeason >= seasonLength)
        {
            player.playerReset();
            season++;

        }
        if (season == 4)
        {
            year++;
            season = 0;
        }
        month = distanceInYear / monthLength;

    }

    private void CalculateDebug()
    {
        // Season
        switch (season)
        {
            case 0: debugSeason.text = "Season 0 (Summer)"; break;
            case 1: debugSeason.text = "Season 1 (Fall)"; break;
            case 2: debugSeason.text = "Season 2 (Winter)"; break;
            case 3: debugSeason.text = "Season 3 (Spring)"; break;
        }

        // Units in month
        debugUnitsInMonth.text = distanceInMonth + " units in month";

        // Units in season
        debugUnitsInSeason.text = distanceInSeason + " units in season";

        // Units in year
        debugUnitsInYear.text = distanceInYear + " units in year";

        // Units total
        debugTotalUnits.text = totalDistance + " units total";
    }
}
