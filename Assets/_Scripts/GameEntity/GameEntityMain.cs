using _Scripts.Game;
using _Scripts.Services.Input;
using UnityEngine;

namespace _Scripts.GameEntity
{
    public class GameEntityMain : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private Transform _trail;
        [SerializeField] private Rigidbody _rigidbody;
        
        private IGameHandler _gameHandler;
        private IInputService _inputService;

        private GameEntity _gameEntity;
        private Transform _parentGameEntity;

        private float _speed = 5;

        private bool _isExitInput;
        private float _speedToEntities = 25;

        public void Initialize(GameEntity gameEntity, Transform parentGameEntity, IInputService inputService, IGameHandler gameHandler)
        {
            _gameHandler = gameHandler;
            _gameEntity = gameEntity;
            _inputService = inputService;
            _parentGameEntity = parentGameEntity;
            gameEntity.OnTouchGameObject += OnTouchGameEntity;
            inputService.OnExitInput += OnExitInput;
            
            FreezeRotationState(true);
        }

        private void OnExitInput()
        {
            _isExitInput = true;
            _sprite.gameObject.SetActive(false);
            MoveToEntities();
            Debug.Log("OnExitInput");
        }

        private void FreezeRotationState(bool value) => 
            _rigidbody.freezeRotation = value;

        private void FixedUpdate()
        {
            if (IsExitInput())
            {
                MoveToEntities();
                return;
            }
            
            var horizontal = _inputService.GetHorizontal();
            MoveToLeftRight(horizontal);
        }

        private bool IsExitInput() => 
            _isExitInput;

        private void MoveToLeftRight(float horizontal) => 
            _rigidbody.velocity = new Vector3( _speed * horizontal, 0, 0);

        private void MoveToEntities() => 
            _rigidbody.velocity = new Vector3(0, 0, _speedToEntities);

        private void OnTouchGameEntity()
        {
            if (!IsExitInput())
                return;

            SetCurrentParent();
            FreezeRotationState(false);
            UnsubscribeEvents();
            CreateNewMainGameEntity();
            
            Destroy(this);
            Destroy(_trail.gameObject);
        }

        private void SetCurrentParent() => 
            transform.SetParent(_parentGameEntity);

        private void CreateNewMainGameEntity() => 
            _gameHandler.CreateNewGameEntityAfterUsedPrevious();

        private void UnsubscribeEvents()
        {
            _gameEntity.OnTouchGameObject -= OnTouchGameEntity;
            _inputService.OnExitInput -= OnExitInput;
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }
    }
}