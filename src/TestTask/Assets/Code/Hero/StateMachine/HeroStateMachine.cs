using System.Collections.Generic;
using System.Linq;
using Code.Hero.StateMachine.States;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.StateMachine
{
    public class HeroStateMachine : IStateSwitcher
    {
        private readonly List<IState> _states;
        private IState _currentState;

        public HeroStateMachine(IInputService inputService, float movementSpeed, HeroAnimator animator, Rigidbody rigidbody)
        {
            _states = new List<IState>()
            {
                new IdleState(this, inputService, movementSpeed, rigidbody),
                new MoveState(this, inputService, movementSpeed, rigidbody),
                new JumpState(this, inputService, movementSpeed, animator, rigidbody)
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

        public void Update() => _currentState.Update();
    }
}