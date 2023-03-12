using States;

namespace Services.Interfaces
{
    public interface IGameStateMachine : IService
    {
        void Enter<T>() where T : IState;
    }
}