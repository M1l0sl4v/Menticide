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
    private float spawnerChoose;
    private float spawnTimeObstacle;
    public float spawnminObstacle;
    public float spawnmaxObstacle;
    public obstaclespawner obstaclespawner;
    public obstaclespawner obstaclespawner2;

    // Start is called before the first frame update
    void Start()
    {
        treeSpawner.Spawn();
        obstaclespawner.Spawn();
        obstaclespawner2.Spawn();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time > spawnTimeTree)
        {
            timeBetweenSpawnTree = Random.Range(spawnminTree, spawnmaxTree);
            treeSpawner.Spawn();
            spawnTimeTree = Time.time + timeBetweenSpawn;
        }
        /*if (Time.time > spawnTimeObstacle)
        {
            timeBetweenSpawn = Random.Range(spawnminObstacle, spawnmaxObstacle);
            //chose between the two spawners
            spawnerChoose = Random.value;
            if (spawnerChoose > .5f)
            {
                 obstaclespawner.Spawn();
            }
            else
            {
                obstaclespawner2.Spawn();
            }
            spawnTimeObstacle = Time.time + timeBetweenSpawn;
            // Debug.Log(timeBetweenSpawn);
        }*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
