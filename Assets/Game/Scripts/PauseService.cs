using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class PauseService : MonoBehaviour
{
    [HideInInspector] public UnityEvent<bool> eGameStopped;
    [HideInInspector] public UnityEvent<bool> eInterfaceLocked;

    public bool IsGameStopped { get; private set; } = false;
    public bool IsInterfaceLocked { get; private set; } = false;


    [Inject]
    public void Construct() {}


    public void StopGame() 
    {
        if (IsGameStopped) { return; }
        IsGameStopped = true;
        eGameStopped?.Invoke(true);
    }

    public void ResumeGame() 
    {
        if (!IsGameStopped) { return; }
        IsGameStopped = false;
        eGameStopped?.Invoke(false);
    }

    public void LockInterface() 
    {
        if (IsInterfaceLocked) { return; }
        IsInterfaceLocked = true;
        eInterfaceLocked?.Invoke(true);
    }

    public void UnlockInterface() 
    {
        if (!IsInterfaceLocked) { return; }
        IsInterfaceLocked = false;
        eInterfaceLocked?.Invoke(false);
    }

}
