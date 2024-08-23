using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pathScroll : MonoBehaviour
{
    public float Scroll;
    private Material path;
    private MeshRenderer _meshRenderer;
    void Start()
    {
        //finds the local meshrenderer
        _meshRenderer = GetComponent<MeshRenderer>();

    }
    void Update()
    {
        //just moves the texture a litle bit every frame
        _meshRenderer.material.mainTextureOffset = new Vector2(0, Scroll * Time.time);
    }
}
