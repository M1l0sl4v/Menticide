using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public enum SpriteType
    {
        Left,
        Middle,
        Right
    }
    public SpriteType spriteType;

    public enum TileLayer
    {
        Base,
        Overlay
    }
    public TileLayer tileLayer;


    private void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            TileManagerFSM.instance.ProcessTile(this);
        }
    }
}
