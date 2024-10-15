using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSprite
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
        Overlay
    }
    public Layer layer;

    public enum Season
    {
        None = 0,
        Summer,
        Fall,
        Winter,
        Spring
    }
    public Season season;

    public enum PathMaterial
    {
        None,
        Brick,
        Paved,
        Dirt,
        Cobble
    }
    public PathMaterial material;

    public TileSprite(Sprite sp, Direction d, Layer l, Season s)
    {
        this.sprite = sp;
        this.direction = d;
        this.layer = l;
        this.season = s;
    }

    public TileSprite(Sprite sp, Direction d, Layer l, PathMaterial m)
    {
        this.sprite = sp;
        this.direction = d;
        this.layer = l;
        this.material = m;
    }

    public override string ToString()
    {
        if (layer == Layer.Base) return direction.ToString() + layer.ToString() + material.ToString();
        if (layer == Layer.Overlay) return direction.ToString() + layer.ToString() + season.ToString();
        throw new System.Exception("Unset layer not allowed"); // this line should never be reached
    }
}
