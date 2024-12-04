using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    [Header("Video Settings")]
    public VideoClip clip;
    public VideoPlayer player;
    public float beforeVideoPadding;
    public float afterVideoPadding;
    [Header("Skip Cutscene Settings")]
    public float timeToSkip;
    public float barProgress;
    public Image progressBar;
    // Start is called before the first frame update
    void Start()
    {
        player.clip = clip;
        Invoke("PlayVideo", beforeVideoPadding);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            barProgress += Time.deltaTime;
        }
        else barProgress = 0f;

        UpdateBar();

        if (barProgress >= timeToSkip)
        {
            LoadNext();
            //Invoke("LoadNext", 1.5f);
            //Blackout.instance.On();
        }
    }

    void UpdateBar()
    {
        progressBar.fillAmount = barProgress / timeToSkip;
    }

    void LoadNext()
    {
        Debug.LogWarning("switching scene");
        SceneManager.LoadScene(2);
    }

    void PlayVideo()
    {
        player.Play();
        player.GetComponent<SpriteRenderer>().enabled = true;
        Invoke("LoadNext", (float)clip.length + afterVideoPadding);
    }
}
