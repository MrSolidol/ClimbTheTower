using UnityEngine;
using UnityEngine.Events;

public class UIPopupHandler : MonoBehaviour
{
    public UnityEvent ePopupShowed;
    public UnityEvent ePopupHided;

    public void HandlerAppearAnimation()
    {
        ePopupShowed?.Invoke();
    }

    public void HandlerDisappearAnimation()
    {
        ePopupHided?.Invoke();
    }

}
