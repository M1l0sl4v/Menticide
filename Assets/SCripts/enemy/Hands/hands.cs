using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class hands : MonoBehaviour
{
    public float speed;
    private Vector3 endPos;
    public float moveSmooth;
    private Transform playerTransform;
    private GameObject player;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        endPos = playerTransform.position;
        transform.position = Vector3.Lerp(transform.position, endPos,Time.deltaTime*moveSmooth);
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