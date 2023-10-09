using System;
using _Scripts.Game;
using UnityEngine;

namespace _Scripts.GameEntity
{
    public class GameEntity : MonoBehaviour
    {
        [SerializeField] private GameEntityView _gameEntityView;
        
        private GameEntityTouchHandler _gameEntityTouchHandler;

        public Action OnTouchGameObject;

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
            var gameEntity = OnTouchGameEntity(other);

       
            
            if(OnTouchWall(other) || gameEntity)
                OnTouchGameObject?.Invoke();
        }

        public void OnCollisionStay(Collision other)
        {
            var gameEntity = OnTouchGameEntity(other);
            if (gameEntity && IsSameNumberEntities(gameEntity))
            {
                IncreaseEntityNumber();
                _gameEntityTouchHandler.FindSameEntity(gameEntity);
            }
        }

        private static GameEntity OnTouchGameEntity(Collision other)
        {
            other.transform.TryGetComponent(out GameEntity gameEntity);
            return gameEntity;
        }

        private static bool OnTouchWall(Collision other)
        {
            other.transform.TryGetComponent(out Wall wall);
            return wall;
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
            OnTouchGameObject?.Invoke();
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
