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
        summer();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void summer()
    {
        cameraMain.backgroundColor = new Color(14f / 255,48f / 255, 7f / 255, 1);//we devide by 255 to get the rgb value
    }
    public void fall()
    {
        cameraMain.backgroundColor = new Color(48f / 255, 32f / 255, 7f / 255, 1);
    }
    public void winter()
    {
        cameraMain.backgroundColor = new Color(200f / 255, 194f / 255, 186f / 255, 1);
    }
    public void spring()
    {
        cameraMain.backgroundColor = new Color(118f / 255, 114f / 255, 24f / 255, 1);
    }
}
