using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.StateMachine.States
{
    public class IdleState : BaseMovementState
    {
        public IdleState(HeroStateMachine stateSwitcher, IInputService inputService, float movementSpeed, Rigidbody rigidbody) : base(stateSwitcher, inputService, movementSpeed, rigidbody) {}

        public override void Enter() {}

        public override void Exit() {}

        public override void Update()
        {
            CheckForMove();
            CheckForJump();
        }
    }
}