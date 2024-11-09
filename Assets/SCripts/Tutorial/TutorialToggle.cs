using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialToggle : MonoBehaviour
{
    public Image toggleButton;
    public Sprite[] buttonSprites;

    void Start()
    {
        SyncState();
    }

    public void SyncState()
    {
        toggleButton.sprite = Tutorial.startWithTutorial ? buttonSprites[0] : buttonSprites[1];
    }
    public void ToggleTutorial()
    {
        Tutorial.startWithTutorial = !Tutorial.startWithTutorial;
        SyncState();
    }
}
