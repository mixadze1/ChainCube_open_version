using System;
using UnityEngine.EventSystems;

namespace _Scripts.Game
{
    public interface IClickGameObjectHandler
    {
        public event Action<PointerEventData> OnDownClick;

        public event Action<PointerEventData> OnExitClick;
    }
}