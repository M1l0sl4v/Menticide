using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropAcid : MonoBehaviour
{
    public float AcidRate = .5f;
    public GameObject AcidPools;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpitterShoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpitterShoot()
    {
        yield return new WaitForSeconds(AcidRate);
        Instantiate(AcidPools, transform.position, Quaternion.identity);
        StartCoroutine(SpitterShoot());
    }
}
