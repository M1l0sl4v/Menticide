using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public SpriteRenderer HealthBar;
    public TextMeshPro text;
    public float totalHealth;
    public GameObject enemy;
    private void Start()
    {
        TakeDamage(0);
    }
    public void TakeDamage(int amount)
    {
        totalHealth -= amount;
        text.text = totalHealth.ToString();
        HealthBar.size = new Vector2(totalHealth/100f,1);
        if (totalHealth <= 0)
        {
            ObjectPoolManager.ReturnObjectToPool(enemy);
        }
    }
}