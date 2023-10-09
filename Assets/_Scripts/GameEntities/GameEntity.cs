using System;
using _Scripts.AssetsProvider;
using _Scripts.Factory;
using _Scripts.Game;
using _Scripts.Generators;
using _Scripts.Handlers;
using UnityEngine;

namespace _Scripts.GameEntities
{
    [RequireComponent(typeof(GameEntityView))]
    [RequireComponent(typeof(Rigidbody))]
    public class GameEntity : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Transform _parentGameEntity;
        private GameEntityView _gameEntityView;
        private FactoryPoof _factoryPoof;
        private GameEntityTouchHandler _gameEntityTouchHandler;
        private Score _score;

        private IReclaimerEntity _reclaimerEntity;
        
        private float _jumpPower = 7;

        private int _increaseValue = 2;

        public Action OnTouchGameObject;

        public int ValueEntity { get; private set; }

        public void Initialize(IReclaimerEntity reclaimerEntity, GameEntityTouchHandler gameEntityTouchHandler,
            int valueEntity,
            ColorHandler colorHandler, Vector3 position, Score score, FactoryPoof factoryPoof,
            Transform parentGameEntity)
        {
            _parentGameEntity = parentGameEntity;
            _factoryPoof = factoryPoof;
            _reclaimerEntity = reclaimerEntity;
            _score = score;
            _gameEntityTouchHandler = gameEntityTouchHandler;
            ValueEntity = valueEntity;
            SetPosition(position);
            InitializeDependency();
            InitializeGameEntityView(colorHandler);
        }

        public void ReclaimEntity()
        {
            DisableEntity();
            OnTouchGameObject?.Invoke();
            CreatePoof();
            _reclaimerEntity.ReclaimEntity(this);
        }

        private void CreatePoof()
        {
            var instance = _factoryPoof.CreatePoof(AssetPath.Poof, _parentGameEntity);
            instance.Initialize(transform.position);
        }

        private void InitializeDependency()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _gameEntityView = GetComponent<GameEntityView>();
        }

        private void InitializeGameEntityView(ColorHandler colorHandler) =>
            _gameEntityView.Initialize(colorHandler, ValueEntity);

        private void SetPosition(Vector3 position) =>
            transform.position = position;

        public void OnCollisionEnter(Collision other)
        {
            var gameEntity = OnTouchGameEntity(other);

            if (OnTouchWall(other) || gameEntity)
                OnTouchGameObject?.Invoke();
        }

        private void OnCollisionStay(Collision other)
        {
            var gameEntity = OnTouchGameEntity(other);
            if (gameEntity && IsSameNumberEntitiesCondition(gameEntity))
            {
                IncreaseEntityNumber();
                UpdateScore();
                JumpEntity();
                OnFindSameEntity(gameEntity);
            }
        }

        private void JumpEntity() =>
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpPower, _rigidbody.velocity.z);

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
            ValueEntity *= _increaseValue;
            UpdateView(ValueEntity);
        }

        private void UpdateScore() =>
            _score.UpdateScore(ValueEntity);

        private void UpdateView(int valueEntity) =>
            _gameEntityView.UpdateView(valueEntity);

        private void DisableEntity() =>
            gameObject.SetActive(false);

        private bool IsSameNumberEntitiesCondition(GameEntity gameEntity) =>
            gameEntity.ValueEntity == ValueEntity && gameObject.activeSelf;
    }
}
