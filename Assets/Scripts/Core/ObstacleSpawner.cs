using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private float obstacleSpawnInterval;
        [SerializeField] private float obstacleDespawnDistance;
        [SerializeField] private float obstacleShiftMax;
        [SerializeField] private float obstacleShiftMin;
        [SerializeField] private float moveSpeed;

        [Inject] private GameController gameController;
        [Inject] private ObstacleController.Pool obstaclePool;

        private readonly HashSet<ObstacleController> activeObstacles = new();
        private readonly HashSet<ObstacleController> obstaclesToDespawn = new();
        private bool active;
        private float currentInterval;

        private void Start()
        {
            active = false;
            gameController.OnStateChange.AddListener(OnGameStateChange);
            OnGameStateChange();
        }

        private void Update()
        {
            if (!active) return;

            UpdateActiveObstacles();
            DespawnObstacles();

            if(currentInterval > 0)
            {
                currentInterval -= Time.deltaTime;
                if (currentInterval <= 0) currentInterval = 0;
                else return;
            }

            SpawnObstacle();
        }

        private void SpawnObstacle()
        {
            var shift = Random.Range(obstacleShiftMin, obstacleShiftMax);
            var obstacle = obstaclePool.Spawn();
            obstacle.transform.position = transform.position + new Vector3(0f, shift, 0f);
            activeObstacles.Add(obstacle);
            currentInterval = obstacleSpawnInterval;
        }

        private void UpdateActiveObstacles()
        {
            var diff = new Vector3(-Time.deltaTime * moveSpeed, 0f, 0f);
            foreach(var obstacle in activeObstacles)
            {
                obstacle.transform.position += diff;
                if (Mathf.Abs(obstacle.transform.position.x - transform.position.x) > obstacleDespawnDistance) obstaclesToDespawn.Add(obstacle);
            }
        }

        private void DespawnObstacles()
        {
            foreach(var obstacle in obstaclesToDespawn)
            {
                activeObstacles.Remove(obstacle);
                obstaclePool.Despawn(obstacle);
            }
            obstaclesToDespawn.Clear();
        }

        public void Run() => active = true;

        public void Stop()
        {
            active = false;
            currentInterval = 0;
        }

        public void ResetObstacles()
        {
            foreach (var obstacle in activeObstacles)
            {
                obstaclePool.Despawn(obstacle);
            }
            activeObstacles.Clear();
        }

        private void OnGameStateChange()
        {
            active = gameController.State == GameStates.Run;
        }
    }
}
