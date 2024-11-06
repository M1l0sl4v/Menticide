using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public GameObject drawBarPrefab;
    public Material wallMaterial;
    public float wallWidth = 0.3f;
    public string wallTag = "Wall";
    public AudioClip drawingSoundClip; // Assign an audio clip in the Inspector
    public float lineLifetime = 5f; // Time in seconds before the line is destroyed

    private LineRenderer currentLine;
    private Vector3 initialPoint;
    private AudioSource audioSource;
    public AudioMixerGroup lineSoundGroup;

    private PauseMenu pauseMenu;

    private List<Vector3> linePositions = new List<Vector3>();
    public float segments = 10f;
    private float drawBarMax = 1f;
    private float drawBarcurrent;
    private Image drawBarImage;
    private bool fillingUp = false;

    public GameObject swipeObject;



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

        pauseMenu = FindAnyObjectByType<PauseMenu>();
        
        drawBarcurrent = drawBarMax;
        drawBarImage =  drawBarPrefab.GetComponentInChildren<Image>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !pauseMenu.paused && drawBarcurrent>=0 && fillingUp == false)
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
        else if (drawBarcurrent < drawBarMax)
            {
                drawBarcurrent+=0.01f;
                updateDrawBar();
                if (drawBarcurrent <= 0.01f)
                {
                    fillingUp = true;
                }
                else if (drawBarcurrent >= drawBarMax -0.03f)
                {
                    fillingUp = false;
                }
        }
        if (Input.GetMouseButtonDown(1) && !pauseMenu.paused)
        {
            StartSwipeing();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopSwipeing();
        }
        if (swipeObject.activeSelf == true)
        {
            ContinueSwipeing();
        }
    }
    void StartSwipeing() {
        swipeObject.SetActive(true);
    }
    void StopSwipeing()
    {
        swipeObject.SetActive(false);
    }
    void ContinueSwipeing()
    {
        swipeObject.transform.position = GetMousePosWithZ();
    }

    void updateDrawBar()
    {
        drawBarImage.fillAmount = drawBarcurrent;
    }

    void StartDrawing()
    {
        GameObject lineObject = new GameObject("Line");
        currentLine = lineObject.AddComponent<LineRenderer>();

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
        linePositions.Clear();
        linePositions.Add(initialPoint);
        linePositions.Add(GetMousePosWithZ());

        currentLine.SetPosition(0, initialPoint);
        currentLine.SetPosition(1, GetMousePosWithZ());

        // Play the drawing sound
        audioSource.Play();
    }

    void ContinueDrawing()
    {
        updateDrawBar();
        // Update the second point of the line to follow the mouse position
        Vector3 mousePos = GetMousePosWithZ();
        if (linePositions.Count < 2 || Vector3.Distance(linePositions[linePositions.Count - 2], mousePos) > 1)
        {
            linePositions.Insert(linePositions.Count - 1, mousePos);
            List<Vector3> smoothLinePositions = makeLineSmoth(linePositions.GetRange(0, linePositions.Count - 1));
            smoothLinePositions.Add(mousePos);

            currentLine.positionCount = smoothLinePositions.Count;
            currentLine.SetPositions(smoothLinePositions.ToArray());
            drawBarcurrent-=0.1f;
        }
        currentLine.SetPosition(currentLine.positionCount - 1, mousePos);
        if(drawBarcurrent <= 0)
        {
            StopDrawing();
        }
    }

    void StopDrawing()
    {
        if (currentLine != null)
        {
            Vector3 mousePos = GetMousePosWithZ();
            linePositions.Insert(linePositions.Count - 1, mousePos);
            List<Vector3> smoothLinePositions = makeLineSmoth(linePositions.GetRange(0, linePositions.Count - 1));
            smoothLinePositions.Add(mousePos);

            currentLine.positionCount = smoothLinePositions.Count;
            currentLine.SetPositions(smoothLinePositions.ToArray());
            // Stop playing the drawing sound
            audioSource.Stop();
            //remve mouse line
            if (linePositions.Count > 1)
            {
                linePositions.RemoveAt(linePositions.Count - 1);
            }
            // Finalize the line to be straight between initial and final points
            smoothLinePositions = makeLineSmoth(linePositions);
            currentLine.positionCount = smoothLinePositions.Count;
            currentLine.SetPositions(smoothLinePositions.ToArray());

            // Add collider to drawn line
            EdgeCollider2D edgeCollider = currentLine.gameObject.AddComponent<EdgeCollider2D>();
            List<Vector2> colliderPoints = new List<Vector2>();
            foreach (var pos in smoothLinePositions)
            {
                colliderPoints.Add(new Vector2(pos.x, pos.y));
            }
            edgeCollider.points = colliderPoints.ToArray();
            currentLine.gameObject.layer = LayerMask.NameToLayer(wallTag);

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
    List<Vector3> makeLineSmoth(List<Vector3> points)
    {
        List<Vector3> smoothedPoints = new List<Vector3>();
        if (points.Count < 3) return points;
        for (int i = 0; i < points.Count - 2; i += 2)
        {
            Vector3 p0 = points[i];
            Vector3 p1 = points[i + 1];
            Vector3 p2 = points[i + 2];
            for (int x = 0; x <= segments; x += 1)
            {
                float t = x / segments;
                Vector3 curvePoint = Mathf.Pow(1 - t, 2) * p0 + 2 * (1 - t) * t * p1 + Mathf.Pow(t, 2) * p2;
                smoothedPoints.Add(curvePoint);
            }
        }
        smoothedPoints.Add(points[points.Count - 1]);
        return smoothedPoints;
    }
}