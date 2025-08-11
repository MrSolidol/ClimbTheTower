using UnityEngine;
using UnityEngine.Events;

public abstract class InputController : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Vector2> ePressOn;
    [HideInInspector] public UnityEvent<Vector2> ePress;
    [HideInInspector] public UnityEvent<Vector2> ePressUp;


    public abstract void PressOn();

    public abstract void Press();

    public abstract void PressUp();
}
