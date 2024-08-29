using System.Collections;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public Material wallMaterial;
    public float wallWidth = 0.3f;
    public string wallTag = "Wall";
    public AudioClip drawingSoundClip; // Assign an audio clip in the Inspector
    public float lineLifetime = 5f; // Time in seconds before the line is destroyed

    private LineRenderer currentLine;
    private Vector3 initialPoint;
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = drawingSoundClip;
        audioSource.loop = true; // Loop the audio while drawing
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }

        if (currentLine != null)
        {
            ContinueDrawing();
        }
    }

    void StartDrawing()
    {
        GameObject lineObject = new GameObject("Line");
        currentLine = lineObject.AddComponent<LineRenderer>();
        Debug.Log("Started Drawing");

        // Set the order in layer
        currentLine.sortingOrder = 6;

        currentLine.material = wallMaterial;
        currentLine.startWidth = wallWidth;
        currentLine.endWidth = wallWidth;
        currentLine.textureMode = LineTextureMode.Tile;

        lineObject.tag = wallTag;

        // Set the initial point of the line
        initialPoint = GetMousePosWithZ();
        currentLine.positionCount = 2;
        currentLine.SetPosition(0, initialPoint);
        currentLine.SetPosition(1, initialPoint);

        // Play the drawing sound
        audioSource.Play();
    }

    void ContinueDrawing()
    {
        // Update the second point of the line to follow the mouse position
        Vector3 mousePos = GetMousePosWithZ();
        currentLine.SetPosition(1, mousePos);
    }

    void StopDrawing()
    {
        if (currentLine != null)
        {
            // Stop playing the drawing sound
            audioSource.Stop();

            // Finalize the line to be straight between initial and final points
            Vector3 finalPoint = GetMousePosWithZ();
            currentLine.SetPosition(1, finalPoint);

            // Add collider to drawn line
            EdgeCollider2D edgeCollider = currentLine.gameObject.AddComponent<EdgeCollider2D>();
            edgeCollider.points = new Vector2[] { (Vector2)initialPoint, (Vector2)finalPoint };

            // Start the coroutine to destroy the line after a delay
            StartCoroutine(DestroyLineAfterDelay(currentLine.gameObject, lineLifetime));

            currentLine = null;
        }
    }

    IEnumerator DestroyLineAfterDelay(GameObject lineObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(lineObject);
    }

    Vector3 GetMousePosWithZ()
    {
        // Set the z-coordinate to the desired wall height
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
}
