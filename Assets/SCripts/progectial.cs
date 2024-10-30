using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class progectial : MonoBehaviour
{
    public int HowMuchDamage = 1;
    public float bulletSpeed = 20f;
    public bool BounceOffWall;
    public float LifeSpan = 3f;


    private Collider2D col;
    private Rigidbody2D rb;

    private bool CanHitEnemy;
    // Start is called before the first frame update
    private void OnEnable()
    {
        col = GetComponentInChildren<CircleCollider2D>();
        rb = GetComponentInChildren<Rigidbody2D>();
    }
    void Start()
    {
        Destroy(gameObject, LifeSpan);
        if (BounceOffWall == false)
        {
            col.enabled = false;
        }
        rb.velocity = transform.up * bulletSpeed;
    }

    internal static void CreateProjectile(GameObject progectialSpitter, Vector3 position, float angle)
    {
        throw new NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//do player collition
        {
            playermovement.instance.TakeDamage(HowMuchDamage);
            Destroy();
        }
        if (other.gameObject.CompareTag("Wall"))//do wall collition
        {
            if (BounceOffWall == false)
            {
                Destroy();
            }
            else
            {
                CanHitEnemy = true;
            }
        }
        if (other.gameObject.CompareTag("cullingField"))
        {
            Destroy();
        }
        if (other.gameObject.CompareTag("Enemy") && CanHitEnemy == true)
        {
            //ObjectPoolManager.ReturnObjectToPool(other.gameObject);
            Destroy(other.gameObject);
            Destroy();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletSpeed /= 2;
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
    public static void CreateProjectile(GameObject projectile, Vector3 position, Quaternion direction,float Speed)
    {
        float temp = Speed;
        projectile.GetComponent<progectial>().bulletSpeed += temp;
        Instantiate(projectile, position, direction);
        projectile.GetComponent<progectial>().bulletSpeed -= temp;
    }
    public static void CreateProjectileCluster(GameObject projectile, Vector3 position, Quaternion direction, float Speed,float SpeedVar)
    {
        float temp = Random.Range(0, SpeedVar);
        projectile.GetComponent<progectial>().bulletSpeed += temp +Speed;
        Instantiate(projectile, position, direction);
        projectile.GetComponent<progectial>().bulletSpeed -= temp+ Speed;
    }

}
