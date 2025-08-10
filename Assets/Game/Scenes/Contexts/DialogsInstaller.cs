using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class DialogsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        List<DialogeInfo> dialogeInfos = Resources.LoadAll<DialogeInfo>("").ToList();
        Dictionary<string, DialogeInfo> dialogeMap = new Dictionary<string, DialogeInfo>();

        foreach (var info in dialogeInfos)
        { dialogeMap[info.NpcName] = info; }

        Container.Bind<Dictionary<string, DialogeInfo>>().FromInstance(dialogeMap).AsSingle().NonLazy();
    }
}
