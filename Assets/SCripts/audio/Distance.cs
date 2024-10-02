using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Distance : MonoBehaviour
{
    private AudioSource audioSource;
    public float maxDistance = 8f;    
    public float minDistance = 1f;     
    public float maxVolumeDb = 1f;     
    public float minVolumeDb = -10f;   
    private void Start()
    { 
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 currentPos = transform.position;
        float dist = Vector3.Distance(currentPos, playerPos);
        // Normalize the distance to a 0-1 range
        float normalizedDistance = Mathf.InverseLerp(minDistance, maxDistance, dist);
        
        // Interpolate between the max and min decibel values based on distance
        float volumeDb = Mathf.Lerp(maxVolumeDb, minVolumeDb, normalizedDistance);
        
        // Convert dB to linear volume
        float linearVolume = Mathf.Pow(10, volumeDb / 20);

        // Apply the calculated linear volume to the audio source
        audioSource.volume = linearVolume;
    }
}
