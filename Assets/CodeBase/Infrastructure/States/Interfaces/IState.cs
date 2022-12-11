namespace CodeBase.Infrastructure.States.Interfaces
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}