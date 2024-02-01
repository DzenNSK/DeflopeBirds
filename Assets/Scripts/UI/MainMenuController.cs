using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button scoreButton;
        [SerializeField] private GameObject scoreScreen;

        [Inject] GameController gameController;

        private void OnStartButton()
        {
            gameController.SwitchState(GameStates.Run);
        }

        private void OnScoreButton()
        {
            scoreScreen.SetActive(true);
        }

        private void Subscribe()
        {
            startButton.onClick.AddListener(OnStartButton);
            scoreButton.onClick.AddListener(OnScoreButton);
        }

        void Start()
        {
            Subscribe();
        }
    }
}
