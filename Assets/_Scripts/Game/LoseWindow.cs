using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Scripts.Game
{
    public class LoseWindow : MonoBehaviour
    {

        [SerializeField] private RectTransform _container;

        [SerializeField] private Button _restart;
       
        private ILoseHandler _loseHandler;

        [Inject]
        public void Initialize(ILoseHandler loseHandler)
        {
            _loseHandler = loseHandler;
            _restart.onClick.AddListener(CompleteLose);
        }

        private void CompleteLose()
        {
            DisableView();
            _loseHandler.CompleteLose();
        }

        private void DisableView()
        {
            _container.gameObject.SetActive(false);
        }

        public void EnableView() => 
            _container.gameObject.SetActive(true);
    }
}