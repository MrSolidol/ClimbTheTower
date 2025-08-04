using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ScreensInstaller : MonoInstaller
{
    [SerializeField] private Transform currentCamera;
    [SerializeField] private GameObject musicPlayer;

    public override void InstallBindings()
    {
        var screenZoneList = FindObjectsOfType<ScreenZone>().ToList();
        var musicInfoList = Resources.LoadAll<MusicInfo>("").ToList();

        Container.Bind<ScreenController>().AsSingle().WithArguments(currentCamera, musicPlayer, screenZoneList, musicInfoList).NonLazy();
    }
}
