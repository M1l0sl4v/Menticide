using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathScreenElement
{
    private float fadeInSpeed;
    private float fadeInProgress;
    public float delay;
    public bool complete;
    private readonly GameObject gameObject;

    public DeathScreenElement(GameObject gameObject, float delay)
    {
        this.gameObject = gameObject;
        this.delay = delay;
        this.fadeInSpeed = 0.01f;
    }
    public DeathScreenElement(GameObject gameObject, float delay, float fadeInSpeed)
    {
        this.gameObject = gameObject;
        this.delay = delay;
        this.fadeInSpeed = fadeInSpeed;
    }

    public void FadeIn()
    {
        fadeInProgress += fadeInSpeed;
        SetAplha(gameObject.transform, fadeInProgress);
        complete = fadeInProgress >= 1;
    }

    private void SetAplha(Transform obj, float alpha)
    {
        foreach (TMP_Text text in obj.GetComponentsInChildren<TMP_Text>())
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            SetAplha(text.transform, alpha);
        }
    }

}
