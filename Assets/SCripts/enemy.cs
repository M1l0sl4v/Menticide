using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemyspeed;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, enemyspeed) * Time.deltaTime);
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

