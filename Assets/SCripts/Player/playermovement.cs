
using System.Collections;
using System.Diagnostics.Contracts;
using UnityEngine;
using TMPro;
using System.Diagnostics.CodeAnalysis;
public class playermovement : MonoBehaviour
{
    // Movement speed of the player
    public float speed = 5f;
    public float maxSpeed;
    // Multiplier for speed increase when colliding with a wall
    public float speedMultiplier = 1.5f;

    // Rate at which speed decreases back to the original value
    public float speedDecayRate = 1f;

    public float _originalSpeed; // Store the initial speed
    private Vector2 _direction = Vector2.up; // Current movement direction
    private bool _isCollidingWithWall = false; // Flag to check if the player is colliding with a wall

    public float resetTriggerDistance;
    //distance at which the player is reset
    
    //screenshake stuff
    public float screenshakeAmount;
    public float screenshakeTime;

    static public int maxHealth = 3;
    public int health;
    public uiHearts uiHearts;
    public float invincibilityDuration;
    private float invincibilityLeft;
    public static playermovement instance;


    //player statis effects


    [SerializeField] private AudioClip takeDamageSound;



    private Vector3 _target;
    public Camera Camera;

    private void Start()
    {
        // Initialize the original speed value
        _originalSpeed = speed;
        health = maxHealth;
        uiHearts.StartHealth(health);
        instance = this;
    }
    
    public void TakeDamage(int amount)
    {
        if (invincibilityLeft <= 0 && !DeathSequence.controlLock && !StaticDebugTools.instance.playerInvincibility)
        {
            AudioManager.instance.playerFX(takeDamageSound, transform, 1f, 1f);
            cameraShake.instance.shakeCamera(screenshakeAmount, screenshakeTime);
            for (int i = health - 1; i > (health - 1) - amount; i--)
            {
                if (i >= 0) uiHearts.hearts[i].GetComponent<Animator>().SetTrigger("HeartLost");
            }
            health -= amount;

            //uiHearts.hearts[health - 1].GetComponent<Animator>().SetTrigger("HeartLost");
            StartCoroutine(DamageSequence());
            if (health <= 0)
            {
                DeathSequence.instance.StartDeathSequence();
            }

            // Begin i-frames
            invincibilityLeft = invincibilityDuration;
        }
    }
    public void AddHealth(int amount)//health buffs
    {
        health += amount;
        uiHearts.updateHealth(health);
    }
    

    private IEnumerator DamageSequence()
    {
        yield return new WaitForSeconds(0.417f); // length of Heartdestroy
        uiHearts.updateHealth(health);
    }

    private void Update()
    {
        
        //Debug.Log(speed);
        // Gradually decrease the speed back to the original value if not colliding with a wall
        if (!_isCollidingWithWall && speed > _originalSpeed)
        {
            speed -= speedDecayRate * Time.deltaTime;
            if (speed < _originalSpeed)
            {
                speed = Mathf.Clamp(_originalSpeed,5,maxSpeed);
            }
        }

        //movePointAndClick();
        // Move the player in the current direction
        transform.Translate(_direction * speed * Time.deltaTime);

        //pointAmount.text = ((int)transform.position.y).ToString();
        //this checks at certain intervuls, this will be changed later depending on what we want.
        //if (transform.position.y >= resetTriggerDistance)
        //{
        //    playerReset();
        //}

        // DEBUG: Kill player
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            bool before = StaticDebugTools.instance.playerInvincibility;
            StaticDebugTools.instance.playerInvincibility = false;
            TakeDamage(health);
            StaticDebugTools.instance.playerInvincibility = before;
        }
        // i-frames
        if (invincibilityLeft > 0) invincibilityLeft -= Time.deltaTime;
        if (invincibilityLeft < 0) invincibilityLeft = 0;
        GetComponent<Animator>().SetFloat("I Frames", invincibilityLeft);
        moveWASD();

        // Limit speed
        if (!StaticDebugTools.instance.playerMoveSpeedOverride)
        {
            speed = Mathf.Clamp(speed, 5, maxSpeed);
        }
    }
//this sets the player back to zero
//i am using this method to move everything else back, but it is not working great
    public void playerReset()
    {
       GameManager.instance.backToZero(resetTriggerDistance);
        seasons.instance.seasonChange();
    }
    
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision with walls
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!_isCollidingWithWall)
            {
                // Calculate the reflected direction based on the wall's normal
                Vector2 wallNormal = collision.contacts[0].normal;
                Vector2 reflectedDirection = Vector2.Reflect(_direction, wallNormal);

                // Add an upward bias to the reflected direction for better sliding effect
                reflectedDirection += new Vector2(0, 1f);

                // Update the direction and speed
                _direction = reflectedDirection.normalized;
                _isCollidingWithWall = true;
                speed = _originalSpeed * speedMultiplier;

                // Start the sliding coroutine
                StartCoroutine(SlideAlongWall(collision, reflectedDirection));
            }
        }
        if (collision.gameObject.CompareTag("AcidPool"))
        {
            playerStatisEffect.ApplyEffect(playerStatisEffect.Effects.Slow);
            playerStatisEffect.ApplyEffect(playerStatisEffect.Effects.Poison);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Reset collision state when no longer in contact with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            _isCollidingWithWall = false;
        }
        if (collision.gameObject.CompareTag("AcidPool"))
        {
            playerStatisEffect.RemoveEffect(playerStatisEffect.Effects.Slow);
            playerStatisEffect.RemoveEffect(playerStatisEffect.Effects.Poison);
        }
    }
    private IEnumerator SlideAlongWall(Collision2D collision, Vector2 reflectedDirection)
    {
        // Get the EdgeCollider2D component from the collided wall
        EdgeCollider2D edgeCollider = collision.gameObject.GetComponent<EdgeCollider2D>();
        if (edgeCollider != null)
        {
            Vector2 collisionPoint = collision.contacts[0].point;
            Vector2[] points = edgeCollider.points;

            // Transform edge collider points to world space
            Vector2 startPoint = edgeCollider.transform.TransformPoint(points[0]);
            Vector2 endPoint = edgeCollider.transform.TransformPoint(points[1]);

            // Determine the closest point on the edge collider to the collision point
            float distanceToStart = Vector2.Distance(collisionPoint, startPoint);
            float distanceToEnd = Vector2.Distance(collisionPoint, endPoint);

            Vector2 targetPoint = distanceToStart < distanceToEnd ? endPoint : startPoint;
            Vector2 slideDirection = (targetPoint - collisionPoint).normalized;

            // Adjust the reflected direction to avoid moving parallel to the wall
            Vector2 adjustedDirection = Vector2.Lerp(reflectedDirection, slideDirection, 0.5f).normalized;

            // Calculate the sliding duration
            float slideDistance = Vector2.Distance(collisionPoint, targetPoint);
            float slideTime = slideDistance / (speed * speedMultiplier);

            float elapsedTime = 0f;
            while (elapsedTime < slideTime)
            {
                // Move the player along the adjusted direction
                transform.Translate(adjustedDirection * speed * Time.deltaTime);
                elapsedTime += Time.deltaTime;

                // Stop sliding if the player has reached or passed the target point
                if (Vector2.Distance(transform.position, targetPoint) < 0.1f)
                {
                    break;
                }

                yield return null;
            }
        }

        // Reset direction and collision state after sliding
        _direction = Vector2.up;
        _isCollidingWithWall = false;
    }
    void moveWASD()
    {
        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), 0,0);
        transform.position += moveVec * speed * Time.deltaTime;
    }
    void movePointAndClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _target = Camera.ScreenToWorldPoint(Input.mousePosition);
            _target.z = 0;
        }

        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);
        transform.Translate(_direction * speed * Time.deltaTime);
    }
}
