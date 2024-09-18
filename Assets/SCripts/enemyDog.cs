using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyDog : MonoBehaviour
{
    public LayerMask myLayerMask;
    public float losDistance;
    public float enemyspeedChase;
    public float enemyspeedNormal;
    public float despawnDistance = 50;
    public int enemyBehaviour;
    private Vector3 currentPos;

    public float despawnDistanceDog = 10f;

    private Vector3 enemyStartPos;
    private Vector2 _direction;
    private int distance;

    private bool lineOfSight = false;
    public SpriteRenderer sp;
    private GameObject player;
    
    public GameObject exlimation;
    private bool surprised = true;
    private GameObject e;


    void Update()
    {

        //this measures the distance to the player and returns the object to the pool when it gets too far away
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 currentPos = transform.position;
        float dist = Vector3.Distance(currentPos, playerPos);
        if (dist > despawnDistanceDog)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        enemyAi();
    }
    // hmmmm
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "cullingField")
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            playermovement.instance.TakeDamage(1);
        }
    }

    private void enemyAi()
    {
        if (lineOfSight)
        {
            stuned();
        }
        else
        {
            idle();
        }
    }

    private void Start()
    {
        distance = Random.Range(5, 10);
        enemyStartPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, losDistance,myLayerMask);
        if (ray.collider != null)
        {
            lineOfSight = ray.collider.CompareTag("Player");
            if (lineOfSight)
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
            }
        }
    }
    private void idle()
    {
        surprised = true;

        transform.Translate(_direction * Time.deltaTime);

        if (Mathf.Abs(enemyStartPos.x - currentPos.x) <= distance)
        {
            _direction = new Vector2(enemyspeedNormal, 0);
        }
        else
        {
            enemyspeedNormal *= -1;
            distance = Random.Range(5, 10);
            enemyStartPos = transform.position;
            sp.flipX = !sp.flipX;
        }
    }
    private void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyspeedChase * Time.deltaTime);
    }
    private void stuned()
    {
        if (surprised)
        {
            e = Instantiate(exlimation, transform);
            e.transform.parent = transform;
            surprised = false;
        }
        if (e == null)
        {
            chase();
        }
        
    }
}
