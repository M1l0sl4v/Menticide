using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seasons : MonoBehaviour
{
    public Camera cameraMain;//this sets the imported camera to cameraMain
    int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        summer();//what season it starts on
    }

    // Update is called once per frame
    void Update()
    {
        x++;//this is temprary later we can call the season classes from anuther class to swich the seasons
        if (x == 2000)
        {
            fall();
        }
        if (x == 4000)
        {
            winter();
        }
        if (x == 6000)
        {
            spring();
        }
    }
    void summer()
    {
        cameraMain.backgroundColor = new Color(14f / 255,48f / 255, 7f / 255, 1);//we devide by 255 to get the rgb value
    }
    void fall()
    {
        cameraMain.backgroundColor = new Color(48f / 255, 32f / 255, 7f / 255, 1);
    }
    void winter()
    {
        cameraMain.backgroundColor = new Color(200f / 255, 194f / 255, 186f / 255, 1);
    }
    void spring()
    {
        cameraMain.backgroundColor = new Color(118f / 255, 114f / 255, 24f / 255, 1);
    }
}
