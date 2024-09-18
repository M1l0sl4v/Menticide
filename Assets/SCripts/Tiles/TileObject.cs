using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class TileObject : MonoBehaviour
{

    // Example traits, not functional or final
    private Sprite _sprite;
    private float _speedMultiplier;
    static int pathLayer = 3;
    public GameObject tile;

    public enum TileAlignment
    {
        LeftEdge,
        LeftCenter,
        RightCenter,
        RightEdge
    }

    public enum TileType
    {
        Dirt,
        Solid,
        Brick
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TileObject(Sprite sprite, float speedMultiplier)
    {
        _sprite = sprite;
        _speedMultiplier = speedMultiplier;
    }

    //public GameObject Instantiate()
    //{
    //    GameObject tile = new GameObject("Tile");

    //    SpriteRenderer spriteRenderer = tile.AddComponent<SpriteRenderer>();
    //    spriteRenderer.sprite = _sprite;

    //    BoxCollider2D boxCollider = tile.AddComponent<BoxCollider2D>();

    //    tile.layer = pathLayer;

    //    TileObject script = tile.AddComponent<TileObject>();


    //    return tile;
    //}
}
