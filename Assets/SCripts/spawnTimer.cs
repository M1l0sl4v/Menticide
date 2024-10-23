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

    private float timeToNextSpawn;
    private float spawnerChoose;
    public float mintimebetweenenemyspawn;
    public float maxtimebetweenenemyspawn;
    public enemyspawner Enemyspawner;
    public enemyspawner Enemyspawner2;

    // Start is called before the first frame update
    void Start()
    {
        treeSpawner.Spawn();
        Enemyspawner.Spawn();
        Enemyspawner2.Spawn();
        timeToNextSpawn = Random.Range(mintimebetweenenemyspawn, maxtimebetweenenemyspawn);

    }

    void Update()
    {
        if (timeToNextSpawn > 0)
        {
            timeToNextSpawn -= Time.deltaTime;
        }

        // Once the countdown reaches 0, spawn an enemy
        if (timeToNextSpawn <= 0)
        {
            //picks between the two spawners
            spawnerChoose = Random.value;
            if (spawnerChoose > 0.5f)
            {
                Enemyspawner.Spawn();
            }
            else
            {
                Enemyspawner2.Spawn();
            }

            // Reset the timer with a new random interval
            timeToNextSpawn = Random.Range(mintimebetweenenemyspawn, maxtimebetweenenemyspawn);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time > spawnTimeTree)
        {
            timeBetweenSpawnTree = Random.Range(spawnminTree, spawnmaxTree);
            treeSpawner.Spawn();
            spawnTimeTree = Time.time + timeBetweenSpawn;
        }
       
    }
    
    
    /*if (spawnerChoose > .5f)
                    {
                         Enemyspawner.Spawn();
                    }
                    else
                    {
                        Enemyspawner2.Spawn();
                    }*/
}
