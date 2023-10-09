using System.Collections.Generic;
using _Scripts.Handlers;
using TMPro;
using UnityEngine;

namespace _Scripts.GameEntities
{
    public class GameEntityView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer; 
        [SerializeField] private List<TextMeshProUGUI> _numbersView;

        private ColorHandler _colorHandler;

        public void Initialize(ColorHandler colorHandler, int numberEntity)
        {
            _colorHandler = colorHandler;
            UpdateView(numberEntity);
        }

        public void UpdateView(int numberEntity)
        {
            foreach (var text in _numbersView)
            {
                text.text = numberEntity.ToString();
            }
            _meshRenderer.material.color = _colorHandler.SetCurrentColor(numberEntity);
        }
    }
}
