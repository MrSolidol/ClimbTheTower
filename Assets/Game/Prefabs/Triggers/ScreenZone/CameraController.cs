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
    

    public void SetCamera(Vector3 newPosition, float newScale) 
    {
        currentCamera.transform.position = newPosition;
        currentCamera.gameObject.GetComponent<Camera>().orthographicSize = CAMERA_SCALE * newScale;
    }

    public async void MoveCamera(Vector3 newPosition, float newScale) 
    {
        Vector3 _oldPosition = currentCamera.transform.position;
        float _oldScale = currentCamera.gameObject.GetComponent<Camera>().orthographicSize;

        var scaleTask = ChangeScale(newScale * CAMERA_SCALE, _oldScale);
        var posTask = ChangePosition(newPosition, _oldPosition);

        await scaleTask;
        await posTask;
    }

    private async Task ChangeScale(float newScale, float oldScale) 
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

    private async Task ChangePosition(Vector3 newPosition, Vector3 oldPosition) 
    {
        float time = 0f;
        float unit = 0f;

        while (unit < 1)
        {
            currentCamera.transform.position = Vector3.Lerp(oldPosition, newPosition, positionCurve.Evaluate(unit));

            time += Time.deltaTime;
            unit = time / positionDuration;
            await Task.Yield();
        }
    }

}
