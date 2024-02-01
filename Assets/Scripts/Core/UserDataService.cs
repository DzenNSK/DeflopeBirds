using UnityEngine;

namespace Dzen.DeflopeBirds
{
    public class UserDataService : MonoBehaviour
    {
        private const string highScoreKey = "HighScore";

        public int HighScore {  get; private set; }

        public void UpdateHighScore(int score)
        {
            if (score <= HighScore) return;
            HighScore = score;
            PlayerPrefs.SetInt(highScoreKey, HighScore);
            PlayerPrefs.Save();
        }

        private void Start()
        {
            HighScore = PlayerPrefs.GetInt(highScoreKey);
        }
    }
}
