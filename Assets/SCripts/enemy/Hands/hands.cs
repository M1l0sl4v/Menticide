using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class hands : MonoBehaviour
{
    public static hands instance;
    public float speed;
    private Vector3 endPos;
    public float moveSmooth;
    private bool active;
    private float initDistanceFromPlayer;
    [SerializeField] private AudioClip slam;

    public float backOffDistance;
    public float backOffDuration;
    public float chaseCoolDown;

    private bool isBackingOff;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        initDistanceFromPlayer = Vector2.Distance(playermovement.instance.transform.position, transform.position);
        if (!Tutorial.startWithTutorial) active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            endPos = playermovement.instance.transform.position;
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime*moveSmooth);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playermovement.instance.TakeDamage(1);
            StartCoroutine(backOffRoutine());
        }
    }

    public void Activate()
    {
        Vector3 playerPos = playermovement.instance.transform.position;
        transform.position = new Vector3(playerPos.x, playerPos.y - initDistanceFromPlayer, playerPos.z);
        active = true;
    }

    private IEnumerator backOffRoutine()
    {
        isBackingOff = true;
        
        Vector3 directionAwayFromPlayer = (transform.position - playermovement.instance.transform.position).normalized;
        Vector3 backOffPositon = transform.position + directionAwayFromPlayer * backOffDistance;

        float timer = 0;
        while (timer < backOffDuration)
        {
            transform.position = Vector3.Lerp(transform.position, backOffPositon, Time.deltaTime * moveSmooth);
            timer += Time.deltaTime;
            yield return null;
        }
        
        yield return new WaitForSeconds(chaseCoolDown);
        
        isBackingOff = false;
    }

    public void handSlam()
    {
        AudioManager.instance.enemyFX(slam, transform ,.7f, 1);
    }

    public void leftHandSlam()
    {
        
    }

    public void rightHandSlam()
    {
        
    }  
    
}