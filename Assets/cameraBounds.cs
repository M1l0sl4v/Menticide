using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public Transform target;
    public float minX, maxX;       
    private Camera cam;
    private float camHalfWidth;
    public float smoothSpeed;

    void Start()
    {
        cam = GetComponent<Camera>();
        camHalfWidth = cam.orthographicSize * cam.aspect;
    }

    void FixedUpdate()
    {
            Vector3 targetPosition = target.position;
            float clampedX = Mathf.Clamp(targetPosition.x, minX + camHalfWidth, maxX - camHalfWidth);
            Vector3 desiredPosition = new Vector3(clampedX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}