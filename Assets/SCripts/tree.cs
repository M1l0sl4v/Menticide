using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    public float despawnDistanceTree = 10;
    public static tree instance;
    [SerializeField] private ParticleSystem leafs;
    [SerializeField] private AudioClip ruscleSound;
    
    private ParticleSystem leafsinstance;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        //checks distance to player and despawns when it gets too far away.
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 currentPos = transform.position;
        float dist = Vector3.Distance(currentPos, playerPos);
        if (dist > despawnDistanceTree)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("wow");
        if (other.CompareTag("Player"))
        {
            leafspawn();
            AudioManager.instance.environmentFX(ruscleSound, transform ,1f);
            Debug.Log("leafspawn");
        }

        if (other.CompareTag("cullingField"))
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        
    }


    private void leafspawn()
    {
       
        leafsinstance = Instantiate(leafs, transform.position , Quaternion.identity, transform);
    }
    
}
