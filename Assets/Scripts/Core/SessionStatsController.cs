using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class SessionStatsController : MonoBehaviour
    {
        [Inject] private GameController gameController;
        [Inject] private UserDataService userDataService;

        public int SessionScore { get; private set; }
        public readonly UnityEvent OnScoreChange = new();

        public void AddScore()
        {
            SessionScore++;
            OnScoreChange?.Invoke();
        }

        private void OnGameStateChange()
        {
            if(gameController.State == GameStates.MainMenu)
            {
                userDataService.UpdateHighScore(SessionScore);
                SessionScore = 0;
                OnScoreChange.Invoke();
            }
        }

        private void Start()
        {
            gameController.OnStateChange.AddListener(OnGameStateChange);
            OnGameStateChange();
        }
    }
}
