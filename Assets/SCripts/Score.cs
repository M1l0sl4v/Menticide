using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;

    private int distanceInMonth;
    private int distanceInSeason;
    private int distanceInYear;

    public int months;
    public int years;

    public static Score instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Update months
        if (distanceInMonth > seasons.monthLength)
        {
            months++;
            distanceInMonth = 0;
        }

        if (distanceInSeason > seasons.seasonLength)
        {
            
        }

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
    }

}