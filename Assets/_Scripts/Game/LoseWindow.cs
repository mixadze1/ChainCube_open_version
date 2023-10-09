using _Scripts.Generators;
using _Scripts.Handlers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Game
{
    public class LoseWindow : MonoBehaviour
    {
        [SerializeField] private RectTransform _container;

        [SerializeField] private Button _restart;
        [SerializeField] private Button _restartSecond;
       
        private ILoseHandler _loseHandler;
        private IReclaimerEntity _reclaimerEntity;

        [Inject]
        public void Initialize(ILoseHandler loseHandler, IReclaimerEntity reclaimerEntity)
        {
            _reclaimerEntity = reclaimerEntity;
            _loseHandler = loseHandler;
            _restart.onClick.AddListener(CompleteLose);
            _restartSecond.onClick.AddListener(CompleteLose);
        }

        private void CompleteLose()
        {
            DisableView();
            ClearEntities();
            _loseHandler.CompleteLose();
        }

        private void ClearEntities()
        {
            _reclaimerEntity.ClearEntities();
        }

        private void DisableView()
        {
            _container.gameObject.SetActive(false);
        }

        public void EnableView() => 
            _container.gameObject.SetActive(true);
    }
}