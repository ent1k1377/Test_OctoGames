using CodeBase.Infrastructure.States.Interfaces;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    { 
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader; 
        private readonly LoadingCurtain _loadingCurtain;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _sceneLoader.Load(sceneName, OnLoaded);
            _loadingCurtain.Show();
        }

        public void Exit() => 
            _loadingCurtain.Hide();

        private void OnLoaded() => 
            _stateMachine.Enter<GameLoopState>();
    }
}