using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TreeSpawner : MonoBehaviour
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

    [Header("Seasons")]
    public GameObject summerTree;
    public GameObject fallTree;
    public GameObject winterTree;
    public GameObject springTree;

    private GameObject[] treePool;
    private int treePoolIndex;
    public static int treeSwitchPoint = 100;

    private void Start()
    {
        treePool = new GameObject[4] { summerTree, fallTree, winterTree, springTree };
    }

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
        if (spawnChance > treeChance) 
        {
            //calling the pool
            ObjectPoolManager.SpawnObject(treePool[treePoolIndex], spawnposition, ObjectPoolManager.PoolType.Tree);
        }
        else
        {
            Debug.Log("no tree");
        }
    }

    // Advances pool to next season, called from ScoreManager/IncreaseScore
    public void AdvanceTreePool()
    {
        if (++treePoolIndex == treePool.Length) treePoolIndex = 0;
        TreeObject.layerToUse = 0;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f); 
        Vector3 bottomLeft = new Vector3(xMin, yMin, 0);
        Vector3 topRight = new Vector3(xMax, yMax, 0);
        Gizmos.DrawCube((bottomLeft + topRight) / 2, topRight - bottomLeft); 
    }
}
