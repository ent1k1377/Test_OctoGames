using CodeBase.Infrastructure;
using CodeBase.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers.ProjectInstallers
{
    public class GameStateMachineInstaller : MonoInstaller, IInitializable, ICoroutine
    {
        [SerializeField] private LoadingCurtain _loadingCurtainInstallerPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameStateMachineInstaller>().FromInstance(this).AsSingle();
            
            var loadingCurtainInstance = 
                Container.InstantiatePrefabForComponent<LoadingCurtain>(_loadingCurtainInstallerPrefab);
            
            Container.Bind<LoadingCurtain>().FromInstance(loadingCurtainInstance).AsSingle().NonLazy();
            Container.Bind<SceneLoader>().AsSingle().WithArguments(this).NonLazy();
        }

        public void Initialize()
        {
            Container.Bind<GameStateMachine>().AsSingle();
            Container.Resolve<GameStateMachine>().Enter<BootstrapState>();
        }
    }
}