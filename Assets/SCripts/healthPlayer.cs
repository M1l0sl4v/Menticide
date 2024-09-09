using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPlayer: MonoBehaviour
{
    static public int maxHealth=3;
    static public int health;
    public uiHearts uiHearts;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        uiHearts.StartHealth(health);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        uiHearts.updateHealth(health);
        if (health <= 0)
        {
            return;
        }
    }
}
