using UnityEngine;

public class CullingCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");
        Destroy(other.gameObject);

        // for reuse mechanic in future implementations 
        // other.gameObject.SetActive(false);

    }
}
