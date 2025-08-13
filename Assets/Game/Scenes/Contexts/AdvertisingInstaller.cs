using System.Linq;
using System.Collections.Generic;
using Zenject;

public class AdvertisingInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        List<PlayerAnimations> playerAnimations = FindObjectsOfType<PlayerAnimations>(includeInactive: true).ToList();

        if (playerAnimations.Count != 1) 
        { UnityEngine.Debug.Log("Player Animation binding error!"); return; }

        Container.Bind<PlayerAnimations>().FromInstance(playerAnimations[0]).AsSingle();
    }
}
