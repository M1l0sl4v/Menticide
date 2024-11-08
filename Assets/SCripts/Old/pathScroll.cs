using UnityEngine;

public class pathscroll : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private Vector2 savedOffset;
    private Material material;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
        savedOffset = material.mainTextureOffset;
    }

    void Update()
    {
        float y = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(savedOffset.x, y);
        material.mainTextureOffset = offset;
    }

    void OnDisable()
    {
        material.mainTextureOffset = savedOffset;
    }
}
