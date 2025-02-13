using Code.Hero.StateMachine.States;
using UnityEngine;

namespace Code.Hero.StateMachine
{
    public interface IStateSwitcher
    {
        void SwitchState<T>() where T : IState;
        void SwitchState<T>(Vector3 target = default) where T : IState;
    }
}