using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    
    public bool paused;
    public TMP_Text pauseText;
    private float minAplha = 0f;
    private float maxAplha = 1f;
    private bool fading = true;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !DeathSequence.controlLock)
        {
            TogglePause();
        }

        pauseMenu.SetActive(paused);
    }

    void FixedUpdate()
    {
        // yeah this doesn't work cause the game is paused when it would run but that's fine
        if (paused)
        {
            if (fading)
            {
                pauseText.color = new Color(1, 0, 0, pauseText.color.a - 0.025f);
                if (pauseText.color.a <= minAplha) fading = false;
            }
            else
            {
                pauseText.color = new Color(1, 0, 0, pauseText.color.a + 0.025f);
                if (pauseText.color.a >= maxAplha) fading = true;
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
        pauseText.color = Color.red;
    }

    public void Unpause()
    {
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void TogglePause()
    {
        Time.timeScale = Mathf.Abs(Time.timeScale - 1.0f);
        paused = !paused;
    }

    public void Menu(float delay)
    {
        Blackout.instance.On();
        StartCoroutine(_Menu(delay));
    }

    IEnumerator _Menu(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(0);
    }

    public void Quit(float delay)
    {
        Blackout.instance.On();
        StartCoroutine(_Quit(delay));
    }

    IEnumerator _Quit(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}
