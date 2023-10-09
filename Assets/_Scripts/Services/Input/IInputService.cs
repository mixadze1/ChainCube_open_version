using System;
using UnityEngine;

namespace _Scripts.Services.Input
{
    public interface IInputService
    {
        float GetHorizontal();

        public event Action OnExitInput;
    }
}