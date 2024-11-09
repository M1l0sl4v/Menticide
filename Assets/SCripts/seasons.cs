using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class seasons : MonoBehaviour
{
    
    public static seasons instance;

    public int currentSeason;
    private int nextSeason;

    public GameObject backGround;
    //public UnityEvent seasonChanged;
    private enemyspawner[] spawners;
    public static int monthLength = 50;
    public static int seasonLength = 150; // should be 3x month length
    public static int transitionAfter = 100; // start the season change after this long, lasting until end of season

    public Color summerColorNotWork = Color.blue;
    public Color fallColorNotWork = Color.green;
    public Color winterColorNotWork = Color.magenta;
    public Color springColorNotWork = Color.yellow;

    private Color colorTransition;
    // this is whats used to store the season 1=summer,2=fall,3=winter,4=spring
    void Start()
    {
        instance = this;
        backGround.GetComponent<SpriteRenderer>().color = new Color(14f / 255, 48f / 255, 7f / 255, 1);
        summer();
        spawners = FindObjectsOfType<enemyspawner>();
    }

    // Update is called once per frame
    void Update()
    {
        backGround.GetComponent<Renderer>().material.color = Color.Lerp(backGround.GetComponent<Renderer>().material.color, colorTransition, 1f * Time.deltaTime);

    }
//switch statement cycles through the seasons and calls their methods. just calling this method changes the season.
    public void seasonChange()
    {
            switch (nextSeason)
                    {
                        case 1:
                            summer();
                            break;
                        case 2:
                            fall();
                            break;
                        case 3:
                            winter();
                            break;
                        case 4:
                            spring();
                            break;
                        default:
                            return;
                    }
        //seasonChanged.Invoke();
        foreach (var spawner in spawners)
        {
            if (spawner.scalingType == enemyspawner.ScalingType.Seasonal) spawner.IncreaseChance();
        }
        WeatherManager.instance.ChangeWeather();//call the weather from that season
    }
    
    //each season has a method here, its set up so we should be able to kinda go ham with the seasons.
    public void summer()
    {
        colorTransition = new Color(14f / 255,48f / 255, 7f / 255, 1);//we devide by 255 to get the rgb value
        TileManagerFSM.instance.season = TileSprite.Season.Summer;
        currentSeason = 1;
        nextSeason = 2; //passes this to fall
    }
    public void fall()
    {
        colorTransition=new Color(48f / 255, 32f / 255, 7f / 255, 1);//we devide by 255 to get the rgb value
        TileManagerFSM.instance.season = TileSprite.Season.Fall;
        currentSeason = 2;
        nextSeason = 3;//passes this to winter
    }
    public void winter()
    {
        colorTransition=new Color(255f / 255, 255f / 255, 255f / 255, 1);//we devide by 255 to get the rgb value
        TileManagerFSM.instance.season = TileSprite.Season.Winter;
        currentSeason = 3;
        nextSeason = 4; //passes this to spring
    }
    public void spring()
    {
        colorTransition=new Color(118f / 255, 114f / 255, 24f / 255, 1);//we devide by 255 to get the rgb value
        TileManagerFSM.instance.season = TileSprite.Season.Spring;
        currentSeason = 4;
        nextSeason = 1;// loops back to summer
    }

}
