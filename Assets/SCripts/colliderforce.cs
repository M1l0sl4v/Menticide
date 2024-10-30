using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderforce : MonoBehaviour
{
    public float pushforce = 10f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            { Vector3 pushDirection = other.transform.position - transform.position;
                rb.AddForce(pushDirection.normalized * pushforce, ForceMode.Impulse); // Adjust the force value as needed
            }
        }
    }
}
