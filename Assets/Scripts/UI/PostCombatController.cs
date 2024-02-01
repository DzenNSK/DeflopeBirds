using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class PostCombatController : MonoBehaviour
    {
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private TMP_Text highScoreField;
        [SerializeField] private TMP_Text scoreField;

        [Inject] private GameController gameController;
        [Inject] private SessionStatsController sessionStatsController;
        [Inject] private UserDataService userDataService;

        private void OnMainMenuButton()
        {
            gameController.SwitchState(GameStates.MainMenu);
        }

        private void OnGameStateChange()
        {
            if(gameController.State == GameStates.Death)
            {
                highScoreField.text = $"High score: {userDataService.HighScore}";
                scoreField.text = $"Score: {sessionStatsController.SessionScore}";
            }
        }

        private void Start()
        {
            mainMenuButton.onClick.AddListener(OnMainMenuButton);
            gameController.OnStateChange.AddListener(OnGameStateChange);
            OnGameStateChange();
        }
    }
}
