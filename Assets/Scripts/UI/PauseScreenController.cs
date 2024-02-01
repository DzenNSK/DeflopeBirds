using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class PauseScreenController : MonoBehaviour
    {
        [SerializeField] private Button resumeButton;
        [SerializeField] private Button menuButton;

        [Inject] private GameController gameController;

        private void OnResumeButton()
        {
            gameController.SwitchState(GameStates.Run);
        }

        private void OnMenuButton()
        {
            gameController.SwitchState(GameStates.MainMenu);
        }

        private void Start()
        {
            resumeButton.onClick.AddListener(OnResumeButton);
            menuButton.onClick.AddListener(OnMenuButton);
        }
    }
}
