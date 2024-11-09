using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    public float LifeSpan=2;
    private void Start()
    {
        Destroy(gameObject, LifeSpan);
    }
}
