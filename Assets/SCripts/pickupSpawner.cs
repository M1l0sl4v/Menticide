using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class pickupSpawner : MonoBehaviour
{
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    private float timeBetweenSpawn;
    private float spawnTime;
    public float spawnmin;
    public float spawnmax;
    public List<GameObject> pickUps;
    public float pickupChance;
    public Tilemap tilemap;
    void Update()
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
        Vector3Int randomCellPossition = new Vector3Int(Random.Range((int)xMin, (int)xMax), (Random.Range((int)yMin,(int)yMax)),0);

        Vector3 spawnposition = tilemap.GetCellCenterWorld(randomCellPossition);


        float spawnChance = Random.value;
        if (spawnChance > pickupChance) 
        {
            Instantiate(pickUps[Random.Range(0, pickUps.Count)], spawnposition, Quaternion.identity);
        }
        else
        {
            Debug.Log("no tree");
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.17f, 1f, 0.28f, 0.53f); 
        Vector3 bottomLeft = new Vector3(xMin, yMin, 0);
        Vector3 topRight = new Vector3(xMax, yMax, 0);
        Gizmos.DrawCube((bottomLeft + topRight) / 2, topRight - bottomLeft); 
    }
}
