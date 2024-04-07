using Timer.Events;
using UnityEngine;
using VDFramework;

namespace Dragon
{
    public class DragonSpawner : BetterMonoBehaviour
    {
        [SerializeField]
        private GameObject dragonPrefab;
        
        private void Awake()
        {
            GameTimerExpiredEvent.AddListener(SpawnDragon);
        }

        private void OnDestroy()
        {
            GameTimerExpiredEvent.RemoveListener(SpawnDragon);
        }

        private void SpawnDragon()
        {
            Instantiate(dragonPrefab, transform.position, Quaternion.identity);
        }
    }
}
