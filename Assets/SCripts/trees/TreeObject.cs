using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public Sprite[] spriteList;
    public static int layerToUse;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = spriteList[Random.Range(0, spriteList.Length)];
        GetComponent<SpriteRenderer>().sortingOrder = --layerToUse;
    }


}
