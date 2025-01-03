using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpitter : MonoBehaviour
{
    public GameObject progectialSpitter;
    public float fireRate = 1f;
    public float idleDelay = 3f;
    public int bulletSpeed = 10;
    public int shootAhead = 10;

    public bool singleShot;

    public enum States
    {
        Idle,
        Atack
    }
    
    private Animator animator;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        EnemyStates(States.Idle);
        animator = gameObject.GetComponent<Animator>();
    }
    IEnumerator SpitterShootNorm()
    {
        //TriggerAttackAnimation();
        Shoot();
        yield return new WaitForSeconds(fireRate);
        EnemyStates(States.Idle);
    }
    IEnumerator SpitterShootCluster()
    {
        yield return new WaitForSeconds(fireRate);
        ShootCluster(5, 10, 0);
    }
    IEnumerator SpitterShootShotgun()
    {
        //TriggerAttackAnimation();
        yield return new WaitForSeconds(fireRate);
        ShootShotgun(5,3);
        EnemyStates(States.Idle);
    }
    IEnumerator Idle()
    {
        //TriggerIdleAnimation();
        yield return new WaitForSeconds(idleDelay);
        EnemyStates(States.Atack);
    }
    void Shoot()
    {
        Vector3 direction = player.transform.position - transform.position + new Vector3(0, shootAhead, 0);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        progectial.CreateProjectile(progectialSpitter, transform.position, rotation, bulletSpeed, gameObject);
    }
    void ShootCluster(int HowMany,int spread,int Speeds)
    {
        for(int i=0; i < HowMany; i++)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90 + Random.Range(-spread, spread);
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            progectial.CreateProjectileCluster(progectialSpitter, transform.position, rotation, Speeds,0,gameObject);
        }
    }
    void ShootShotgun(int HowMany, int Speed)
    {
        for (int i = HowMany *-1; i < HowMany; i+=2)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90 + i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            progectial.CreateProjectile(progectialSpitter, transform.position, rotation, Speed, gameObject);
        }
    }
    void EnemyStates(States states)
    {
        switch (states) {
            case States.Idle:
                TriggerIdleAnimation();
                StartCoroutine(Idle());
                break;
            case States.Atack:
                TriggerAttackAnimation();
                if (singleShot)
                {
                    StartCoroutine(SpitterShootNorm());
                }
                else if (!singleShot)
                {
                    StartCoroutine(SpitterShootShotgun());
                }
                break;
        }

    }
    void TriggerAttackAnimation()
    {
        if (animator)
        {
            animator.SetTrigger("Attack");
        }
    }

    void TriggerIdleAnimation()
    {
        if (animator)
        {
            animator.SetTrigger("Idle");            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cullingField"))
        {
            Destroy(gameObject);
        }
    }
}
