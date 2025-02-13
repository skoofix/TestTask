using UnityEngine;

namespace Code.Services.Input
{
    public interface IInputService
    {
        Vector2 Axis { get; }

        bool IsJumpButtonDown();
        bool HasAxisInput();
        bool GetRightMouseButton();
        float GetVerticalMouseAxis();
        float GetHorizontalMouseAxis();
    }
}