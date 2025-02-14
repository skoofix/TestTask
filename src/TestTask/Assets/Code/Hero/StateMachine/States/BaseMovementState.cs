using Code.Infrastructure;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.StateMachine.States
{
    public abstract class BaseMovementState : IUpdatableState
    {
        protected readonly HeroStateMachine StateSwitcher;
        protected readonly IInputService InputService;
        protected readonly Rigidbody Rigidbody;
        protected readonly float MovementSpeed;
        protected readonly Camera Camera;

        protected BaseMovementState(HeroStateMachine stateSwitcher, IInputService inputService, float movementSpeed, Rigidbody rigidbody)
        {
            StateSwitcher = stateSwitcher;
            InputService = inputService;
            Rigidbody = rigidbody;
            MovementSpeed = movementSpeed;
            Camera = Camera.main;
        }
        
        public virtual void Enter() {}
        public virtual void Exit() {}
        public virtual void Update() {}

        protected void CheckForJump()
        {
            if (InputService.IsJumpButtonDown())
                StateSwitcher.SwitchState<JumpState>();
        }
        protected void CheckForMove()
        {
            if (InputService.HasAxisInput())
                StateSwitcher.SwitchState<MoveState>();
        }
        
        protected void CheckForIdleState()
        {
            if (!InputService.HasAxisInput())
                StateSwitcher.SwitchState<IdleState>();
        }
    }
}