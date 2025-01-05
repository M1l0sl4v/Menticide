using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spikeScript : MonoBehaviour
{
    public bool canGrow = true;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playermovement.instance.TakeDamage(1);
            transform.localScale = new Vector3(.3f, 0, 1f);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            transform.localScale = new Vector3(.3f, 0, 1f);
        }
    }
}
