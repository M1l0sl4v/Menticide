using System.Collections;
using System.Collections.Generic;
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
        Paved
    }
    public PathMaterial pathMaterial;

    public static float tileResetDistance = 18;

    [Header("Summer Sprites")]
    public Sprite[] smrLSprites;
    public Sprite[] smrMSprites;
    public Sprite[] smrRSprites;
    [Header("Early Fall Sprites")]
    public Sprite[] earlyFallLSprites;
    public Sprite[] earlyFallMSprites;
    public Sprite[] earlyFallRSprites;
    [Header("Fall Sprites")]
    public Sprite[] fallLSprites;
    public Sprite[] fallMSprites;
    public Sprite[] fallRSprites;
    [Header("Early Winter Sprites")]
    public Sprite[] earlyWinLSprites;
    public Sprite[] earlyWinMSprites;
    public Sprite[] earlyWinRSprites;
    [Header("Winter Sprites")]
    public Sprite[] winLSprites;
    public Sprite[] winMSprites;
    public Sprite[] winRSprites;
    [Header("Early Spring Sprites")]
    public Sprite[] earlySprLSprites;
    public Sprite[] earlySprMSprites;
    public Sprite[] earlySprRSprites;
    [Header("Spring Sprites")]
    public Sprite[] sprLSprites;
    public Sprite[] sprMSprites;
    public Sprite[] sprRSprites;
    [Header("Early Summer Sprites")]
    public Sprite[] earlySmrLSprites;
    public Sprite[] earlySmrMSprites;
    public Sprite[] earlySmrRSprites;

    public static TileManagerFSM instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (seasonState)
        {
            case SeasonState.Summer:
                UpdateSummer(); break;
            case SeasonState.EarlyFall:
                UpdateEarlyFall(); break;
            case SeasonState.Fall:
                UpdateFall(); break;
            case SeasonState.EarlyWinter:
                UpdateEarlyWinter(); break;
            case SeasonState.Winter:
                UpdateWinter(); break;
            case SeasonState.EarlySpring:
                UpdateEarlySpring(); break;
            case SeasonState.Spring:
                UpdateSpring(); break;
            case SeasonState.EarlySummer:
                UpdateEarlySummer(); break;
        }

    }

    private void UpdateSummer()
    {

    }

    private void UpdateEarlyFall()
    {
        
    }

    private void UpdateFall()
    {

    }

    private void UpdateEarlyWinter()
    {

    }
    private void UpdateWinter()
    {

    }

    private void UpdateEarlySpring()
    {

    }

    private void UpdateSpring()
    {

    }

    private void UpdateEarlySummer()
    {

    }

    public void ProcessTile(TileObject tile)
    {
        switch (tile.tileLayer)
        {
            case TileObject.TileLayer.Base:
                switch (seasonState)
                {
                    //Sprite sprite = tile.gameObject.GetComponent<SpriteRenderer>().sprite 
                    case SeasonState.Summer:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrLSprites[Random.Range(0, smrLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrMSprites[Random.Range(0, smrMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = smrRSprites[Random.Range(0, smrRSprites.Length)];
                                break;
                        }
                        break;
                    case SeasonState.EarlyFall:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyFallLSprites[Random.Range(0, earlyFallLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyFallMSprites[Random.Range(0, earlyFallMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyFallRSprites[Random.Range(0, earlyFallRSprites.Length)];
                                break;
                        }
                        break;
                    case SeasonState.Fall:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallLSprites[Random.Range(0, fallLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallMSprites[Random.Range(0, fallMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = fallRSprites[Random.Range(0, fallRSprites.Length)];
                                break;
                        }
                        break;
                    case SeasonState.EarlyWinter:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyWinLSprites[Random.Range(0, earlyWinLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyWinMSprites[Random.Range(0, earlyWinMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlyWinRSprites[Random.Range(0, earlyWinRSprites.Length)];
                                break;
                        }
                        break;
                    case SeasonState.Winter:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winLSprites[Random.Range(0, winLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winMSprites[Random.Range(0, winMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = winRSprites[Random.Range(0, winRSprites.Length)];
                                break;
                        }
                        break;
                    case SeasonState.EarlySpring:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySprLSprites[Random.Range(0, earlySprLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySprMSprites[Random.Range(0, earlySprMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySprRSprites[Random.Range(0, earlySprRSprites.Length)];
                                break;
                        }
                        break;
                    case SeasonState.Spring:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprLSprites[Random.Range(0, sprLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprMSprites[Random.Range(0, sprMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = sprRSprites[Random.Range(0, sprRSprites.Length)];
                                break;
                        }
                        break;
                    case SeasonState.EarlySummer:
                        switch (tile.spriteType)
                        {
                            case TileObject.SpriteType.Left:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySmrLSprites[Random.Range(0, earlySmrLSprites.Length)];
                                break;
                            case TileObject.SpriteType.Middle:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySmrMSprites[Random.Range(0, earlySmrMSprites.Length)];
                                break;
                            case TileObject.SpriteType.Right:
                                tile.gameObject.GetComponent<SpriteRenderer>().sprite = earlySmrRSprites[Random.Range(0, earlySmrRSprites.Length)];
                                break;
                        }
                        break;
                } break;
            case TileObject.TileLayer.Overlay:
                switch (pathMaterial)
                {
                    case PathMaterial.Brick:
                        // change base material to brick
                        break;
                    case PathMaterial.Paved:
                        // change base material to paved
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
