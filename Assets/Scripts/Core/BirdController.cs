using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class BirdController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D birdBody;
        [SerializeField] private float kickForce;

        [Inject] private GameController gameController;
        [Inject] private SessionStatsController sessionStatsController;
        [Inject] private UserInputService userInputService;

        private float timeout;
        private bool paused;
        private Vector2 velocityBackup;

        public void Kick()
        {
            //birdBody.AddForce(Vector2.up * kickForce, ForceMode2D.Impulse);
            birdBody.velocity = Vector2.up * kickForce;
        }

        public void ResetBird(Vector3 position)
        {
            gameObject.transform.position = position;
            birdBody.velocity = Vector3.zero;
        }

        private void OnGameStateChange()
        {
            if(gameController.State == GameStates.Pause)
            {
                paused = true;
                velocityBackup = birdBody.velocity;
                birdBody.Sleep();
            }
            else if (gameController.State == GameStates.Run && paused)
            {
                birdBody.WakeUp();
                birdBody.velocity = velocityBackup;
                paused = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Obstacle")
            {
                Debug.Log("Death");
                gameController.SwitchState(GameStates.Death);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ObstacleExit")
            {
                Debug.Log("Score");
                sessionStatsController.AddScore();
            }
        }

        private void Start()
        {
            gameController.OnStateChange.AddListener(OnGameStateChange);
            OnGameStateChange();
        }

        private void Update()
        {
            if (timeout > 0) 
            { 
                timeout -= Time.deltaTime;
                if (timeout <= 0) timeout = 0;
                else return;
            }

            if (userInputService.GetTouch())
            {
                Kick();
                timeout = 0.3f;
            }
        }
    }
}
