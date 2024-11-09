using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashingColistions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.ToString());
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hihihi");
            enemyHealth health = collision.gameObject.GetComponentInChildren<enemyHealth>();
            if (health) health.TakeDamage(10);
        }
    }
}
