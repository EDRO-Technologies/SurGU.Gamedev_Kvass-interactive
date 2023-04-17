using Combat.Turrets;
using Logic.Turrets;
using Zenject;

namespace Installer
{
    public class TurretFactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<TurretFactory>()
                .FromNewComponentOnNewGameObject()
                .AsSingle();
        }
    }
}