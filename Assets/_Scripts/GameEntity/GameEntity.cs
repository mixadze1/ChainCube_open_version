using System;
using _Scripts.Game;
using UnityEngine;

namespace _Scripts.GameEntity
{
    public class GameEntity : MonoBehaviour
    {
        [SerializeField] private GameEntityView _gameEntityView;
        
        private GameEntityTouchHandler _gameEntityTouchHandler;

        public Action OnTouchGameEntity;

        public int ValueEntity { get; private set; }

        public void Initialize(GameEntityTouchHandler gameEntityTouchHandler, GameEntityModel gameEntityModel, ColorHandler colorHandler,  Vector3 position)
        {
            _gameEntityTouchHandler = gameEntityTouchHandler;
            ValueEntity = gameEntityModel.ValueEntity;
            SetPosition(position);
            InitializeGameEntityView(colorHandler);
        }

        private void InitializeGameEntityView(ColorHandler colorHandler)
        {
            _gameEntityView.Initialize(colorHandler, ValueEntity);
        }

        private void SetPosition(Vector3 position) => 
            transform.position = position;
    
        public void OnCollisionEnter(Collision other)
        {
            other.transform.TryGetComponent(out GameEntity gameEntity);

            if (gameEntity && IsSameNumberEntities(gameEntity))
            {
                IncreaseEntityNumber();
                OnTouchGameEntity?.Invoke();
                _gameEntityTouchHandler.FindSameEntity(gameEntity);
            }
        }

        private void IncreaseEntityNumber()
        {
            ValueEntity *= 2;
            UpdateView(ValueEntity);
        }

        private void UpdateView(int valueEntity)
        {
            _gameEntityView.UpdateView(valueEntity);
        }


        public void DestroyEntity()
        {
            DisableEntity();
            Destroy(gameObject);
        }

        private void DisableEntity()
        {
            gameObject.SetActive(false);
        }

        private bool IsSameNumberEntities(GameEntity gameEntity)
        {
            return gameEntity.ValueEntity == ValueEntity && gameObject.activeSelf;
        }
    }
}
