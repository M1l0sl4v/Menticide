using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathSequence : MonoBehaviour
{
    public static DeathSequence instance;
    public static bool controlLock;

    public GameObject blackoutPanel;
    public GameObject endScreen;
    public float endScreenDelay;
    public float deathMessageDelay;

    public TMP_Text messageObject; // gameobject to display the death message
    public string[] deathMessages; // possible death messages to choose from
    public TMP_Text[] buttons; // menu button text

    private float buttonAlpha;
    private bool menuActive;
    private float deathMessageAlpha;
    private bool deathMessageActive;

    public float fadeInSpeed;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (menuActive && buttonAlpha < 1)
        {
            foreach (TMP_Text button in buttons)
            {
                button.color = new Color(1, 0, 0, buttonAlpha);
            }
            buttonAlpha += fadeInSpeed;
        }

        if (deathMessageActive && deathMessageAlpha < 1)
        {
            messageObject.color = new Color(1, 0, 0, deathMessageAlpha);
            deathMessageAlpha += fadeInSpeed;
        }
    }

    public void StartDeathSequence()
    {
        StartCoroutine(_StartDeathSequence());
    }

    private IEnumerator _StartDeathSequence()
    {
        controlLock = true;
        UIStack.Push(blackoutPanel);
        foreach (TMP_Text button in buttons)
        {
            button.color = Color.black;
        }
        messageObject.color = Color.black;
        yield return new WaitForSeconds(deathMessageDelay);
        deathMessageActive = true;
        messageObject.text = deathMessages[Random.Range(0, deathMessages.Length)];
        yield return new WaitForSeconds(endScreenDelay);
        UIStack.Push(endScreen);
        menuActive = true;
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        throw new System.NotImplementedException();
        SceneManager.LoadScene(0);
    }


}
