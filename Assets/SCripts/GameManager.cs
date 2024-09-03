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

    public void backToZero()
    {
        all.transform.position = Vector3.zero;
        foreach (Transform child in all.transform)
        {
            child.localPosition = Vector3.zero;
        }
    }
}
