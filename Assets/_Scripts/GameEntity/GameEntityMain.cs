using _Scripts.Services.Input;
using UnityEngine;

namespace _Scripts.Game
{
    public class GameEntityMain : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        private Transform _parentGameEntity;
        private IInputService _inputService;
        private float _speed = 5;

        public void Initialize(GameEntity.GameEntity gameEntity, Transform parentGameEntity, IInputService inputService)
        {
            gameEntity.OnTouchGameEntity += OnTouchGameEntity;
            _inputService = inputService;
            _parentGameEntity = parentGameEntity;
            FreezeRotationState(true);
        }

        private void FreezeRotationState(bool value) => 
            _rigidbody.freezeRotation = value;

        private void FixedUpdate()
        {
            var horizontal = _inputService.GetHorizontal();
            MoveTo(horizontal);
        }

        private void MoveTo(float horizontal)
        {
            _rigidbody.velocity = new Vector3( _speed * horizontal, 0, 0);
        }

        public void OnTouchGameEntity()
        {
            this.transform.SetParent(_parentGameEntity);
            Destroy(this);
        }
    }
}