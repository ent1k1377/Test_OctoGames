using CodeBase.Infrastructure.States.Interfaces;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string _initial = "Initial";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        
        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _sceneLoader.Load(_initial, EnterLoadLevel);
        }

        public void Exit()
        {
        }
        
        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>("Main");
    }
}