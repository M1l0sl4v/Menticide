using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class tilemanager : MonoBehaviour
{
    public float scrollspeed;
    public float tileresetdistance;
    public static tilemanager instance;

    private void Start()
    {
        instance = this;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            Vector3 newPosition = transform.position;
            newPosition.y += tileresetdistance;  
            transform.position = newPosition;
        }
    }

    public void backToZero()
    {
        float startposition = 0;
        Vector3 newPosition = transform.position;
        newPosition.y = startposition;  
        transform.position = newPosition;
    }
}
