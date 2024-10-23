using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
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
    public float mintimebetweenenemyspawn;
    public float maxtimebetweenenemyspawn;
    private float garunteedMaxIntervul = 1;
    public enemyspawner Enemyspawner;
    public enemyspawner Enemyspawner2;

    // Start is called before the first frame update
    void Start()
    {
        treeSpawner.Spawn();
        Enemyspawner.Spawn();
        Enemyspawner2.Spawn();
        
        float initialDelay = Random.Range(mintimebetweenenemyspawn, maxtimebetweenenemyspawn);
        
        InvokeRepeating("chooseSpawner", initialDelay, Random.Range(mintimebetweenenemyspawn, maxtimebetweenenemyspawn));
        InvokeRepeating("garunteedSpawn",garunteedMaxIntervul, garunteedMaxIntervul);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.time > spawnTimeTree)
        {
            timeBetweenSpawnTree = Random.Range(spawnminTree, spawnmaxTree);
            treeSpawner.Spawn();
            spawnTimeTree = Time.time + timeBetweenSpawnTree;
        }
    }

    public void chooseSpawner()
    {
        Debug.Log("choose spawner");
        spawnerChoose = Random.value;
        if (spawnerChoose > .5f)
        {
             Enemyspawner.Spawn();
        }
        else
        {
            Enemyspawner2.Spawn();
        }
        
        CancelInvoke("chooseSpawner");
        InvokeRepeating("chooseSpawner", Random.Range(mintimebetweenenemyspawn, maxtimebetweenenemyspawn), 
            Random.Range(mintimebetweenenemyspawn, maxtimebetweenenemyspawn));
    }

    public void garunteedSpawn()
    {
        chooseSpawner();
    }

    float GetRandomIntervul(float min, float max)
    {
        float randomValue = Mathf.Pow(Random.value, 2f);
        return Mathf.Lerp(min, max, randomValue);
    }
    
}
