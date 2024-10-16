using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public bool paused;
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

    public void Pause()
    {
        Time.timeScale = 0f;
        paused = true;
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

    public void Quit()
    {
        Application.Quit();
    }
}
