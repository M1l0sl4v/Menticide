using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    static int pathLayer = 3;

    private TileQueue tileQueue;

    // Debug
    public bool logDebugMessages = false;

    // Start is called before the first frame update
    void Start()
    {
        tileQueue = FindAnyObjectByType<TileQueue>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // The moment the tile spawner leaves contact with a tile (object in the "Path" layer),
    // pull a tile from the TileQueue and spawn it in place
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == pathLayer)
        {
            Instantiate(tileQueue.DequeueTile());

            if (logDebugMessages) Debug.Log("Tile spawned");
        }
    }
}
