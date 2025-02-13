using System.Collections.Generic;
using System.Linq;
using Code.Hero.StateMachine.States;
using UnityEngine;

namespace Code.Hero.StateMachine
{
    public class UnitStateMachine : IStateSwitcher
    {
        private readonly List<IState> _states;
        private IState _currentState;

        public UnitStateMachine()
        {
            _states = new List<IState>()
            {
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);
            
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void SwitchState<T>(Vector3 target = default) where T : IState
        {
            IState state = _states.FirstOrDefault(state => state is T);
            
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => _currentState.Update();
    }
}