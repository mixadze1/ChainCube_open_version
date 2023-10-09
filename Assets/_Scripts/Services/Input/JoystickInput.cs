using System;
using UnityEngine;

namespace _Scripts.Services.Input
{
    public class JoystickInput : DynamicJoystick, IInputService
    {
        public float GetHorizontal() => 
             Horizontal;
        
        private bool IsActiveJoystick() => 
            !isActiveAndEnabled;
    }
}