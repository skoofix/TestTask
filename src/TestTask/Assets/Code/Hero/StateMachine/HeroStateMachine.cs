using System.Collections.Generic;
using System.Linq;
using Code.Hero.StateMachine.States;
using Code.Infrastructure;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.StateMachine
{
    public class HeroStateMachine
    {
        private readonly List<IUpdatableState> _states;
        private IUpdatableState _currentState;

        public HeroStateMachine(IInputService inputService, float movementSpeed,float jumpForce, HeroAnimator animator, Rigidbody rigidbody)
        {
            _states = new List<IUpdatableState>()
            {
                new IdleState(this, inputService, movementSpeed, rigidbody),
                new MoveState(this, inputService, movementSpeed, rigidbody),
                new JumpState(this, inputService, movementSpeed, jumpForce, animator, rigidbody)
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IUpdatableState
        {
            IUpdatableState state = _states.FirstOrDefault(state => state is T);
            
            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }

        public void Update() => 
            _currentState.Update();
    }
}