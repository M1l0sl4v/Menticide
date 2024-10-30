using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class enemyDog : MonoBehaviour
{
    public LayerMask myLayerMask;
    public float losDistance;
    public float enemyspeedChase;
    public float enemyspeedNormal;
    public float despawnDistanceDog = 10f;

    [SerializeField] private GameObject exlimation;

    private Vector3 targetCellWorldPos;
    private bool lineOfSight = false;
    private bool lineOfSightLine = false;
    private bool surprised = true;
    private GameObject player;
    private GameObject e;
    public List<AudioClip> noticeSounds;

    public SpriteRenderer sp;
    private Animator animator;
    private Tilemap tilemap;

    private float enemyspeedChaseOrng;
    private float enemyspeedNormalOrng;



    private void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("enemyTilemap").GetComponent<Tilemap>();
        player = GameObject.FindGameObjectWithTag("Player");
        targetCellWorldPos = transform.position;
        animator = GetComponent<Animator>();
        enemyspeedChaseOrng = enemyspeedChase;
        enemyspeedNormalOrng = enemyspeedNormal;
    }

    private void Update()
    {
        // Measure the distance to the player and return to the object pool if too far away
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist > despawnDistanceDog)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            return;
        }
        if (lineOfSightLine == true)
        {
            enemyspeedChase = 0;
            enemyspeedNormal = 0;
        }
        else
        {
            enemyspeedChase = enemyspeedChaseOrng;
            enemyspeedNormal = enemyspeedNormalOrng;
        }

        enemyAi();
    }

    private void FixedUpdate()
    {
        // Check for line of sight with the player
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, losDistance, myLayerMask);
        if (ray.collider != null)
        {
            lineOfSight = ray.collider.CompareTag("Player");
            lineOfSightLine = ray.collider.CompareTag("Wall");
            Debug.DrawRay(transform.position, player.transform.position - transform.position, lineOfSight ? Color.green : Color.red);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("cullingField"))
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
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

    private void idle()
    {
        surprised = true; // Reset surprise state when idle
                          // Here, we could implement some idle movement logic if needed
        transform.position = Vector2.MoveTowards(transform.position, targetCellWorldPos, enemyspeedNormal * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetCellWorldPos) < 0.001f)
        {
            findIdleNectPos();
        }
    }
    void findIdleNectPos()//set pos to go to "cell" then it goes to it then finds nex pos
    {
        Vector3Int nextCellIdle = new Vector3Int(Random.Range(-4, 4), Random.Range(-4, 4),0);

        Vector3Int enemyCell = tilemap.WorldToCell(transform.position);
        Vector3Int nextCell = enemyCell;


        if (Mathf.Abs(nextCellIdle.x - enemyCell.x) >= Mathf.Abs(nextCellIdle.y - enemyCell.y))
        {
            if (nextCellIdle.x > enemyCell.x)
            {
                nextCell.x += 1;

                animator.SetBool("WalkRight", true);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkDown", false);
            }
            else if (nextCellIdle.x < enemyCell.x)
            {
                nextCell.x -= 1;

                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", true);
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkDown", false);
            }
        }
        else
        {
            if (nextCellIdle.y > enemyCell.y)
            {
                nextCell.y += 1;
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUp", true);
                animator.SetBool("WalkDown", false);
            }
            else if (nextCellIdle.y < enemyCell.y)
            {
                nextCell.y -= 1;
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkDown", true);
            }
        }
        targetCellWorldPos = tilemap.GetCellCenterWorld(nextCell);
    
    }

    private void chase()
    {
        // Move towards the target cell center position
        transform.position = Vector2.MoveTowards(transform.position, targetCellWorldPos, enemyspeedChase * Time.deltaTime);

        // If the enemy reaches the center of the target cell, recalculate path
        if (Vector3.Distance(transform.position, targetCellWorldPos) < 0.001f)
        {
            calculatePathToPlayer();
        }
    }

    private void stuned()
    {
        if (surprised)
        {
            int prefabIndex = Random.Range(0, noticeSounds.Count);
            float pitch = Random.Range(.7f, 1.3f);
            AudioManager.instance.enemyFX(noticeSounds[prefabIndex], transform, 1f, pitch);
            e = Instantiate(exlimation, transform); // Show exclamation mark
            e.transform.parent = transform;
            surprised = false; // Avoid re-triggerings
            animator.SetTrigger("stun");
        }

        if (e == null)
        {
            chase();
        }
    }

    private void calculatePathToPlayer()
    {
        Vector3Int enemyCell = tilemap.WorldToCell(transform.position);
        Vector3Int playerCell = tilemap.WorldToCell(player.transform.position);
        Vector3Int nextCell = enemyCell;
        if (Mathf.Abs(playerCell.x - enemyCell.x) >= Mathf.Abs(playerCell.y - enemyCell.y))
        {
            if (playerCell.x > enemyCell.x)
            {
                nextCell.x += 1;

                animator.SetBool("WalkRight", true);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkDown", false);
            }
            else if (playerCell.x < enemyCell.x)
            {
                nextCell.x -= 1;

                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", true);
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkDown", false);
            }
        }
        else
        {
            if (playerCell.y > enemyCell.y)
            {
                nextCell.y += 1;
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUp", true);
                animator.SetBool("WalkDown", false);
            }
            else if (playerCell.y < enemyCell.y)
            {
                nextCell.y -= 1;
                animator.SetBool("WalkRight", false);
                animator.SetBool("WalkLeft", false);
                animator.SetBool("WalkUp", false);
                animator.SetBool("WalkDown", true);
            }
        }
        targetCellWorldPos = tilemap.GetCellCenterWorld(nextCell);
    }
}