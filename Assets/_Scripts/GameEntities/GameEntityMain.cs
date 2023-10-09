using _Scripts.Game;
using _Scripts.Services.Input;
using UnityEngine;

namespace _Scripts.GameEntities
{
    [RequireComponent(typeof(GameEntity))]
    public class GameEntityMain : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _lineSprite;
        [SerializeField] private TrailRenderer _trail;

        private const string GameObjectNameAfterUse = "GameEntity";

        private IGameHandler _gameHandler;
        private IInputService _inputService;

        private Rigidbody _rigidbody;
        private GameEntity _gameEntity;
        private Transform _parentGameEntity;

        private float _speedLeftRight = 10;
        private float _speedToEntities = 25;
        
        private bool _isExitInput;

        public void Initialize(GameEntity gameEntity, Transform parentGameEntity, IInputService inputService, IGameHandler gameHandler)
        {
            _gameHandler = gameHandler;
            _gameEntity = gameEntity;
            _inputService = inputService;
            _parentGameEntity = parentGameEntity;
            gameEntity.OnTouchGameObject += OnTouchGameEntity;
            inputService.OnExitInput += OnExitInput;

            InitializeDependency();
            TrailsState(false);
            FreezeRotationState(true);
        }

        private void TrailsState(bool value) => 
            _trail.enabled = value;

        private void InitializeDependency() => 
            _rigidbody = GetComponent<Rigidbody>();

        private void OnExitInput()
        {
            _isExitInput = true;
            TrailsState(true);
            DisableLineSprite();
            MoveToEntities();
        }

        private void DisableLineSprite() => 
            _lineSprite.gameObject.SetActive(false);

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
            _rigidbody.velocity = new Vector3( _speedLeftRight * horizontal, 0, 0);

        private void MoveToEntities() => 
            _rigidbody.velocity = new Vector3(0, 0, _speedToEntities);

        private void OnTouchGameEntity()
        {
            if (!IsExitInput())
                return;

            SetCurrentParent();
            RenameGameObject();
            FreezeRotationState(false);
            UnsubscribeEvents();
            CreateNewMainGameEntity();
            
            Destroy(this);
            Destroy(_trail.gameObject);
        }

        private void RenameGameObject() => 
            this.gameObject.name = GameObjectNameAfterUse;

        private void SetCurrentParent() => 
            transform.SetParent(_parentGameEntity);

        private void CreateNewMainGameEntity() => 
            _gameHandler.CreateNewGameEntityAfterUsedPrevious();

        private void UnsubscribeEvents()
        {
            _gameEntity.OnTouchGameObject -= OnTouchGameEntity;
            _inputService.OnExitInput -= OnExitInput;
        }

        private void OnDestroy() => 
            UnsubscribeEvents();
    }
}