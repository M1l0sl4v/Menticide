using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironMadion : MonoBehaviour
{
    Animator animator;
    public GameObject[] spikes;
    public float growSpeed;
    public Vector3 targetScale = new Vector3(1f, 5f, 1f);
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
    }
    IEnumerator growSpike()
    {
        while (true)
        {
            foreach (GameObject spike in spikes)
            {
                spike.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, Time.deltaTime * growSpeed);
            }
        }
    }
}
