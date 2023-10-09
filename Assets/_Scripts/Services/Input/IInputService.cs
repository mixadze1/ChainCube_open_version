using UnityEngine;

namespace _Scripts.Services.Input
{
    public interface IInputService
    {
        float GetHorizontal();

        float GetVertical();

        Vector2 GetDirection();
    }
}