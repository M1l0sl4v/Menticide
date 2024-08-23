using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] treeObstaclePrefabs;
    public GameObject[] monsterObstaclePrefabs;
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;
    public float obstacleLifetime = 5f;
    public float treeScaleFactor = 6.5f;
    //public float monsterScaleFactor = 3f;


    void Start()
    {
        // Start spawning obstacles with a random initial delay
        float initialDelay = Random.Range(0f, maxSpawnInterval);
        InvokeRepeating("SpawnObstacle", initialDelay, Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    void SpawnObstacle()
    {
        // Determine whether to spawn a tree or a monster
        GameObject randomObstaclePrefab;
        bool treeFlag = false;

        if (Random.Range(0, 10) == 0) // 1/10 chance (0-based index)
        {
            // If the random number is 0, pick from the monster pool
            randomObstaclePrefab = monsterObstaclePrefabs[Random.Range(0, monsterObstaclePrefabs.Length)];

        }
        else
        {
            // Otherwise, pick from the tree pool
            randomObstaclePrefab = treeObstaclePrefabs[Random.Range(0, treeObstaclePrefabs.Length)];
            treeFlag = true;
        }

        // Instantiate the selected obstacle at the spawner's position
        GameObject obstacle = Instantiate(randomObstaclePrefab, transform.position, Quaternion.identity);

        // Set the scale of the obstacle to be larger
        // Adjust the scale factor as needed
        if (treeFlag)
        {
            obstacle.transform.localScale = new Vector3(treeScaleFactor, treeScaleFactor, 1f);
        }
        //else
        //{
        //    obstacle.transform.localScale = new Vector3(monsterScaleFactor, monsterScaleFactor, 1f);
        //}

        // Destroy the obstacle after its lifetime
        Destroy(obstacle, obstacleLifetime);

        // Set a new random spawn interval
        CancelInvoke("SpawnObstacle");
        InvokeRepeating("SpawnObstacle", Random.Range(minSpawnInterval, maxSpawnInterval), Random.Range(minSpawnInterval, maxSpawnInterval));
    }
}
