using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree : MonoBehaviour
{
    public float treespeed;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(0, -treespeed) * Time.deltaTime);
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
