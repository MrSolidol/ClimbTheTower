using System.Linq;
using UnityEngine;
using Zenject;

public class ScreensInstaller : MonoInstaller
{
    [SerializeField] CameraController camControll;
    [SerializeField] MusicController musControll;

    public override void InstallBindings()
    {
        var screenZoneList = FindObjectsOfType<ScreenZone>().ToList();

        Container.Bind<ScreenController>().AsSingle().WithArguments(camControll, musControll, screenZoneList).NonLazy();
    }


}
