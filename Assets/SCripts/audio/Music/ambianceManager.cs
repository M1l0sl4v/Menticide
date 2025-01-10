using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambianceManager : MonoBehaviour
{
    public static ambianceManager instance;

    [SerializeField] private musicLibrary musicLibrary;
    [SerializeField] private AudioSource musicSource;

    public float percent = 0;
    
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayAmbiance(string trackName, float fadeDuration = .2f)
    {
        StartCoroutine(AnimateMusicCrossfade(musicLibrary.GetClipFromName(trackName), fadeDuration));
    }
    
    public void stopAmbiance()
    {
        if (musicSource.isPlaying)
        {
            float fadeTime = .4f;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeTime;
                musicSource.volume = Mathf.Lerp(.3f, 0, percent);
            }
            musicSource.Stop();
        }
    }

    public void unPauseAmbiance()
    {
        if (!musicSource.isPlaying)
        {
            float fadeTime = .4f;
            while (percent < 1)
            {
                percent += Time.deltaTime * 1 / fadeTime;
                musicSource.volume = Mathf.Lerp(0, .5f, percent);
            }
            musicSource.UnPause();
        }
    }

    IEnumerator AnimateMusicCrossfade(AudioClip nextTrack, float fadeTime = .2f)
    {
        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / .1f;
            musicSource.volume = Mathf.Lerp(.3f, 0, percent);
            yield return null;
        }
 
        musicSource.clip = nextTrack;
        musicSource.Play();
 
        percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / fadeTime;
            musicSource.volume = Mathf.Lerp(0, .3f, percent);
            yield return null;
        }
    }
    
    
}
