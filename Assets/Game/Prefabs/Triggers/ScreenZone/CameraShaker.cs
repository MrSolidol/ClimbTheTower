using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private Transform shakeRoot;
    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AnimationCurve amplitudeCurve;
    [SerializeField] private float amplitudeMultiplayer;
    [SerializeField] private float shakeFrequency;
    [SerializeField] private float shakeDuration;

    private CancellationTokenSource token;
    private Task task;


    private void Awake()
    {
        token = new CancellationTokenSource();
    }

    private void OnEnable()
    {
        playerMovement.eDisplayShake.AddListener(ShakeCamera);
    }

    private void OnDisable()
    {
        playerMovement.eDisplayShake.RemoveListener(ShakeCamera);
    }


    private void ShakeCamera(Vector2 vec) 
    {
        token.Cancel();
        token = new CancellationTokenSource();
        task = ShakeProcess(vec, token);
    }

    private async Task ShakeProcess(Vector2 vec, CancellationTokenSource token) 
    {
        try
        {
            float time = 0f;
            float unit = 0f;
            float coeff = 0f;

            while (unit < 1)
            {
                coeff = Mathf.Sin(unit * shakeFrequency)* amplitudeCurve.Evaluate(unit) * amplitudeMultiplayer;
                shakeRoot.localPosition = coeff * vec.normalized * NormalizeAmplitude(vec.magnitude);

                time += Time.deltaTime;
                unit = time / shakeDuration;
                await Task.Yield();
            }

            shakeRoot.localPosition = Vector2.zero;
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Shake cancelled");
        }
    }

    private float NormalizeAmplitude(float input)
    {
        input = Mathf.Clamp(input, 0, 30);
        float normalized = MathF.Pow(input / 30f, 4f);
        return normalized;
    }

}
