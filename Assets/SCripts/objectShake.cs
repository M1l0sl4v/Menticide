using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using Random = UnityEngine.Random;

public class objectShake : MonoBehaviour
{
    public float shakeTime;
    public float shakeAmount;

    private Vector2 ogPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        ogPosition = transform.localPosition;
        if (other.CompareTag("Player"))
        { 
            shakeObject();
            Debug.Log("hitplayer");
        }
        
    }

    public void shakeObject()
    {
        StartCoroutine(shake());
    }


    private IEnumerator shake()
    {
        float elapsed = 0.0f;


        while (elapsed < shakeTime)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeAmount;
            float offsetY = Random.Range(-1f, 1f) * shakeAmount;
            
            transform.localPosition = ogPosition + new Vector2(offsetX, offsetY);
            
            elapsed += Time.deltaTime;
            
            Debug.Log("shaken");
            
            yield return null;
        }
        
        transform.localPosition = ogPosition;
        
    }
}