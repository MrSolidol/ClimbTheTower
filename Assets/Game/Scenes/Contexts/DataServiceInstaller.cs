using UnityEngine;
using Zenject;

public class DataServiceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DataService>().FromNew().AsSingle().NonLazy();
    }
}