using System;

namespace _Scripts.Services.Input
{
    public interface IInputService
    {
        float GetHorizontal();
        event Action OnExitInput;
    }
}