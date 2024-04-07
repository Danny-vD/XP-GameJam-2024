using System;
using GameManagement.Events;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace GameManagement
{
    public class CastleManager : BetterMonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.layer.Equals(7)) return;
            
            Destroy(other.gameObject);
            EventManager.RaiseEvent<VillagerSaveEvent>(new VillagerSaveEvent());
        }
    }
}