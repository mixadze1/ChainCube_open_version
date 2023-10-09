using UnityEngine;

namespace _Scripts.Services.Input
{
    public class JoystickInput : DynamicJoystick, IInputService
    {
        public float GetHorizontal() => 
             Horizontal;

        public float GetVertical() => 
            IsActiveJoystick() ? 0 : Vertical;

        public Vector2 GetDirection() => 
            IsActiveJoystick() ? Vector2.zero : Direction;

        private bool IsActiveJoystick() => 
            !isActiveAndEnabled;
    }
}