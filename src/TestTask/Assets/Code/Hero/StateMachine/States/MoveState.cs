using Code.Services.Input;
using UnityEngine;

namespace Code.Hero.StateMachine.States
{
    public class MoveState : BaseMovementState
    {
        public MoveState(IStateSwitcher stateSwitcher, IInputService inputService, float movementSpeed, Rigidbody rigidbody) : base(stateSwitcher, inputService, movementSpeed, rigidbody) {}

        public override void Enter() {}

        public override void Exit() {}

        public override void Update()
        {
            Move();
            CheckForIdleState();
            CheckForJump();
        }

        private void Move()
        {
            var movementVector = CalculateMovementVector();

            Rigidbody.velocity = new Vector3(movementVector.x * MovementSpeed, Rigidbody.velocity.y, movementVector.z * MovementSpeed);
        }

        private Vector3 CalculateMovementVector()
        {
            Vector3 movementVector = Vector3.zero;

            if (InputService.Axis.sqrMagnitude > 0.001f)
            {
                movementVector = Camera.transform.TransformDirection(InputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                RotateAlongDirection(movementVector);
            }
            
            return movementVector;
        }

        private void RotateAlongDirection(Vector3 movementVector) =>
            Rigidbody.transform.forward = movementVector;
        
    }
}