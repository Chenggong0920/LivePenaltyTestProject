using UnityEngine;
using Zenject;

public class PenaltyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAPIHandler>().To<APIHandler>().AsSingle();
    }
}