using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class hands : MonoBehaviour
{
    public float speed;
    private Vector2 direction = Vector2.up; // Current movement direction

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.transform.Translate( direction* speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playermovement.instance.TakeDamage(1);
            Debug.Log("Player Hit");
        }
    }
}
