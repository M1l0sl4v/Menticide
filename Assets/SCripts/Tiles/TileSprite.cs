using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprite : MonoBehaviour
{

    public Sprite sprite;

    public enum Direction
    {
        Left,
        Middle,
        Right
    }
    public Direction direction;

    public enum Layer
    {
        Base,
        Overylay
    }
    public Layer layer;

    public enum Season
    {
        None,
        Summer,
        Fall,
        Winter,
        Spring
    }
    public Season season;

    public enum Material
    {
        None,
        Brick,
        Paved
    }
    public Material material;

    public TileSprite(Sprite sp, Direction d, Layer l, Season s)
    {
        this.sprite = sp;
        this.direction = d;
        this.layer = l;
        this.season = s;
    }

    public TileSprite(Sprite sp, Direction d, Layer l, Material m)
    {
        this.sprite = sp;
        this.direction = d;
        this.layer = l;
        this.material = m;
    }
}
