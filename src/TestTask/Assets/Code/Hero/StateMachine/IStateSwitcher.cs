using Code.Hero.StateMachine.States;

namespace Code.Hero.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
    }
}