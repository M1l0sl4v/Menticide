using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class TileManagerFSM : MonoBehaviour
{
    public enum SeasonState
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
    public SeasonState seasonState;

    public enum PathMaterial
    {
        Brick,
        Paved,
        Cobble,
        Dirt
    }
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

    private Sprite[] combinedSprites;
    private TileSprite[] masterList;

    public static TileManagerFSM instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        ApplyEnums();

        // EXPERIMENTAL
        // Compile big list
        combinedSprites = ConcatArrays(
            smrLSprites,
            smrMSprites,
            smrRSprites,
            fallLSprites,
            fallMSprites,
            fallRSprites,
            winLSprites,
            winMSprites,
            winRSprites,
            sprLSprites,
            sprMSprites,
            sprRSprites,
            brickFadeoutSprites,
            brickFadeinSprites,
            brickLSprites,
            brickMSprites,
            brickRSprites,
            pavedFadeoutSprites,
            pavedFadeinSprites,
            pavedLSprites,
            pavedMSprites,
            pavedRSprites,
            dirtFadeoutSprites,
            dirtFadeinSprites,
            dirtLSprites,
            dirtMSprites,
            dirtRSprites,
            cobbleFadeoutSprites,
            cobbleFadeinSprites,
            cobbleLSprites,
            cobbleMSprites,
            cobbleRSprites
            );

            
    }

    // Update is called once per frame
    void Update()
    {

    }

    // EXPERIMENTAL
    private Sprite[] ConcatArrays(params Sprite[][] p)
    {
        var position = 0;
        var outputArray = new Sprite[p.Sum(a => a.Length)];
        foreach (var curr in p)
        {
            Array.Copy(curr, 0, outputArray, position, curr.Length);
            position += curr.Length;
        }
        return outputArray;
    }
    private void UpdateSummer(TileObject tile)
    {
        switch (tile.spriteType)
        {
            case TileObject.SpriteType.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrLSprites[UnityEngine.Random.Range(0, smrLSprites.Length)];
                break;
            case TileObject.SpriteType.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrMSprites[UnityEngine.Random.Range(0, smrMSprites.Length)];
                break;
            case TileObject.SpriteType.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrRSprites[UnityEngine.Random.Range(0, smrRSprites.Length)];
                break;
        }
    }

    private void UpdateFall(TileObject tile)
    {
        switch (tile.spriteType)
        {
            case TileObject.SpriteType.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallLSprites[UnityEngine.Random.Range(0, fallLSprites.Length)];
                break;
            case TileObject.SpriteType.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallMSprites[UnityEngine.Random.Range(0, fallMSprites.Length)];
                break;
            case TileObject.SpriteType.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallRSprites[UnityEngine.Random.Range(0, fallRSprites.Length)];
                break;
        }
    }

    private void UpdateWinter(TileObject tile)
    {
        switch (tile.spriteType)
        {
            case TileObject.SpriteType.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winLSprites[UnityEngine.Random.Range(0, winLSprites.Length)];
                break;
            case TileObject.SpriteType.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winMSprites[UnityEngine.Random.Range(0, winMSprites.Length)];
                break;
            case TileObject.SpriteType.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winRSprites[UnityEngine.Random.Range(0, winRSprites.Length)];
                break;
        }
    }

    private void UpdateSpring(TileObject tile)
    {
        switch (tile.spriteType)
        {
            case TileObject.SpriteType.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprLSprites[UnityEngine.Random.Range(0, sprLSprites.Length)];
                break;
            case TileObject.SpriteType.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprMSprites[UnityEngine.Random.Range(0, sprMSprites.Length)];
                break;
            case TileObject.SpriteType.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprRSprites[UnityEngine.Random.Range(0, sprRSprites.Length)];
                break;
        }
    }



    public void ProcessTile(TileObject tile)
    {
        switch (tile.tileLayer)
        {
            case TileObject.TileLayer.Overlay:
                switch (seasonState)
                {
                    case SeasonState.Summer:
                        UpdateSummer(tile);
                        break;
                        
                    case SeasonState.Fall:
                        UpdateFall(tile);
                        break;

                    case SeasonState.Winter:
                        UpdateWinter(tile);
                        break;

                    case SeasonState.Spring:
                        UpdateSpring(tile);
                        break;

                }
                break;
            case TileObject.TileLayer.Base:
                switch (pathMaterial)
                {
                    case PathMaterial.Brick:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = brickLSprites[UnityEngine.Random.Range(0, brickLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = brickMSprites[UnityEngine.Random.Range(0, brickMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = brickRSprites[UnityEngine.Random.Range(0, brickRSprites.Length)];
                                break;
                        }
                        break;
                    case PathMaterial.Paved:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = pavedLSprites[UnityEngine.Random.Range(0, pavedLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = pavedMSprites[UnityEngine.Random.Range(0, pavedMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = pavedRSprites[UnityEngine.Random.Range(0, pavedRSprites.Length)];
                                break;
                        }
                        break;
                    case PathMaterial.Cobble:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = cobbleLSprites[UnityEngine.Random.Range(0, cobbleLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = cobbleMSprites[UnityEngine.Random.Range(0, cobbleMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = cobbleRSprites[UnityEngine.Random.Range(0, cobbleRSprites.Length)];
                                break;
                        }
                        break;
                    case PathMaterial.Dirt:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = dirtLSprites[UnityEngine.Random.Range(0, dirtLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = dirtMSprites[UnityEngine.Random.Range(0, dirtMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = dirtRSprites[UnityEngine.Random.Range(0, dirtRSprites.Length)];
                                break;
                        }
                        break;
                }
                break;
        }


        // 5% chance to spawn rubble as well






        // set tile sprite to random one from [x] deck

        //foreach (TileSprite sprite in )


        // start with mega-deck of all possible sprites
        //    - sprites have "tags" or some way of indentifying
        // 




    }
    // This function shouldn't have to exist, but Unity saves enums in a weird way so this should be called once every time the scene is loaded
    public void ApplyEnums()
    {
        Transform tilemap = GameObject.Find("Tilemap").transform;

        foreach (Transform row in tilemap)
        {
            Transform baseTiles = row.Find("Base");
            Transform overlayTiles = row.Find("Overlay");

            foreach (TileObject tile in baseTiles.GetComponentsInChildren<TileObject>())
            {
                tile.tileLayer = TileObject.TileLayer.Base;
                if (tile.name == "Base 0") tile.spriteType = TileObject.SpriteType.Left;
                else if (tile.name == "Base 1" || tile.name == "Base 2") tile.spriteType = TileObject.SpriteType.Middle;
                else if (tile.name == "Base 3") tile.spriteType = TileObject.SpriteType.Right;
            }

            foreach (TileObject tile in overlayTiles.GetComponentsInChildren<TileObject>())
            {
                tile.tileLayer = TileObject.TileLayer.Overlay;
                if (tile.name == "Overlay 0") tile.spriteType = TileObject.SpriteType.Left;
                else if (tile.name == "Overlay 1" || tile.name == "Overlay 2") tile.spriteType = TileObject.SpriteType.Middle;
                else if (tile.name == "Overlay 3") tile.spriteType = TileObject.SpriteType.Right;
            }
        }
    }
}
