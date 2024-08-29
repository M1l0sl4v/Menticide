using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float slideDuration = 0.5f;
    private Vector2 direction = Vector2.up;

    private void Start()
    {
   
    }
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Vector2 wallNormal = collision.contacts[0].normal;
            direction = Vector2.Reflect(direction, wallNormal);
            StartCoroutine(Slide());
        }
    }

    IEnumerator Slide()
    {
        yield return new WaitForSeconds(slideDuration);
        direction = Vector2.up;
    }
}




//void OnCollisionEnter2D(Collision2D collision)
//{
//    if (collision.collider.CompareTag("wall"))
//    {
//        // Get the normal of the collision
//        Vector2 normal = collision.contacts[0].normal;

//        // Calculate the bounce direction
//        Vector2 bounceDirection = Vector2.Reflect(rb.velocity.normalized, normal).normalized;

//        // Keep the vertical speed constant, only change the horizontal component
//        bounceDirection.y = rb.velocity.normalized.y;

//        // Apply the bounce direction with additional force
//        rb.velocity = bounceDirection * speed + bounceForce * normal;
//    }
//}
