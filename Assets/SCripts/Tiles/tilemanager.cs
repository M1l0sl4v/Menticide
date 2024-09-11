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
    public float restart;

    public static TileQueue tileQueue;

    private void Start()
    {
        instance = this;
    }

    //moves the tiles forward whenever the tiles hit the culling field.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            Vector3 newPosition = transform.position;
            newPosition.y += tileresetdistance;  
            transform.position = newPosition;


            // Add culled tiles to the TileQueue
            //tileQueue.EnqueueTile(collision.gameObject

        }
    }
    
}
