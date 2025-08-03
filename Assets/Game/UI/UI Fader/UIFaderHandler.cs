using System;
using UnityEngine;
using UnityEngine.Events;

public class UIFaderHandler : MonoBehaviour
{
    public UnityEvent eFaderShowed;
    public UnityEvent eFaderHided;

    public void HandlerEnterAnimation()
    {
        eFaderShowed?.Invoke();
    }

    public void HandlerExitAnimation()
    {
        eFaderHided?.Invoke();
    }
}
