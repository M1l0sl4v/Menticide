using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TileSprite;

public class TileObject : MonoBehaviour
{
    //public enum Direction
    //{
    //    Left,
    //    Middle,
    //    Right
    //}
    public Direction direction;

    //public enum Layer
    //{
    //    Base,
    //    Overlay
    //}
    public Layer layer;


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

    public void SetSprite(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    
}
