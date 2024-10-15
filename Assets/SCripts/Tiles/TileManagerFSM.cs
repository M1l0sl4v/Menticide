using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using static TileSprite;

public class TileManagerFSM : MonoBehaviour
{

    // barely even a FSM at this point...

    private enum SeasonState
    {
        Summer,
        EarlyFall,
        Fall,
        EarlyWinter,
        Winter,
        EarlySpring,
        Spring,
        EarlySummer
    }
    private SeasonState seasonState;

    public Season season;

    //public enum PathMaterial
    //{
    //    Brick,
    //    Paved,
    //    Cobble,
    //    Dirt
    //}
    public PathMaterial pathMaterial;



    public static float tileResetDistance = 18;

    // Overlay
    [Header("Summer Sprites")]
    public Sprite[] smrLSprites;
    public Sprite[] smrMSprites;
    public Sprite[] smrRSprites;
    [Header("Fall Sprites")]
    public Sprite[] fallLSprites;
    public Sprite[] fallMSprites;
    public Sprite[] fallRSprites;
    [Header("Winter Sprites")]
    public Sprite[] winLSprites;
    public Sprite[] winMSprites;
    public Sprite[] winRSprites;
    [Header("Spring Sprites")]
    public Sprite[] sprLSprites;
    public Sprite[] sprMSprites;
    public Sprite[] sprRSprites;


    // Base
    [Header("Brick Sprites")]
    public Sprite[] brickFadeoutSprites;
    public Sprite[] brickFadeinSprites;
    public Sprite[] brickLSprites;
    public Sprite[] brickMSprites;
    public Sprite[] brickRSprites;
    [Header("Paved Sprites")]
    public Sprite[] pavedFadeoutSprites;
    public Sprite[] pavedFadeinSprites;
    public Sprite[] pavedLSprites;
    public Sprite[] pavedMSprites;
    public Sprite[] pavedRSprites;
    [Header("Dirt Sprites")]
    public Sprite[] dirtFadeoutSprites;
    public Sprite[] dirtFadeinSprites;
    public Sprite[] dirtLSprites;
    public Sprite[] dirtMSprites;
    public Sprite[] dirtRSprites;
    [Header("Cobble Sprites")]
    public Sprite[] cobbleFadeoutSprites;
    public Sprite[] cobbleFadeinSprites;
    public Sprite[] cobbleLSprites;
    public Sprite[] cobbleMSprites;
    public Sprite[] cobbleRSprites;


    private int emptyChance;
    private int nextSeasonChance;
    private float seasonChangeSpeed;

    private TileSprite[] masterList;

    public static TileManagerFSM instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // EXPERIMENTAL
        // Compile big list
        masterList = ConcatArrays(

            // OVERLAY

            // Summer
            AssignTags(smrLSprites, Direction.Left, Layer.Overlay, Season.Summer),
            AssignTags(smrMSprites, Direction.Middle, Layer.Overlay, Season.Summer),
            AssignTags(smrRSprites, Direction.Right, Layer.Overlay, Season.Summer),

            // Fall
            AssignTags(fallLSprites, Direction.Left, Layer.Overlay, Season.Fall),
            AssignTags(fallMSprites, Direction.Middle, Layer.Overlay, Season.Fall),
            AssignTags(fallRSprites, Direction.Right, Layer.Overlay, Season.Fall),

            // Spring
            AssignTags(winLSprites, Direction.Left, Layer.Overlay, Season.Winter),
            AssignTags(winMSprites, Direction.Middle, Layer.Overlay, Season.Winter),
            AssignTags(winMSprites, Direction.Right, Layer.Overlay, Season.Winter),

            // Winter
            AssignTags(sprLSprites, Direction.Left, Layer.Overlay, Season.Spring),
            AssignTags(sprMSprites, Direction.Middle, Layer.Overlay, Season.Spring),
            AssignTags(sprRSprites, Direction.Right, Layer.Overlay, Season.Spring),


            // BASE

            // Brick
            AssignTags(brickLSprites, Direction.Left, Layer.Base, PathMaterial.Brick),
            AssignTags(brickMSprites, Direction.Middle, Layer.Base, PathMaterial.Brick),
            AssignTags(brickRSprites, Direction.Right, Layer.Base, PathMaterial.Brick),

            // Paved
            AssignTags(pavedLSprites, Direction.Left, Layer.Base, PathMaterial.Paved),
            AssignTags(pavedMSprites, Direction.Middle, Layer.Base, PathMaterial.Paved),
            AssignTags(pavedRSprites, Direction.Right, Layer.Base, PathMaterial.Paved),

            // Dirt
            AssignTags(dirtLSprites, Direction.Left, Layer.Base, PathMaterial.Dirt),
            AssignTags(dirtMSprites, Direction.Middle, Layer.Base, PathMaterial.Dirt),
            AssignTags(dirtRSprites, Direction.Right, Layer.Base, PathMaterial.Dirt),

            // Cobble
            AssignTags(cobbleLSprites, Direction.Left, Layer.Base, PathMaterial.Cobble),
            AssignTags(cobbleMSprites, Direction.Middle, Layer.Base, PathMaterial.Cobble),
            AssignTags(cobbleRSprites, Direction.Right, Layer.Base, PathMaterial.Cobble)
            );

        //DeathSequence.LogCollection<TileSprite>(masterList);

            
    }

    // Update is called once per frame
    void Update()
    {

    }

    private TileSprite[] AssignTags(Sprite[] sprites, Direction dir, Layer layer, Season season)
    {
        TileSprite[] output = new TileSprite[sprites.Length];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = new TileSprite(sprites[i], dir, layer, season);
        }

        return output;
    }

    private TileSprite[] AssignTags(Sprite[] sprites, Direction dir, Layer layer, PathMaterial material)
    {
        TileSprite[] output = new TileSprite[sprites.Length];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = new TileSprite(sprites[i], dir, layer, material);
        }

        return output;
    }

    private TileSprite[] ConcatArrays(params TileSprite[][] p)
    {
        var position = 0;
        var outputArray = new TileSprite[p.Sum(a => a.Length)];
        foreach (var curr in p)
        {
            Array.Copy(curr, 0, outputArray, position, curr.Length);
            position += curr.Length;
        }
        return outputArray;
    }



    public void ProcessTile(TileObject tile)
    {
        Direction direction = tile.direction;
        Layer layer = tile.layer;
        switch (layer)
        {
            case Layer.Overlay:
                Season season = this.season;
                break;
            case Layer.Base:
                PathMaterial pathMaterial = this.pathMaterial;
                break;
        }

        List<Sprite> spriteChoices = new List<Sprite>();

        foreach (TileSprite sprite in masterList)
        {
            if (sprite.layer != layer) continue;
            if (sprite.direction != direction) continue;
            if (layer == Layer.Overlay && sprite.season != season) continue;
            if (layer == Layer.Base && sprite.material != pathMaterial) continue;
            spriteChoices.Add(sprite.sprite);
        }

        tile.GetComponent<SpriteRenderer>().sprite = spriteChoices[UnityEngine.Random.Range(0, spriteChoices.Count)];




        // 5% chance to spawn rubble as well






        // set tile sprite to random one from [x] deck

        //foreach (TileSprite sprite in )


        // start with mega-deck of all possible sprites
        //    - sprites have "tags" or some way of indentifying
        // 




    }
}
