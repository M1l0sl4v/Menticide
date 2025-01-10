using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSoundManager : MonoBehaviour
{

    public AudioClip hover;
    public AudioClip click;
    public AudioClip startGame;
    
    public void buttonHover()
    {
        AudioManager.instance.UIFX(hover, transform ,1f, 1);
    }
    public void buttonClick()
    {
        AudioManager.instance.UIFX(click, transform ,1f, 1);
    }
    public void startButton()
    {
        AudioManager.instance.UIFX(startGame, transform ,1f, 1);
    }
}
