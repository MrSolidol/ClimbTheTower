using System.Linq;
using UnityEngine;
using Zenject;

public class ScreensInstaller : MonoInstaller
{
    [SerializeField] private Transform currentCamera;

    public override void InstallBindings()
    {
        var screenZones = FindObjectsOfType<ScreenZone>();

        Container.Bind<ScreenController>().AsSingle().WithArguments(screenZones.ToList(), currentCamera).NonLazy();
    }
}
