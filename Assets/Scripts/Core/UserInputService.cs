using UnityEngine;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class UserInputService : MonoBehaviour
    {
        [Inject] private GameController gameController;

        private bool touch;

        public bool GetTouch()
        {
            var res = touch;
            touch = false;
            return res;
        }

        private void Update()
        {
            if (gameController.State != GameStates.Run)
            {
                touch = false;
                return;
            }

            touch = Input.anyKeyDown;
        }
    }
}
