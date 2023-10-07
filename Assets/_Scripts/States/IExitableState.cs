namespace _Scripts.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IExitableState
    {
        void Exit();
    }

    public interface IPayloadedState<TParam> : IExitableState
    {
        void Enter(TParam payload);
    }
}