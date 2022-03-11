using Zenject;


public class ProjectContextInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IUserProfile>().To<UserProfile>().AsSingle();
    }
}
