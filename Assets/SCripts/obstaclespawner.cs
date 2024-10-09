using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class obstaclespawner : MonoBehaviour
{
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    private float timeBetweenSpawn;
    private float spawnTime;
    public float spawnmin;
    public float spawnmax;
    
    
    public float rareenemychance;
 
    public List<GameObject> enemytype1;
    public List<GameObject> enemytype2;

    private void Start()
    {
        timeBetweenSpawn = Random.Range(spawnmin, spawnmax);
        spawnTime = Time.time + timeBetweenSpawn;
    }

    void FixedUpdate()
    {

        // delay on start, then incriments
        if(Time.time > spawnTime)
        {
            timeBetweenSpawn = Random.Range(spawnmin, spawnmax);
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn; 
        }
      
    }
    public void Spawn()
    {
        //location
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        float spawnChance = Random.value;
        
        if (spawnChance > rareenemychance)
        {
            int prefabIndex = Random.Range(0, enemytype2.Count);
            //calling the pool
            ObjectPoolManager.SpawnObject(enemytype2[prefabIndex], transform.position + new Vector3(randomY, randomX), ObjectPoolManager.PoolType.Enemytype2);
            
        }
        else 
        {
            int prefabIndex = Random.Range(0, enemytype1.Count);
            //calling the pool
            ObjectPoolManager.SpawnObject(enemytype1[prefabIndex], transform.position + new Vector3(randomY, randomX), ObjectPoolManager.PoolType.Enemytype1);
        }
        
        //i wanted a spawn percentage that could scale easily. this just rolls between 0 and 1, and if it is above .9 it spawns a rare, if not it spawns a common. this allows us to just add a new variable 
        //with a new spawn type or enemy type. we will have to fiddle with the percentages, but this should be a pretty decent system, i hope. 
    }
}
