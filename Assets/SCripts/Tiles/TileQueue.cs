using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileQueue : MonoBehaviour
{

    private Queue<TileObject> tileQueue = new Queue<TileObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnqueueTile(TileObject tile)
    {
        tileQueue.Enqueue(tile);
    }

    public void EnqueueTiles(List<TileObject> tiles)
    {
        foreach (TileObject tile in tiles) tileQueue.Enqueue(tile);
    }

    public TileObject DequeueTile() 
    {
        return tileQueue.Dequeue();
    }

    public List<TileObject> DequeueTiles(int n)
    {
        List<TileObject> tiles = new List<TileObject>();
        for (int i = 0; i < n; i++)
        {
            tiles.Add(DequeueTile());
        }
        return tiles;
    }

    public int Count()
    {
        return tileQueue.Count;
    }
}
