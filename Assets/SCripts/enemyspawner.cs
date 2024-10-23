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
   
    
    
    public float rareenemychance;
 
    public List<GameObject> enemytype1;
    public List<GameObject> enemytype2;
    
    public void Spawn()
    {
        float randomAngle = Random.Range(-90f, 90f);
        Vector3 direction = Quaternion.Euler(0, randomAngle, 0) * transform.forward;
        Vector3 spawnPossition = transform.position + direction * spawnRadius;
        
        
        float spawnChance = Random.value; // a stupid way of making rare enemies spawn every now and then
        if (spawnChance > rareenemychance)
        {
            int prefabIndex = Random.Range(0, enemytype2.Count);
            //calling the pool
            ObjectPoolManager.SpawnObject(enemytype2[prefabIndex], spawnPossition, ObjectPoolManager.PoolType.Enemytype2);
            
        }
        else 
        {
            int prefabIndex = Random.Range(0, enemytype1.Count);
            //calling the pool
            ObjectPoolManager.SpawnObject(enemytype1[prefabIndex], spawnPossition, ObjectPoolManager.PoolType.Enemytype1);
        }
        
        //i wanted a spawn percentage that could scale easily. this just rolls between 0 and 1, and if it is above .9 it spawns a rare, if not it spawns a common. this allows us to just add a new variable 
        //with a new spawn type or enemy type. we will have to fiddle with the percentages, but this should be a pretty decent system, i hope. 
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
