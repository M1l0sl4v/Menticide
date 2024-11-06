using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{
    public float borderresetdistance;
   
  
    

    //moves the borders forward whenever the tiles hit the culling field.
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            Vector3 newPosition = transform.position;
            newPosition.y += borderresetdistance;  
            transform.position = newPosition;
        }
    }
}
