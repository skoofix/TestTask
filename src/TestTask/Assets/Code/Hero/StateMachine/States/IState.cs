namespace Code.Hero.StateMachine.States
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
    }
}