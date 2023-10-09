using _Scripts.Saves;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Scripts.GameEntities
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private TextMeshProUGUI _bestScore;

        private SaveScore _saveScore;
    
        private int _valueScore;

        [Inject]
        public void Initialize(SaveScore saveScore)
        {
            _saveScore = saveScore;
        }

        public void RestartScore()
        {
            _valueScore = 0;
            UpdateScore(_valueScore);
        }

        public void UpdateScore(int value)
        {
            _valueScore += value;
            UpdateViewScore();
            SaveBestScore();
            UpdateViewBestScore();
        }

        private bool SaveBestScore()
        {
            var operation =  _saveScore.TrySaveBestScore(_valueScore);
            return operation;
        }

        private void UpdateViewScore() => 
            _score.text = $"Score: {_valueScore}";

        private void UpdateViewBestScore() => 
            _bestScore.text = $"BestScore: {_saveScore.GetBestScore()}";
    }
}
