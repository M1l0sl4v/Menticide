using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handSmashDelete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)   
    { 
        if (other.gameObject.CompareTag("cullingField"))   
        {
            Destroy(gameObject);  
        }
    }
}
