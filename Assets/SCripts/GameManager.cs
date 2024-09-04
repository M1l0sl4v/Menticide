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

    //moves the master object that has all of the relevant items back to zero at a set intervul (resetTriggerDistance)
    public void backToZero(float backtozero)
    {
        all.transform.position = new Vector3(all.transform.position.x, all.transform.position.y - backtozero, all.transform.position.z);
    }
}
