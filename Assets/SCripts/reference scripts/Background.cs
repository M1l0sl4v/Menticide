using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float scrollSpeed = 1f;

    void Update()
    {
        // Move the background based on character movement
        float offset = Time.deltaTime * scrollSpeed;
        transform.Translate(Vector3.left * offset);

        // Check if the background has moved off-screen, then reposition it
        if (transform.position.x < -GetComponent<SpriteRenderer>().bounds.size.x)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        // Move the background to the opposite end of the screen
        float offset = 2 * GetComponent<SpriteRenderer>().bounds.size.x;
        transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);
    }
}
