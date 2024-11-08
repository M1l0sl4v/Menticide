using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

public class cameraShake : MonoBehaviour
{
    public static cameraShake instance;

    private CinemachineVirtualCamera vCam;

    private float shakeTime;
    private float shakeTimerTotal;
    private float startingIntensity;
    
    private void Awake()
    {
        instance = this;
        vCam = GetComponent<CinemachineVirtualCamera>();
    }

    public void shakeCamera(float intensity, float duration)
    {
        CinemachineBasicMultiChannelPerlin channel = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        channel.m_AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTime = duration;
        shakeTimerTotal = duration;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
                CinemachineBasicMultiChannelPerlin channel = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                channel.m_AmplitudeGain = 0;
                channel.m_AmplitudeGain =  Mathf.Lerp(startingIntensity, 0f, 1-(shakeTime/shakeTimerTotal));
                //
        }
    }
}
