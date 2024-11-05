using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
    public static Blackout instance;
    private Image image;

    private enum Fade
    {
        None,
        In,
        Out
    }
    private Fade fade;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        image = GetComponent<Image>();
    }

    private void Start()
    {
        // Start with a dramatic fadein
        image.color = Color.black;
        Off();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     switch (fade)
        {
            case Fade.None:
                break;
            case Fade.In:
                image.color = new Color (0,0,0, image.color.a + 0.015f);
                if (image.color.a >= 1f) fade = Fade.None;
                break;
            case Fade.Out:
                image.color = new Color(0, 0, 0, image.color.a - 0.015f);
                if (image.color.a <= 0f) fade = Fade.None;
                break;
        }   
    }

    public void On()
    {
        fade = Fade.In;
        GetComponent<Image>().raycastTarget = true;
    }

    public void Off()
    {
        fade = Fade.Out;
        GetComponent<Image>().raycastTarget = false;
    }
}
