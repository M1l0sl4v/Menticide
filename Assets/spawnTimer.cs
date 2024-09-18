using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTimer : MonoBehaviour
{
    private float timeBetweenSpawnTree;
    private float spawnTimeTree;
    public float spawnminTree;
    public float spawnmaxTree;
    public treeSpawner treeSpawner;

    private float timeBetweenSpawn;
    private float spawnTimeObstacle;
    public float spawnminObstacle;
    public float spawnmaxObstacle;
    public obstaclespawner obstaclespawner;

    // Start is called before the first frame update
    void Start()
    {
        treeSpawner.Spawn();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time > spawnTimeTree)
        {
            timeBetweenSpawnTree = Random.Range(spawnminTree, spawnmaxTree);
            treeSpawner.Spawn();
            spawnTimeTree = Time.time + timeBetweenSpawn;
        }
        if (Time.time > spawnTimeObstacle)
        {
            timeBetweenSpawn = Random.Range(spawnminObstacle, spawnmaxObstacle);
            obstaclespawner.Spawn();
            spawnTimeObstacle = Time.time + timeBetweenSpawn;
            // Debug.Log(timeBetweenSpawn);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
