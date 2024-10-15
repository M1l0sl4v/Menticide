using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldTileFSM : MonoBehaviour
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

    //[Header("Early Fall Sprites")]
    //public Sprite[] earlyFallLSprites;
    //public Sprite[] earlyFallMSprites;
    //public Sprite[] earlyFallRSprites;
    //[Header("Early Winter Sprites")]
    //public Sprite[] earlyWinLSprites;
    //public Sprite[] earlyWinMSprites;
    //public Sprite[] earlyWinRSprites;
    //[Header("Early Spring Sprites")]
    //public Sprite[] earlySprLSprites;
    //public Sprite[] earlySprMSprites;
    //public Sprite[] earlySprRSprites;
    //[Header("Early Summer Sprites")]
    //public Sprite[] earlySmrLSprites;
    //public Sprite[] earlySmrMSprites;
    //public Sprite[] earlySmrRSprites;

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

    public static OldTileFSM instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //switch (seasonState)
        //{
        //    case SeasonState.Summer:
        //        UpdateSummer(); break;
        //    case SeasonState.EarlyFall:
        //        UpdateEarlyFall(); break;
        //    case SeasonState.Fall:
        //        UpdateFall(); break;
        //    case SeasonState.EarlyWinter:
        //        UpdateEarlyWinter(); break;
        //    case SeasonState.Winter:
        //        UpdateWinter(); break;
        //    case SeasonState.EarlySpring:
        //        UpdateEarlySpring(); break;
        //    case SeasonState.Spring:
        //        UpdateSpring(); break;
        //    case SeasonState.EarlySummer:
        //        UpdateEarlySummer(); break;
        //}

    }

    private void UpdateSummer(TileObject tile)
    {
        switch (tile.direction)
        {
            case TileObject.Direction.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrLSprites[Random.Range(0, smrLSprites.Length)];
                break;
            case TileObject.Direction.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrMSprites[Random.Range(0, smrMSprites.Length)];
                break;
            case TileObject.Direction.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrRSprites[Random.Range(0, smrRSprites.Length)];
                break;
        }
    }


    private void UpdateEarlyFall(TileObject tile)
    {

    }

    private void UpdateFall(TileObject tile)
    {
        switch (tile.direction)
        {
            case TileObject.Direction.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallLSprites[Random.Range(0, fallLSprites.Length)];
                break;
            case TileObject.Direction.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallMSprites[Random.Range(0, fallMSprites.Length)];
                break;
            case TileObject.Direction.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallRSprites[Random.Range(0, fallRSprites.Length)];
                break;
        }
    }

    private void UpdateEarlyWinter(TileObject tile)
    {

    }
    private void UpdateWinter(TileObject tile)
    {
        switch (tile.direction)
        {
            case TileObject.Direction.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winLSprites[Random.Range(0, winLSprites.Length)];
                break;
            case TileObject.Direction.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winMSprites[Random.Range(0, winMSprites.Length)];
                break;
            case TileObject.Direction.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winRSprites[Random.Range(0, winRSprites.Length)];
                break;
        }
    }

    private void UpdateEarlySpring(TileObject tile)
    {

    }

    private void UpdateSpring(TileObject tile)
    {
        switch (tile.direction)
        {
            case TileObject.Direction.Left:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprLSprites[Random.Range(0, sprLSprites.Length)];
                break;
            case TileObject.Direction.Middle:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprMSprites[Random.Range(0, sprMSprites.Length)];
                break;
            case TileObject.Direction.Right:
                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprRSprites[Random.Range(0, sprRSprites.Length)];
                break;
        }
    }

    private void UpdateEarlySummer(TileObject tile)
    {

    }

    public void ProcessTile(TileObject tile)
    {
        switch (tile.layer)
        {
            case TileObject.Layer.Overlay:
                switch (seasonState)
                {
                    case SeasonState.Summer:
                        UpdateSummer(tile);
                        break;

                    case SeasonState.EarlyFall:
                        //switch (tile.spriteType)
                        //{
                        //    //case TileObject.SpriteType.Left:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyFallLSprites[Random.Range(0, earlyFallLSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Middle:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyFallMSprites[Random.Range(0, earlyFallMSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Right:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyFallRSprites[Random.Range(0, earlyFallRSprites.Length)];
                        //    //    break;
                        //}
                        //break;
                    case SeasonState.Fall:
                        UpdateFall(tile);
                        break;
                    case SeasonState.EarlyWinter:
                        //switch (tile.spriteType)
                        //{
                        //    //case TileObject.SpriteType.Left:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyWinLSprites[Random.Range(0, earlyWinLSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Middle:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyWinMSprites[Random.Range(0, earlyWinMSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Right:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyWinRSprites[Random.Range(0, earlyWinRSprites.Length)];
                        //    //    break;
                        //}
                        //break;
                    case SeasonState.Winter:
                        UpdateWinter(tile);
                        break;
                    case SeasonState.EarlySpring:
                        //switch (tile.spriteType)
                        //{
                        //    //case TileObject.SpriteType.Left:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySprLSprites[Random.Range(0, earlySprLSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Middle:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySprMSprites[Random.Range(0, earlySprMSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Right:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySprRSprites[Random.Range(0, earlySprRSprites.Length)];
                        //    //break;
                        //}
                        //break;
                    case SeasonState.Spring:
                        UpdateSpring(tile);
                        break;
                    case SeasonState.EarlySummer:
                        //switch (tile.spriteType)
                        //{
                        //    //case TileObject.SpriteType.Left:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySmrLSprites[Random.Range(0, earlySmrLSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Middle:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySmrMSprites[Random.Range(0, earlySmrMSprites.Length)];
                        //    //    break;
                        //    //case TileObject.SpriteType.Right:
                        //    //    tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySmrRSprites[Random.Range(0, earlySmrRSprites.Length)];
                        //    //break;
                        //}
                        //break;
                        break;
                }
                break;
            case TileObject.Layer.Base:
                switch (pathMaterial)
                {
                    case PathMaterial.Brick:
                        switch (tile.direction)
                        {
                            case TileObject.Direction.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = brickLSprites[Random.Range(0, brickLSprites.Length)];
                                break;
                            case TileObject.Direction.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = brickMSprites[Random.Range(0, brickMSprites.Length)];
                                break;
                            case TileObject.Direction.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = brickRSprites[Random.Range(0, brickRSprites.Length)];
                                break;
                        }
                        break;
                    case PathMaterial.Paved:
                        switch (tile.direction)
                        {
                            case TileObject.Direction.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = pavedLSprites[Random.Range(0, pavedLSprites.Length)];
                                break;
                            case TileObject.Direction.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = pavedMSprites[Random.Range(0, pavedMSprites.Length)];
                                break;
                            case TileObject.Direction.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = pavedRSprites[Random.Range(0, pavedRSprites.Length)];
                                break;
                        }
                        break;
                    case PathMaterial.Cobble:
                        switch (tile.direction)
                        {
                            case TileObject.Direction.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = cobbleLSprites[Random.Range(0, cobbleLSprites.Length)];
                                break;
                            case TileObject.Direction.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = cobbleMSprites[Random.Range(0, cobbleMSprites.Length)];
                                break;
                            case TileObject.Direction.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = cobbleRSprites[Random.Range(0, cobbleRSprites.Length)];
                                break;
                        }
                        break;
                    case PathMaterial.Dirt:
                        switch (tile.direction)
                        {
                            case TileObject.Direction.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = dirtLSprites[Random.Range(0, dirtLSprites.Length)];
                                break;
                            case TileObject.Direction.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = dirtMSprites[Random.Range(0, dirtMSprites.Length)];
                                break;
                            case TileObject.Direction.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = dirtRSprites[Random.Range(0, dirtRSprites.Length)];
                                break;
                        }
                        break;
                }
                break;
        }


        // 5% chance to spawn rubble as well






        // set tile sprite to random one from [x] deck



        // start with mega-deck of all possible sprites
        //    - sprites have "tags" or some way of indentifying
        // 




    }
}
