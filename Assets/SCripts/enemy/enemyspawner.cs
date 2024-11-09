using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class enemyspawner : MonoBehaviour
{

    public float spawnRadius;
    public bool showGizmo;
    
    public float timeBetweenSpawnAttempt;
    public float spawnChance;
    public float spawnScaling;
    [HideInInspector]public float rareenemychance; // currently unused
    public List<GameObject> enemytype1;
    [HideInInspector]public List<GameObject> enemytype2;
    public enum ScalingType
    {
        Constant,
        Seasonal
    }
    public ScalingType scalingType;
    private float elapsedTime;

 
    private void Start()
    {

    }

    
    void Update()
    {
        if (!Tutorial.instance.disabledForTutorial)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= timeBetweenSpawnAttempt)
            {
                if (Random.value < spawnChance)
                {
                    Spawn();
                }
                elapsedTime = 0;
            }
        }
        // For debugging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        float randomAngle = Random.Range(-90f, 90f);
        Vector3 direction = Quaternion.Euler(0, randomAngle, 0) * transform.forward;
        Vector3 spawnPossition = transform.position + direction * spawnRadius;
        
        
        float spawnChance = Random.value; // a stupid way of making rare enemies spawn every now and then
        if (false && spawnChance > rareenemychance) // this is the rare enemy
        {
            int prefabIndex = Random.Range(0, enemytype2.Count);//rare enemy
            ObjectPoolManager.SpawnObject(enemytype2[prefabIndex], spawnPossition, ObjectPoolManager.PoolType.Enemytype2);
            
        }
        else //this is the common enemy
        {
           // Debug.Log("ENEMY SHOULD BE SPAWNING");
            int prefabIndex = Random.Range(0, enemytype1.Count);
            ObjectPoolManager.SpawnObject(enemytype1[prefabIndex], spawnPossition, ObjectPoolManager.PoolType.Enemytype1);
        }
        
        //i wanted a spawn percentage that could scale easily. this just rolls between 0 and 1, and if it is above .9 it spawns a rare, if not it spawns a common. this allows us to just add a new variable 
        //with a new spawn type or enemy type. we will have to fiddle with the percentages, but this should be a pretty decent system, i hope. 
    }
   
    public void IncreaseChance()
    {
            spawnChance += spawnScaling;
    }
    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
            Vector3 rightDirection = Quaternion.Euler(0, 90, 0) * transform.forward * spawnRadius;
            Vector3 leftDirection = Quaternion.Euler(0, -90, 0) * transform.forward * spawnRadius;
            Gizmos.DrawLine(transform.position, transform.position + rightDirection);
            Gizmos.DrawLine(transform.position, transform.position + leftDirection);
        }
    }
    
  
}
