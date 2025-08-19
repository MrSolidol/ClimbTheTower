using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float CAMERA_SCALE = 6.1875f;


    [SerializeField] private Camera currentCamera;

    [SerializeField] private AnimationCurve positionCurve;
    [SerializeField] private float positionDuration = 1;

    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float scaleDuration = 1;

    [SerializeField] private AnimationCurve aspectCurve;

    private float widthAspect = 3f;
    private float widthScale = 5.6f;

    private float heightAspect = 1f;
    private float heightScale = 10f;

    private float currentScale;

    private CancellationTokenSource token;


    private void Awake()
    {
        token = new CancellationTokenSource();
        widthScale = widthScale / CAMERA_SCALE;
        heightScale = heightScale/ CAMERA_SCALE;
    }

    private void Update()
    {
        UpdateCameraScale();
    }


    public void SetCamera(Vector3 newPosition, float newScale) 
    {
        transform.position = newPosition;
        currentScale = newScale;
        UpdateCameraScale();
    }

    public async void MoveCamera(Vector3 newPosition, float newScale) 
    {
        Vector3 _oldPosition = transform.position;
        float _oldScale = currentCamera.gameObject.GetComponent<Camera>().orthographicSize / (GetAspectMultiplayer() * CAMERA_SCALE);

        token.Cancel();
        token = new CancellationTokenSource();

        var scaleTask = ChangeScale(newScale, _oldScale, token);
        var posTask = ChangePosition(newPosition, _oldPosition, token);

        await scaleTask;
        await posTask;
    }

    private async Task ChangeScale(float newScale, float oldScale, CancellationTokenSource token) 
    {
        try
        {
            float time = 0f;
            float unit = 0f;

            while (unit < 1)
            {
                currentScale = Mathf.Lerp(oldScale, newScale, scaleCurve.Evaluate(unit));

                time += Time.deltaTime;
                unit = time / scaleDuration;
                await Task.Yield();
            }

            currentScale = newScale;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Camera scale change cancelled");
        }
    }

    private async Task ChangePosition(Vector3 newPosition, Vector3 oldPosition, CancellationTokenSource token) 
    {
        try
        {
            float time = 0f;
            float unit = 0f;

            while (unit < 1)
            {
                transform.position = Vector3.Lerp(oldPosition, newPosition, positionCurve.Evaluate(unit));

                time += Time.deltaTime;
                unit = time / positionDuration;
                await Task.Yield();
            }

            transform.position = newPosition;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Camera position change cancelled");
        }
    }

    private void UpdateCameraScale() 
    {
        currentCamera.gameObject.GetComponent<Camera>().orthographicSize = currentScale * CAMERA_SCALE * GetAspectMultiplayer();
    }

    private float GetAspectMultiplayer() 
    {
        float currentAspect = (float)Screen.width / Screen.height;
        float unit = (widthAspect - Mathf.Clamp(currentAspect, heightAspect, widthAspect)) / (widthAspect - heightAspect);
        return Mathf.Lerp(widthScale, heightScale, aspectCurve.Evaluate(unit));
    }
}
