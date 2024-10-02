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

    [SerializeField] private AudioClip snarl;
    [SerializeField] private GameObject exlimation;

    private Vector3 enemyStartPos;
    private Vector3 targetCellWorldPos;
    private bool lineOfSight = false;
    private bool surprised = true;
    private GameObject player;
    private GameObject e;

    public SpriteRenderer sp;
    public Animator animator;
    public Tilemap tilemap;

    private void Start()
    {
        enemyStartPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player"); // Cache player reference
        targetCellWorldPos = transform.position;

    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist > despawnDistanceDog)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
            return;
        }

        enemyAi();
    }

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, losDistance, myLayerMask);
        if (ray.collider != null)
        {
            lineOfSight = ray.collider.CompareTag("Player");
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
    }

    private void chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetCellWorldPos, enemyspeedChase * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetCellWorldPos) < 0.01f)
        {
            calculatePathToPlayer();
        }
    }

    private void stuned()
    {
        if (surprised)
        {
            AudioManager.instance.enemyFX(snarl, transform, 1f); // Play snarl sound
            e = Instantiate(exlimation, transform); // Show exclamation mark
            e.transform.parent = transform;
            surprised = false; // Avoid re-triggering
            animator.SetTrigger("stun");
        }

        if (e == null)
        {
            calculatePathToPlayer();
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
                nextCell.x += 1; // Move right
            }
            else if (playerCell.x < enemyCell.x)
            {
                nextCell.x -= 1; // Move left
            }
        }
        else
        {
            if (playerCell.y > enemyCell.y)
            {
                nextCell.y += 1; // Move up
            }
            else if (playerCell.y < enemyCell.y)
            {
                nextCell.y -= 1; // Move down
            }
        }

        targetCellWorldPos = tilemap.GetCellCenterWorld(nextCell);
    }
}
