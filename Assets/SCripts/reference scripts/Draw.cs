using System.Collections.Generic;
using UnityEngine;

public class WallDrawing : MonoBehaviour
{
    public Material wallMaterial;
    public float wallWidth = 0.3f;
    public string wallTag = "wall";
    public AudioClip drawingSoundClip; // Assign an audio clip in the Inspector

    private LineRenderer currentLine;
    private List<Vector3> linePositions = new List<Vector3>();
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

        lineObject.tag = wallTag;

        // Initialize the list of line positions
        linePositions.Clear();
        linePositions.Add(GetMousePosWithZ());

        // Play the drawing sound
        audioSource.Play();
    }

    void ContinueDrawing()
    {
        // Extend the line renderer based on mouse position
        Vector3 mousePos = GetMousePosWithZ();

        // Check the distance from the last point
        float minDistance = 0.1f; // Adjust the minimum distance as needed

        if (Vector3.Distance(linePositions[linePositions.Count - 1], mousePos) > minDistance)
        {
            linePositions.Add(mousePos);

            currentLine.positionCount = linePositions.Count;
            currentLine.SetPositions(linePositions.ToArray());
        }
    }

    void StopDrawing()
    {
        if (currentLine != null)
        {
            // Stop playing the drawing sound
            audioSource.Stop();

            // Add collider to drawn line
            EdgeCollider2D edgeCollider = currentLine.gameObject.AddComponent<EdgeCollider2D>();
            edgeCollider.points = ConvertToVector2Array(linePositions);

            currentLine = null;
        }
    }

    Vector3 GetMousePosWithZ()
    {
        // Set the z-coordinate to the desired wall height
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;
        return mousePos;
    }

    Vector2[] ConvertToVector2Array(List<Vector3> vector3List)
    {
        Vector2[] vector2Array = new Vector2[vector3List.Count];
        for (int i = 0; i < vector3List.Count; i++)
        {
            vector2Array[i] = vector3List[i];
        }
        return vector2Array;
    }
}
