using UnityEngine;
using UnityEngine.Events;

namespace Dzen.DeflopeBirds
{
    public enum GameStates
    {
        MainMenu, Run, Pause, Death
    }

    public class GameController : MonoBehaviour
    {
        public GameStates State { get; private set; }
        public readonly UnityEvent OnStateChange = new();

        public void SwitchState(GameStates state)
        {
            if (state == State) return;
            State = state;
            OnStateChange?.Invoke();
        }

        private void Start()
        {
            SwitchState(GameStates.MainMenu);
        }
    }
}
