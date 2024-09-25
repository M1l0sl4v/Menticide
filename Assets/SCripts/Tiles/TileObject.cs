using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public enum Type
    {
        Left,
        Middle,
        Right
    }
    public Type type;

    public Sprite sprite;

    private void Start()
    {
        sprite = GetComponent<Sprite>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            TileManagerFSM.instance.ProcessTile(this);
        }
    }
}
