using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playermovement.instance.AddHealth(1);
            Debug.LogWarning("healthup");
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

}
