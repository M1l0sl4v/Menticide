using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour

{ 
    public static GameManager instance;
    public GameObject all;
    void Start()
    {
        instance = this;
    }

    public void backToZero(float backtozero)
    {
        all.transform.position = new Vector3(all.transform.position.x, all.transform.position.y - backtozero, all.transform.position.z);
    }
}
