using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeObject : MonoBehaviour
{
    public Sprite[] spriteList;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = spriteList[Random.Range(0, spriteList.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
