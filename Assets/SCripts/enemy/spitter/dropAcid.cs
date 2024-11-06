using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropAcid : MonoBehaviour
{
    public float AcidRate = .5f;
    public GameObject AcidPools;
    private GameObject pool;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpitterShoot());
    }
    IEnumerator SpitterShoot()
    {
        yield return new WaitForSeconds(AcidRate);
        pool = Instantiate(AcidPools, transform.position, Quaternion.identity);
        pool.transform.SetParent(gameObject.transform.parent);
        StartCoroutine(SpitterShoot());
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
