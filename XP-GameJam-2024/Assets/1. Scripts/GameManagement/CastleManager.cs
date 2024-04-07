using System;
using FMODUtilityPackage.Core;
using FMODUtilityPackage.Enums;
using GameManagement.Events;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace GameManagement
{
    
    public class CastleManager : BetterMonoBehaviour
    {
        [SerializeField] private int layer;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.layer.Equals(layer)) return;
            
            AudioPlayer.PlayOneShot2D(AudioEventType.Sound_Effects_NPCs_Cheer);
            Destroy(other.gameObject);
            EventManager.RaiseEvent<VillagerSaveEvent>(new VillagerSaveEvent());
        }
    }
}