using System;
using System.Collections;
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
            float pitch = Random.Range(.7f, 1.3f);
            AudioManager.instance.environmentFX(ruscleSound, transform ,.6f, pitch);
            StartCoroutine(DestroyLeavesCoroutine(delay));
        }

        if (other.CompareTag("cullingField"))
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            treespawner1.Spawn();
            treespawner2.Spawn();
        }
        
    }

    private IEnumerator DestroyLeavesCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (leafsinstance != null)
        {
            Destroy(leafsinstance.gameObject);  
        }
    }


    private void leafspawn()
    {
       
        leafsinstance = Instantiate(leafs, transform.position , Quaternion.identity, transform);
    }
    
}
