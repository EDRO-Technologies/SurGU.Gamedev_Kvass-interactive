using Zenject;

namespace Installer
{
    public class ActionMapsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            ActionMaps instance = new ActionMaps();

            instance.Enable();

            Container
                .Bind<ActionMaps>()
                .FromInstance(instance)
                .AsSingle();
        }
    }
}