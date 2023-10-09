using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Game
{
    public class OnGameObjectClickHandler : MonoBehaviour, IClickGameObjectHandler, IPointerDownHandler, IPointerExitHandler
    {
        public event Action<PointerEventData> OnDownClick;
        public event Action<PointerEventData> OnExitClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDownClick?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnExitClick?.Invoke(eventData);
        }
    }
}