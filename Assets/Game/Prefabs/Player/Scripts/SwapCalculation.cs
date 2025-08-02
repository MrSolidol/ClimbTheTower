using UnityEngine;
using UnityEngine.Events;

public class SwapCalculation : MonoBehaviour, IControlable
{
    [HideInInspector] public UnityEvent<Vector2> eSwapStarted;
    [HideInInspector] public UnityEvent<Vector2, float, bool> eSwapContinued;
    [HideInInspector] public UnityEvent<Vector2, float, bool> eSwapEnded;

    [HideInInspector] public Vector2 Difference { get; private set; } = Vector2.zero;
    [HideInInspector] public bool IsEnabled { get; set; } = true;
    [HideInInspector] public bool IsSwapped { get; private set; } = false;
    [HideInInspector] public bool IsDifferenceEnough { get; private set; } = false;
    [HideInInspector] public bool IsDegreesAllowed { get; private set; } = false;
    [HideInInspector] public bool IsFullHorizon { get; set; } = false;

    [SerializeField] private float maxDegrees = 70;
    [SerializeField] private float maxSwapDistance = 3.5f;
    [SerializeField] private float minSwapDistance = 1f;

    private Vector2 startVec = Vector2.zero;

    public void PressOn(Vector2 vec)
    {
        if (!IsEnabled) { return; }

        startVec = vec;

        Difference = Vector2.zero;

        eSwapStarted?.Invoke(startVec);
    }

    public void Press(Vector2 vec)
    {
        if (!IsEnabled) { return; }

        Difference = vec - startVec;

        ÑînditionsCheck(Difference);
    }

    public void PressUp(Vector2 vec)
    {
        if (!IsEnabled) { return; }

        if (IsDegreesAllowed && IsDifferenceEnough)
        {
            float value = (Mathf.Clamp(Difference.magnitude, minSwapDistance, maxSwapDistance) - minSwapDistance) / (maxSwapDistance - minSwapDistance);

            eSwapEnded.Invoke(Difference.normalized, value, true);
            IsFullHorizon = false;
        }

        Difference = Vector2.zero;
        startVec = Vector2.zero;
    }

    private void ÑînditionsCheck(Vector2 vec)
    {
        IsDegreesAllowed = ((Vector2.Angle(vec, Vector2.down) < maxDegrees) || IsFullHorizon) ? true : false;
        IsDifferenceEnough = (vec.magnitude > minSwapDistance) ? true : false;

        float value = (Mathf.Clamp(Difference.magnitude, minSwapDistance, maxSwapDistance) - minSwapDistance) / (maxSwapDistance - minSwapDistance);

        eSwapContinued?.Invoke(Difference.normalized, value, IsDegreesAllowed && IsDifferenceEnough);

    }

}
