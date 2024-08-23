using UnityEngine;

public class PlayArea : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform

    void Update()
    {
        if (player != null)
        {
            // Match the y position of this GameObject to the player's y position
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }
}
