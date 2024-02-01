using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject postcombat;
        [SerializeField] private GameObject pause;

        [Inject] private GameController gameController;

        private void OnGameStateChange()
        {
            mainMenu.SetActive(gameController.State == GameStates.MainMenu);
            hud.SetActive(gameController.State != GameStates.MainMenu && gameController.State != GameStates.Death);
            postcombat.SetActive(gameController.State == GameStates.Death);
            pause.SetActive(gameController.State == GameStates.Pause);
        }

        private void Start()
        {
            gameController.OnStateChange.AddListener(OnGameStateChange);
            OnGameStateChange();
        }
    }
}
