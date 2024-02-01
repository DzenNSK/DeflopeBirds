using UnityEngine;
using Zenject;

namespace Dzen.DeflopeBirds
{
    public class ObstacleController : MonoBehaviour
    {
        public class Pool : MonoMemoryPool<ObstacleController>
        {
        }
    }
}
