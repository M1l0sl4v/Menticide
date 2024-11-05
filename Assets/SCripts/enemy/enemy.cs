using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemyspeed;
    public float despawnDistance = 50;
    
    void Update()
    {
        transform.Translate(new Vector2(0, enemyspeed) * Time.deltaTime);
        
        //this measures the distance to the player and returns the object to the pool when it gets too far away
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 currentPos = transform.position;
        float dist = Vector3.Distance(currentPos, playerPos);
        if (dist > despawnDistance)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
    // hmmmm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }

    


}

