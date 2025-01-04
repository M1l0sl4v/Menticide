using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ironMadion : MonoBehaviour
{
    Animator animator;
    public GameObject[] spikes;
    public float growSpeed;
    public float growRate;
    public Vector3 targetScale = new Vector3(0f, 5f, 0f);
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
}
