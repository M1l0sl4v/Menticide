using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class tree : MonoBehaviour
{
    public float despawnDistanceTree = 10;
    [SerializeField] private ParticleSystem leafs;
    [SerializeField] private AudioClip ruscleSound;
    public float delay = 2f;
    
    public TreeSpawner treespawner1;
    public TreeSpawner treespawner2;

    public bool hasLeafs = true;
    
    
    private Coroutine destroyLeavesCoroutine;
    
    private ParticleSystem leafsinstance;

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
            float randomPitch = Random.Range(.9f, 1.3f);
            AudioManager.instance.environmentFX(ruscleSound, transform ,.8f, randomPitch);
            StartCoroutine(DestroyLeavesCoroutine(delay));
        }

        if (other.CompareTag("cullingField"))
        {
            ClearParticles();
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        
    }

    public IEnumerator DestroyLeavesCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (leafsinstance != null)
        {
            //Destroy(leafsinstance);  
            leafsinstance.Clear();
        }
    }

    public void ClearParticles()
    {
        if (leafsinstance != null)
        {
            //Destroy(leafsinstance);  
            leafsinstance.Clear();
        }
    }

    private void leafspawn()
    {
        if (hasLeafs)
        {
            leafsinstance = Instantiate(leafs, transform.position , Quaternion.identity, transform);

        }
        else
        {
            return;
        }
    }
    
}
