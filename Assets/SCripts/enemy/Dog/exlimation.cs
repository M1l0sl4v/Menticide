using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exlimation : MonoBehaviour
{
    public float lifeTime = 0.01f;

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}