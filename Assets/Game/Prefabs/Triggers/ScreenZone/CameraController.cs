using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float CAMERA_SCALE = 6.1875f;

    [SerializeField] private Camera currentCamera;

    [SerializeField] private AnimationCurve positionCurve;
    [SerializeField] private float positionDuration = 1;

    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float scaleDuration = 1;

    private CancellationTokenSource token;


    private void Awake()
    {
        token = new CancellationTokenSource();
    }


    public void SetCamera(Vector3 newPosition, float newScale) 
    {
        transform.position = newPosition;
        currentCamera.gameObject.GetComponent<Camera>().orthographicSize = CAMERA_SCALE * newScale;
    }

    public async void MoveCamera(Vector3 newPosition, float newScale) 
    {
        Vector3 _oldPosition = transform.position;
        float _oldScale = currentCamera.gameObject.GetComponent<Camera>().orthographicSize;

        token.Cancel();
        token = new CancellationTokenSource();

        var scaleTask = ChangeScale(newScale * CAMERA_SCALE, _oldScale, token);
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
                currentCamera.gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(oldScale, newScale, scaleCurve.Evaluate(unit));

                time += Time.deltaTime;
                unit = time / scaleDuration;
                await Task.Yield();
            }

            currentCamera.gameObject.GetComponent<Camera>().orthographicSize = newScale;
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

}
