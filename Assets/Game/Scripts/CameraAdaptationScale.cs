using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdaptationScale : MonoBehaviour
{
    [SerializeField] private AnimationCurve cameraCurve;

    [SerializeField] private float alternativePositionY = 2f;
    [SerializeField] private float alternativeScale = 2.9f;

    [HideInInspector] public float originalScale = 5f;

    private Camera mainCamera;
    private Vector2 lastScreenSize;

    private float originalPositionY = 0f;
    
    private float targetWidth = 1920f;
    private float targetHeight = 1080f;
    

    private void Awake()
    {
        mainCamera = Camera.main;
        lastScreenSize = new Vector2(Screen.width, Screen.height);
        UpdateCamera();
    }

    private void Update()
    { 
        Vector2 currentScreenSize = new Vector2(Screen.width, Screen.height);

        if (currentScreenSize != lastScreenSize)
        {
            lastScreenSize = currentScreenSize;
            UpdateCamera();
        }
    }

    private void UpdateCamera()
    {
        float targetAspect = (float)targetWidth / (float)targetHeight;
        float currentAspect = (float)Screen.width / Screen.height;

        float unit = (3f - Mathf.Clamp(currentAspect, 1.78f, 3f)) / (3f - 1.78f);
        mainCamera.transform.position = new Vector3 (0f, Mathf.Lerp(originalPositionY, alternativePositionY, cameraCurve.Evaluate(1 - unit)), -10f);
        mainCamera.orthographicSize = Mathf.Lerp(originalScale, alternativeScale, cameraCurve.Evaluate(1 - unit));
    }
}
