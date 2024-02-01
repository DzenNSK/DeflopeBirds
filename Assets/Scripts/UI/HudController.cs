using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreField;
        [SerializeField] private Button pauseButton;

        [Inject] private SessionStatsController sessionStatsController;
        [Inject] private GameController gameController;

        private void OnScoreChange()
        {
            scoreField.text = $"Score {sessionStatsController.SessionScore}";
        }

        private void OnPauseButton()
        {
            gameController.SwitchState(GameStates.Pause);
        }

        private void Start()
        {
            pauseButton.onClick.AddListener(OnPauseButton);
            sessionStatsController.OnScoreChange.AddListener(OnScoreChange);
            OnScoreChange();
        }
    }
}
