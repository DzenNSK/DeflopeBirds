using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private Transform sceneRoot;
        [SerializeField] private Transform birdSpawnPoint;
        [SerializeField] private BirdController birdController;
        [SerializeField] private ObstacleSpawner obstacleSpawner;

        [Inject] private GameController gameController;

        public void ResetScene()
        {
            birdController.ResetBird(birdSpawnPoint.position);
            obstacleSpawner.ResetObstacles();
            obstacleSpawner.Run();
        }

        private void CheckMembers()
        {
            if (birdController == null) Debug.LogError("[SceneController] No BirdController attached!");
            if(obstacleSpawner == null) Debug.LogError("[SceneController] No ObstacleSpawner attached!");
        }

        private void OnGameStateChange()
        {
            if (gameController.State == GameStates.MainMenu) ResetScene();
            sceneRoot.gameObject.SetActive(gameController.State != GameStates.MainMenu);
        }

        private void Start()
        {
            CheckMembers();
            ResetScene();
            gameController.OnStateChange.AddListener(OnGameStateChange);
            OnGameStateChange();
        }

        void Update()
        {
        
        }
    }
}
