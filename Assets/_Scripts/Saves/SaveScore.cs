using UnityEngine;

namespace _Scripts.Saves
{
    public class SaveScore
    {
        private const string BestScore = "bestScore";

        public bool TrySaveBestScore(int score)
        {
            var oldScore = GetBestScore();
            if (score >= oldScore)
            {
                PlayerPrefs.SetInt(BestScore, score);
                return true;
            }

            return false;
        }

        public int GetBestScore()
        {
            return PlayerPrefs.GetInt(BestScore);
        }
    }
}
