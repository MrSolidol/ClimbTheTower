using System.Linq;
using UnityEngine;
using Zenject;

public class PauseServiceInstaller : MonoInstaller
{
    [SerializeField] private PauseService pauseService;

    public override void InstallBindings()
    {
        Container.Bind<PauseService>().FromInstance(pauseService).AsSingle().NonLazy();
    }
}
