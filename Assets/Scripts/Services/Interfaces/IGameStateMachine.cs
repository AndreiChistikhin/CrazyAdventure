public interface IGameStateMachine : IService
{
    void Enter<T>() where T : IState;
}