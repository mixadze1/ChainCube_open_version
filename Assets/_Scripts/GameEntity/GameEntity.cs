using System;
using _Scripts.Game;
using UnityEngine;

namespace _Scripts.GameEntity
{
    public class GameEntity : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameEntityView _gameEntityView;

        private float _jumpPower = 7;

        private IGameHandler _gameHandler;
        
        private GameEntityTouchHandler _gameEntityTouchHandler;
        private Score _score;

        public Action OnTouchGameObject;

        public int ValueEntity { get; private set; }

        public void Initialize(IGameHandler gameHandler, GameEntityTouchHandler gameEntityTouchHandler, int valueEntity, ColorHandler colorHandler,  Vector3 position, Score score)
        {
            _gameHandler = gameHandler;
            _score = score;
            _gameEntityTouchHandler = gameEntityTouchHandler;
            ValueEntity = valueEntity;
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
                UpdateScore();
                JumpEntity();
                OnFindSameEntity(gameEntity);
            }
        }

        private void JumpEntity()
        {
            Debug.Log("Jump");
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpPower, _rigidbody.velocity.z);
        }

        private void OnFindSameEntity(GameEntity gameEntity)
        {
            _gameEntityTouchHandler.OnFindSameEntity(gameEntity);
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

        private void UpdateScore()
        {
            _score.UpdateScore(ValueEntity);
        }

        private void UpdateView(int valueEntity)
        {
            _gameEntityView.UpdateView(valueEntity);
        }

        public void ReclaimEntity()
        {
            DisableEntity();
            OnTouchGameObject?.Invoke();
            _gameHandler.ReclaimGameEntity(this);
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
