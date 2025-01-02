using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderforce : MonoBehaviour
{
    public float pushforce = 10f;
    
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            Vector3 pushDirection = other.transform.position - transform.position; 
            rb.AddForce(pushDirection.normalized * pushforce, ForceMode.Impulse); // Adjust the force value as needed
            Debug.Log("player touched");
        }
    }
}
