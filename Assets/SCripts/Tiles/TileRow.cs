using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileRow : MonoBehaviour
{
    private int count = 4;
    private GameObject[] tiles;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add empty tiles to the front of the row
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

        tiles = temp;
    }
}
