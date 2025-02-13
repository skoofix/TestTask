using Code.Infrastructure;
using Code.Services.Input;
using UnityEngine;

namespace Code.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        private IInputService _inputService;
        
        [SerializeField] private Transform _following;
        [SerializeField] private float _rotationAngleX = 0f;
        [SerializeField] private float _rotationAngleY = 0f;
        [SerializeField] private int _distance = 10;
        [SerializeField] private float _offsetY = 2f;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _minAngleX = -40f;
        [SerializeField] private float _maxAngleX = 80f;
        [SerializeField] private LayerMask _collisionLayer;

        private void Awake() => 
            _inputService = Game.InputService;

        private void LateUpdate()
        {
            if (_inputService.GetRightMouseButton())
                HandleCameraRotation();

            Vector3 desiredPosition = CalculateDesiredPosition();
            Vector3 finalPosition = HandleCollision(desiredPosition);

            SetCameraPosition(finalPosition);
            LookAtTarget();
        }

        private void HandleCameraRotation()
        {
            float horizontalInput = _inputService.GetHorizontalMouseAxis();
            float verticalInput = _inputService.GetVerticalMouseAxis();

            _rotationAngleY += horizontalInput * _rotationSpeed;
            _rotationAngleX -= verticalInput * _rotationSpeed;

            _rotationAngleX = Mathf.Clamp(_rotationAngleX, _minAngleX, _maxAngleX);
        }

        private Vector3 CalculateDesiredPosition()
        {
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, _rotationAngleY, 0);
            return rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();
        }

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;
            return followingPosition;
        }

        private Vector3 HandleCollision(Vector3 desiredPosition)
        {
            RaycastHit hit;
            Vector3 from = FollowingPointPosition();
            Vector3 direction = desiredPosition - from;

            if (Physics.Raycast(from, direction, out hit, _distance, _collisionLayer))
                return hit.point;

            return desiredPosition;
        }

        private void SetCameraPosition(Vector3 position) => 
            transform.position = position;

        private void LookAtTarget() => 
            transform.LookAt(_following);
    }
}