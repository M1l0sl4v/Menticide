using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPool : MonoBehaviour
{
    public int HowMuchDamage = 1;
    public int DamageTickRate = 1;
    public float LifeSpan=2;
    private void Start()
    {
        Destroy(gameObject, LifeSpan);
    }
}
