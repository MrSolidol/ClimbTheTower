using UnityEngine;
using UnityEngine.Events;

public class UIPopupHandler : MonoBehaviour
{
    [HideInInspector] public UnityEvent ePopupShowed;
    [HideInInspector] public UnityEvent ePopupHided;

    public void HandlerAppearAnimation()
    {
        ePopupShowed?.Invoke();
    }

    public void HandlerDisappearAnimation()
    {
        ePopupHided?.Invoke();
    }

}
