using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class tree : MonoBehaviour
{
    public float despawnDistanceTree = 10;
    private ParticleSystem leafs;
    [SerializeField] private AudioClip ruscleSound;
    public float delay = 1f;
    public bool hasLeaves = true;
    public GameObject treeSmash;
    public AudioClip treeBreakSound;

    private float randomPitch;
    
    private Coroutine destroyLeavesCoroutine;
    
    void OnEnable()
    {
        if (leafs == null)
        {
            leafs = GetComponentInChildren<ParticleSystem>();  // Find the child ParticleSystem
        }
        
        if (leafs != null)
        {
            leafs.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
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
            if (hasLeaves)
            {
                leafspawn();
                randomPitch = Random.Range(.9f, 1.3f);
            }

            else
            {
                randomPitch = Random.Range(.5f, .65f);
            }
           
            
            AudioManager.instance.environmentFX(ruscleSound, transform ,.5f, randomPitch);
            StartCoroutine(DestroyLeavesCoroutine(delay));
            
            if (destroyLeavesCoroutine != null)
            {
                StopCoroutine(destroyLeavesCoroutine);
            }
        }

        if (other.CompareTag("handSlams"))
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            Instantiate(treeSmash, transform.position, Quaternion.identity);
            randomPitch = Random.Range(.9f, 1.3f);
            AudioManager.instance.environmentFX(ruscleSound, transform ,.5f, randomPitch);
        }

        if (other.CompareTag("cullingField"))
        { 
            ClearParticles();
           ObjectPoolManager.ReturnObjectToPool(gameObject);
           if (leafs != null)
           {
               leafs.Clear();
           }
        }
    }
    private IEnumerator DestroyLeavesCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearParticles();
    }
    

    private void leafspawn()
    {
        if (leafs != null)
        {
            leafs.transform.position = transform.position;
            leafs.Play();
            
        }
        else
        {
            Debug.LogWarning("Leaf particle system is not assigned.");
        }
    }
    private void ClearParticles()
    {
        // Stop the particle system and clear it
        if (leafs != null)
        {
            leafs.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
    
}
