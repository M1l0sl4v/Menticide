using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class hands : MonoBehaviour
{
    public static hands instance;
    public float speed;
    private Vector3 endPos;
    public float moveSmooth;
    private bool active;
    private float initDistanceFromPlayer;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        initDistanceFromPlayer = Vector2.Distance(playermovement.instance.transform.position, transform.position);
        if (!Tutorial.startWithTutorial) active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            endPos = playermovement.instance.transform.position;
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime*moveSmooth);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playermovement.instance.TakeDamage(1);
        }
    }

    public void Activate()
    {
        Vector3 playerPos = playermovement.instance.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y - initDistanceFromPlayer, playerPos.z);
        active = true;
    }
}