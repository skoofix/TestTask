using UnityEngine;

namespace Code.Hero
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int MoveHash = Animator.StringToHash("Walking");
        private static readonly int JumpHash = Animator.StringToHash("Jump");
        private static readonly int IdleHash = Animator.StringToHash("Idle");

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;

        private void Update() => 
            MoveAnimation();

        private void MoveAnimation()
        {
            _animator.SetFloat(MoveHash, _rigidbody.velocity.magnitude, 0.1f, Time.deltaTime);
        }

        public void PlayJump() => 
            _animator.SetTrigger(JumpHash);
    }
}