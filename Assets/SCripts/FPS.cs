using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public TMP_Text display;
    [Tooltip("How often (in seconds) should fps be calculated")]
    public float calculationInterval;

    public int framesPerSecond;
    private float elapsedTime;
    private int frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        frameCounter++;

        if (elapsedTime >= calculationInterval)
        {
            elapsedTime -= calculationInterval;
            framesPerSecond = Mathf.RoundToInt(frameCounter / calculationInterval);
            frameCounter = 0;
        }


        if (display != null)
        {
            display.text = framesPerSecond + " fps";
        }
    }
}
