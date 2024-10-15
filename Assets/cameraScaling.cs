using UnityEngine;
using UnityEngine.U2D; 

public class DynamicCameraScaler : MonoBehaviour
{
    public PixelPerfectCamera pixelPerfectCamera;

    // Set the target reference resolution, same as your Reference Resolution in PixelPerfectCamera
    public int targetWidth = 279;
    public int targetHeight = 480;

    void Start()
    {
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        float targetAspect = (float)targetWidth / targetHeight;  // Aspect ratio of your game (e.g., 1920x1080)
        float screenAspect = (float)Screen.width / Screen.height; // Aspect ratio of the device's screen

        // Calculate the scaling factor to maintain the correct size
        float scalingFactor = targetAspect / screenAspect;

        // Adjust orthographic size of the camera based on the scaling factor
        Camera.main.orthographicSize = pixelPerfectCamera.refResolutionY / 2f * scalingFactor;
    }

    void Update()
    {
        // Continuously check for screen size changes (optional)
        if (Screen.width != pixelPerfectCamera.refResolutionX || Screen.height != pixelPerfectCamera.refResolutionY)
        {
            AdjustCameraSize();
        }
    }
}