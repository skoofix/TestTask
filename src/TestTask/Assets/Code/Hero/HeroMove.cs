using Code.Infrastructure;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class HeroMove: MonoBehaviour
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;
        
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;
            
            _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
        }
    }
}