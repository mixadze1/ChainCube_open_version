using _Scripts.GameEntities;
using UnityEngine;
using Zenject;

namespace _Scripts.Handlers
{
    public class LoseHandler : MonoBehaviour
    {
        private ILoseHandler _loseHandler;

        [Inject]
        public void Initialize(ILoseHandler gameHandler)
        {
            _loseHandler = gameHandler;
        }

        public void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out GameEntityMain main);
            if (main)
                return;
            
            other.TryGetComponent(out GameEntity gameEntity);
            if (gameEntity)
                _loseHandler.ActivateLoseWindow();
        }
    }
}
