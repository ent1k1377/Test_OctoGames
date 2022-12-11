using CodeBase.GeneralSystems;
using Zenject;

namespace CodeBase.Installers.ProjectInstallers
{
    public class CursorInstaller : MonoInstaller
    {
        public override void InstallBindings() => 
            Container.Bind<CursorSystem>().AsSingle();
    }
}