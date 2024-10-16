using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileRow : MonoBehaviour
{
    private int count = 4;
    public GameObject[] tiles;


    // Start is called before the first frame update
    void Start()
    {
        DetectTiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add n empty tiles to the front of the row
    public void AddTilesFront(int n)
    {
        GameObject[] temp = new GameObject[count + n];
        for (int i = 0; i < n; i++)
        {
            temp[i] = null;
        }
        for (int i = 0;i < count; i++)
        {
            temp[n+i] = tiles[i];
        }
        count += n;
        tiles = temp;
    }

    // Add n existing tiles to the front of the row
    public void AddTilesFront(int n, GameObject[] tilesToAdd)
    {
        GameObject[] temp = new GameObject[count + n];
        for (int i = 0; i < n; i++)
        {
            temp[i] = tilesToAdd[i];
        }
        for (int i = 0; i < count; i++)
        {
            temp[n + i] = tiles[i];
        }
        count += n;
        tiles = temp;
    }

    // Add n empty tiles to the back of the row
    public void AddTilesBack(int n)
    {
        GameObject[] temp = new GameObject[count + n];
        for (int i = 0; i < count; i++)
        {
            temp[i] = tiles[i];
        }
        for (int i = 0; i < n; i++)
        {
            temp[n+i] = null;
        }
        count += n;
        tiles = temp;
    }

    // Add n existing tiles to the back of the row
    public void AddTilesBack(int n, GameObject[] tilesToAdd)
    {
        GameObject[] temp = new GameObject[count + n];
        for (int i = 0; i < count; i++)
        {
            temp[i] = tiles[i];
        }
        for (int i = 0; i < n; i++)
        {
            temp[n + i] = tilesToAdd[i];
        }
        count += n;
        tiles = temp;
    }

    // Remove n tiles from the front of the row
    public void RemoveTilesFront(int n)
    {
        GameObject[] temp = new GameObject[count - n];

        for (int i = n; i < count; i++)
        {
            temp[i-n] = tiles[i];
        }
    }

    // Remove n tiles from the back of the row
    public void RemoveTilesBack(int n)
    {
        GameObject[] temp = new GameObject[count - n];

        for (int i = n; i < count; i++)
        {
            temp[i - n] = tiles[i];
        }
    }


    // Return tile GameObject at the specified index
    public GameObject GetTile(int index)
    {
        return tiles[index];
    }

    // Return full array of tiles in the row
    public GameObject[] GetTiles()
    {
        return tiles;
    }

    // Return a selected slice of the tile array
    public GameObject[] GetTiles(int frontBoundInclusive, int backBoundExclusive)
    {
        if (backBoundExclusive <= frontBoundInclusive || frontBoundInclusive < 0)
        {
            throw new ArgumentException("frontBoundInclusive must be greater than 0 and less than backBoundExclusive");
        }

        GameObject[] temp = new GameObject[backBoundExclusive - frontBoundInclusive];

        for (int i = frontBoundInclusive; i < backBoundExclusive; i++)
        {
            temp[i - frontBoundInclusive] = tiles[i];
        }

        return temp;
    }

    // Return the number of tiles in the row
    public int Count()
    {
        return count;
    }

    // Updates the internal array of tiles in the row
    public void DetectTiles()
    {
        GameObject[] temp = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            temp[i] = transform.GetChild(i).gameObject;
        }
        tiles = temp;
    }



    // On collision with culling field, increase score by 1 and move row up
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            if (!DeathSequence.controlLock) Score.instance.IncreaseScore();

            Vector3 newPosition = transform.position;
            newPosition.y += TileManagerFSM.tileResetDistance;
            transform.position = newPosition;
            if (DebugTools.instance.pauseAfterEveryRow) PauseMenu.instance.Pause();
        }
    }

}
