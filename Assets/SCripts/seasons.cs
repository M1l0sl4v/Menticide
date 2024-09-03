using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seasons : MonoBehaviour
{
    
    public static seasons instance;
    public Camera cameraMain;//this sets the imported camera to cameraMain
    public int season;
    // this is whats used to store the season 1=summer,2=fall,3=winter,4=spring
    void Start()
    {
        instance = this;
        summer();
    }

    // Update is called once per frame
    void Update()
    {
    }
//switch statement cycles through the seasons and calls their methods. just calling this method changes the season.
    public void seasonChange()
    {
            switch (season)
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
        Debug.Log("seasoncahnged");
    }
    
    public void summer()
    {
        cameraMain.backgroundColor = new Color(14f / 255,48f / 255, 7f / 255, 1);//we devide by 255 to get the rgb value
        season = 2; //passes this to fall
    }
    public void fall()
    {
        cameraMain.backgroundColor = new Color(48f / 255, 32f / 255, 7f / 255, 1);
        season = 3;//passes this to winter
    }
    public void winter()
    {
        cameraMain.backgroundColor = new Color(200f / 255, 194f / 255, 186f / 255, 1);
        season = 4; //passes this to spring
    }
    public void spring()
    {
        cameraMain.backgroundColor = new Color(118f / 255, 114f / 255, 24f / 255, 1);
        season = 1;// loops back to summer
    }
    
}
