using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public TMP_Text display;
    [Tooltip("How often (in seconds) should fps be calculated")]
    public float calculationInterval;

    public int framesPerSecond;
    private float elapsedTime;
    private int frameCounter;

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
