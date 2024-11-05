using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingWheel : MonoBehaviour
{
    public float rotateSpeed;
    public bool active;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active) transform.Rotate(Vector2.up, rotateSpeed);
    }
}
