using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using static TileSprite;
using UnityEngine.U2D;

public class TileManagerFSM : MonoBehaviour
{

    // not even a FSM at this point...


    public Season season;
    public PathMaterial pathMaterial;




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

    private TileSprite[] masterList;
    private Dictionary<string, List<Sprite>> tileCache = new();


    public static float tileResetDistance = 18;
    public static TileManagerFSM instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        SetEnums();

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
            AssignTags(winRSprites, Direction.Right, Layer.Overlay, Season.Winter),

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

        foreach (TileSprite sprite in masterList)
        {
            if (!tileCache.ContainsKey(sprite.ToString())) tileCache.Add(sprite.ToString(), new List<Sprite>());
            tileCache[sprite.ToString()].Add(sprite.sprite);
        }
    }

    void Update()
    {

    }

    // Called by each tile on itself as it hits the culling field
    public void ProcessTile(TileObject tile)
    {
        // Check for season transition
        Season seasonToUse;
        if (Score.instance.DistanceInSeason() >= seasons.transitionAfter && UnityEngine.Random.value < NextSeasonChance()) seasonToUse = PeekNextSeason();
        else seasonToUse = season;

        // Check for material to use
        PathMaterial materialToUse;
        materialToUse = pathMaterial;


        // List to contain options
        List<Sprite> spriteChoices = new();

        if (StaticDebugTools.instance.tileManagerAlgorithm == StaticDebugTools.Algorithm.Cache)
        {
            if (tile.layer == Layer.Overlay) spriteChoices = tileCache[CacheKey(tile.direction, tile.layer, seasonToUse)];
            if (tile.layer == Layer.Base) spriteChoices = tileCache[CacheKey(tile.direction, tile.layer, materialToUse)];
        }
        


        if (StaticDebugTools.instance.tileManagerAlgorithm == StaticDebugTools.Algorithm.Picker)
        {
            // Check for no overlay in summer, else populate spriteChoices
            if (tile.layer == Layer.Overlay && season == Season.Summer)
            {
                tile.SetSprite(null);
                return;
            }
            else
            {
                foreach (TileSprite sprite in masterList)
                {
                    if (sprite.layer != tile.layer) continue;
                    if (sprite.direction != tile.direction) continue;
                    if (tile.layer == Layer.Overlay && sprite.season != seasonToUse) continue;
                    if (tile.layer == Layer.Base && sprite.material != materialToUse) continue;
                    spriteChoices.Add(sprite.sprite);
                }
            }
        }

        // Check for empty list, else fetch random sprite from spriteChoices
        if (spriteChoices.Count == 0) tile.SetSprite(null);
        else tile.SetSprite(spriteChoices[UnityEngine.Random.Range(0, spriteChoices.Count)]);

    }

    // Returns the chance [0-1] that an overlay tile should have the sprite for the next season
    private float NextSeasonChance()
    {
        float transitionDuration = seasons.seasonLength - seasons.transitionAfter;
        int distanceInTransition = Score.instance.DistanceInSeason() - seasons.transitionAfter;
        return Mathf.Clamp(distanceInTransition / transitionDuration, 0f, 1f);
    }

    // Returns what the next season will be
    private Season PeekNextSeason()
    {
        if (season == Season.Spring) return Season.Summer;
        else return season + 1;
    }

    // Returns the string key associated with a specific combination of traits
    private string CacheKey(Direction direction, Layer layer, Season season)
    {
        return "" + direction + layer + season;
    }
    private string CacheKey(Direction direction, Layer layer, PathMaterial material)
    {
        return "" + direction + layer + material;
    }

    // Clears the tile cache
    public void ClearTileCache()
    {
        tileCache.Clear();
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





    public void SetEnums()
    {
        Transform tilemap = GameObject.Find("Tilemap").transform;

        foreach (Transform row in tilemap)
        {
            Transform baseTiles = row.Find("Base");
            Transform overlayTiles = row.Find("Overlay");

            foreach (TileObject tile in baseTiles.GetComponentsInChildren<TileObject>())
            {
                tile.layer = Layer.Base;
                if (tile.name == "Base 0") tile.direction = Direction.Left;
                else if (tile.name == "Base 1" || tile.name == "Base 2") tile.direction = Direction.Middle;
                else if (tile.name == "Base 3") tile.direction = Direction.Right;
            }

            foreach (TileObject tile in overlayTiles.GetComponentsInChildren<TileObject>())
            {
                tile.layer = Layer.Overlay;
                if (tile.name == "Overlay 0") tile.direction = Direction.Left;
                else if (tile.name == "Overlay 1" || tile.name == "Overlay 2") tile.direction = Direction.Middle;
                else if (tile.name == "Overlay 3") tile.direction = Direction.Right;
            }
        }
    }
}
