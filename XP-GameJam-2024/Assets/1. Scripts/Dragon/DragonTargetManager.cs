using System.Linq;
using GameManagement;
using GameManagement.Events;
using UnityEngine;
using VDFramework;
using VDFramework.Extensions;
using VDFramework.UnityExtensions;

namespace Dragon
{
    public class DragonTargetManager : BetterMonoBehaviour
    {
        private GameObject[] targets;
        
        public GameObject CurrentTarget { get; private set; }
        public bool HasValidTarget => CurrentTarget != null;
        public bool TargetsAvailable => targets.Length > 0;
        
        private void Awake()
        {
            GetAllPossibleTargets();
        }

        private void Start()
        {
            VillagerDeathEvent.AddListener(GetAllPossibleTargets);
        }

        public void SetNewTarget()
        {
            if (GetRandomTarget(out GameObject target))
            {
                CurrentTarget = target;
            }

            CurrentTarget = null;
        }

        private bool GetRandomTarget(out GameObject target)
        {
            target = targets.GetRandomElement();

            return target != null;
        }

        private void GetAllPossibleTargets()
        {
            targets = FindObjectsByType<Villager>(FindObjectsSortMode.None).ToGameObject().ToArray();
        }

        private void OnDestroy()
        {
            VillagerDeathEvent.RemoveListener(GetAllPossibleTargets);
        }
    }
}
