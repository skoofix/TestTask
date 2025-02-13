using UnityEngine;

namespace Code.Services.Input
{
    public class InputService : IInputService
    {
        private const string JumpButton = "Jump";
        private const string Vertical = "Vertical";
        private const string Horizontal = "Horizontal";
        private const string MouseY = "Mouse Y";
        private const string MouseX = "Mouse X";
        
        public Vector2 Axis => 
            new Vector2(GetHorizontalAxis(), GetVerticalAxis());

        public bool IsJumpButtonDown() => 
            UnityEngine.Input.GetButtonDown(JumpButton);

        public bool HasAxisInput() => 
            Mathf.Abs(GetHorizontalAxis()) > 0.01f || Mathf.Abs(GetVerticalAxis()) > 0.01f;
        
        public bool GetRightMouseButton() =>
            UnityEngine.Input.GetMouseButton(1);

        public float GetVerticalMouseAxis() =>
            UnityEngine.Input.GetAxis(MouseY);

        public float GetHorizontalMouseAxis() =>
            UnityEngine.Input.GetAxis(MouseX);

        private float GetVerticalAxis() => 
            UnityEngine.Input.GetAxis(Vertical);
        private float GetHorizontalAxis() => 
            UnityEngine.Input.GetAxis(Horizontal);
    }
}