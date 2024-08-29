using UnityEngine;

public class Scroll : MonoBehaviour
{
    public Material wallMaterial;
    public float scrollSpeed = 0.5f;

    void Update()
    {
        ScrollTexture();
    }

    void ScrollTexture()
    {
        float offset = Time.time * scrollSpeed;
        wallMaterial.mainTextureOffset = new Vector2(offset, 0f);
    }
}