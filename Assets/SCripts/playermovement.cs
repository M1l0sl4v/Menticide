using System.Collections;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed = 5f;
    public float slideDuration = 0.5f;
    public float speedMultiplier = 1.5f; // How much the speed increases during collision
    public float speedDecayRate = 1f; // Rate at which speed decreases back to original

    private float originalSpeed;
    private Vector2 direction = Vector2.up;
    private bool isCollidingWithWall = false;

    private void Start()
    {
        originalSpeed = speed;
    }

    void Update()
    {
        // Gradually decrease speed back to original speed
        if (!isCollidingWithWall && speed > originalSpeed)
        {
            speed -= speedDecayRate * Time.deltaTime;
            if (speed < originalSpeed)
            {
                speed = originalSpeed;
            }
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 wallNormal = collision.contacts[0].normal;
            Vector2 reflectedDirection = Vector2.Reflect(direction, wallNormal);

            // Add a bias towards moving upwards
            reflectedDirection += new Vector2(0, 1f);
            direction = reflectedDirection.normalized;

            isCollidingWithWall = true;
            speed = originalSpeed * speedMultiplier;  // Increase speed on collision
            StartCoroutine(Slide());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isCollidingWithWall = false;
        }
    }

    IEnumerator Slide()
    {
        yield return new WaitForSeconds(slideDuration);
        direction = Vector2.up;
    }
}
