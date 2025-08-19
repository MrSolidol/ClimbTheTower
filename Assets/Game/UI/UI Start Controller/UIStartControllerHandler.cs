using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class UIStartControllerHandler : MonoBehaviour
{
    public UnityEvent ePlaySound;

    [Inject] private SceneLoader sceneLoader;

    [SerializeField] private string nextSceneName;

    public void HandlerStartSound()
    {
        ePlaySound?.Invoke();
    }

    public void HandlerStartAnimation() 
    {
        sceneLoader.LoadSceneWithFade(nextSceneName);
    }
}
