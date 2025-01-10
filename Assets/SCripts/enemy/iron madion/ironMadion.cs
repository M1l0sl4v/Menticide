using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ironMadion : MonoBehaviour
{
    Animator animator;
    public GameObject[] spikes;
    public float growSpeed;
    public float growRate;
    public Vector3 targetScale = new Vector3(0f, 5f, 0f);
    public AudioClip open;
    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(growSpike());
    }
    private void OnEnable()
    {
        animator.SetTrigger("openIronMadion");
        StartCoroutine(growSpike());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        foreach (GameObject spike in spikes)
        {
            spike.transform.localScale = new Vector3(.3f, 0f, 1f);
        }
    }
    IEnumerator growSpike()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            foreach (GameObject spike in spikes)
            {
                spike.transform.localScale = new Vector3(.3f, Mathf.Lerp(spike.transform.localScale.y, targetScale.y, Time.deltaTime * growRate),1f);
            }
            yield return new WaitForSeconds(growSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("cullingField"))
        {
            Destroy(gameObject);
        }
    }

    public void openSound()
    {
        float pitch = Random.Range(.7f, 1f);
        AudioManager.instance.enemyFX(open, transform,1f,pitch);
    }
}
