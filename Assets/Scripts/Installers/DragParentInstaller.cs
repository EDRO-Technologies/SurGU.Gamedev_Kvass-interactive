using Logic.Turrets;
using UnityEngine;
using Zenject;

namespace Installer
{
    public class DragParentInstaller : MonoInstaller
    {
        [SerializeField] private DragParent _instance;
        public override void InstallBindings()
        {
            Container
                .Bind<DragParent>()
                .FromInstance(_instance)
                .AsSingle();
        }
    }
}