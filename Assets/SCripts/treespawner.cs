using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class treeSpawner : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    private float timeBetweenSpawn;
    private float spawnTime;
    public float spawnmin;
    public float spawnmax;
    public List<GameObject> Tree;
    public float treeChance;
    public Tilemap tilemap;
    
    void Update()
    {

        //delay on start, then incriments
        if(Time.time > spawnTime)
        {
            timeBetweenSpawn = Random.Range(spawnmin, spawnmax);
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn; 
        }
      
    }

    void Spawn()
    {
        //location
        Vector3Int randomCellPossition = new Vector3Int(Random.Range((int)xMin, (int)xMax), (Random.Range((int)yMin,(int)yMax)),0);

        Vector3 spawnposition = tilemap.GetCellCenterWorld(randomCellPossition);
        
        float spawnChance = Random.value;
        if (spawnChance > treeChance) 
        {
            int prefabIndex = Random.Range(0, Tree.Count);
            //calling the pool
            ObjectPoolManager.SpawnObject(Tree[prefabIndex], spawnposition, ObjectPoolManager.PoolType.Tree);

            Debug.Log("rare enemy spawned, number was " + spawnChance);
        }
        else
        {
            Debug.Log("no tree");
        }
    }
}
