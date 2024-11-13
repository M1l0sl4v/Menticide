using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashingColistions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hihihi");
            enemyHealth health = collision.gameObject.GetComponentInChildren<enemyHealth>();
            health.TakeDamage(10);
        }
    }
}
