using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class UIStartControllerHandler : MonoBehaviour
{
    public UnityEvent ePlaySound;

    [Inject] private SceneLoader sceneLoader;

    public void HandlerStartSound()
    {
        ePlaySound?.Invoke();
    }

    public void HandlerStartAnimation() 
    {
        sceneLoader.LoadSceneWithFade("MainMenu");
    }
}
