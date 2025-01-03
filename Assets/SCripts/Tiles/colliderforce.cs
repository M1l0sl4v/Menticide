using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderforce : MonoBehaviour
{
    public float pushforce = 10f;
    
    public void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            rb.AddForce(pushDirection * pushforce, ForceMode2D.Impulse);
            Debug.Log("Player touched and pushed");
        }
    }
}
