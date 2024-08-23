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
    void Update()
    {
        //transform.Translate(new Vector2(0, -scrollspeed) * Time.deltaTime);
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
}
