using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.StateMachine.States
{
    public class JumpState : BaseMovementState
    {
        private readonly HeroAnimator _animator;
        private bool _jumpAvailable = true;
        
        public JumpState(IStateSwitcher stateSwitcher, IInputService inputService, float movementSpeed, HeroAnimator animator, Rigidbody rigidbody) : base(stateSwitcher, inputService, movementSpeed, rigidbody)
        {
            _animator = animator;
        }

        public override void Enter() {}

        public override void Exit()
        {
            _jumpAvailable = true;
        }

        public override void Update()
        {
            Jump();
            CheckStateTransition();
        }

        private void Jump()
        {
            if (_jumpAvailable && IsGround())
            {
                Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, CalculateJumpVelocity(), Rigidbody.velocity.z);
                _jumpAvailable = false;
                _animator.PlayJump();
            }
        }

        private bool IsGround() => 
            Mathf.Abs(Rigidbody.velocity.y) < 0.01f;

        private float CalculateJumpVelocity() => 
            Mathf.Sqrt(1 * -2f * Physics.gravity.y);

        private void CheckStateTransition()
        {
            if (IsGround())
            {
                StateSwitcher.SwitchState<IdleState>();
            }
        }
    }
}