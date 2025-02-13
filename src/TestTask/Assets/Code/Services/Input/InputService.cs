using UnityEngine;

namespace Code.Services.Input
{
    public class InputService : IInputService
    {
        private const string JumpButton = "Jump";
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";
        
        public Vector2 Axis => 
            new Vector2(GetHorizontalAxis(), GetVerticalAxis());

        public bool IsJumpButtonUp() => 
            UnityEngine.Input.GetButtonDown(JumpButton);

        private float GetVerticalAxis() => 
            UnityEngine.Input.GetAxis(Vertical);
        private float GetHorizontalAxis() => 
            UnityEngine.Input.GetAxis(Horizontal);
    }
}