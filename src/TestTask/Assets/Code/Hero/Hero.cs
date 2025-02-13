using Code.Hero.StateMachine;
using Code.Infrastructure;
using Code.Services.Input;
using UnityEngine;

namespace Code.Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _jumpForce = 2;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private HeroAnimator  _animator;
        
        private HeroStateMachine _stateMachine;
        private IInputService _inputService;

        private void Awake()
        {
            _inputService = Game.InputService;
            _stateMachine = new HeroStateMachine(_inputService, _movementSpeed, _jumpForce, _animator, _rigidbody);
        }

        private void Update() => 
            _stateMachine.Update();
    }
}