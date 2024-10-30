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
        //TileManagerFSM.instance.ProcessTile(this);
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

    public override string ToString()
    {
        return direction.ToString() + layer.ToString();
    }


}
