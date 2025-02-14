using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.StateMachine.States
{
    public class JumpState : BaseMovementState
    {
        private readonly float _jumpForce;
        private readonly HeroAnimator _animator;
        private bool _jumpAvailable = true;
        
        public JumpState(HeroStateMachine stateSwitcher, IInputService inputService, float movementSpeed, float jumpForce, HeroAnimator animator, Rigidbody rigidbody) : base(stateSwitcher, inputService, movementSpeed, rigidbody)
        {
            _jumpForce = jumpForce;
            _animator = animator;
        }

        public override void Enter()
        {
        }

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
            if (_jumpAvailable)
            {
                _animator.PlayJump();
                Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, CalculateJumpVelocity(), Rigidbody.velocity.z);
                _jumpAvailable = false;
            }
        }

        private float CalculateJumpVelocity() => 
            Mathf.Sqrt(_jumpForce * -2f * Physics.gravity.y);

        private void CheckStateTransition()
        {
            if (IsGround())
            {
                StateSwitcher.SwitchState<IdleState>();
            }
        }

        private bool IsGround() => 
            Mathf.Abs(Rigidbody.velocity.y) < 0.01f;
    }
}