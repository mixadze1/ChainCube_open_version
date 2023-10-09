namespace _Scripts.Services.Input
{
    public class JoystickInput : DynamicJoystick, IInputService
    {
        public float GetHorizontal() => 
             Horizontal;
    }
}