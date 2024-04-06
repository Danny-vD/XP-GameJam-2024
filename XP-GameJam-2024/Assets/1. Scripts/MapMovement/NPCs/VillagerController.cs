using System;
using System.Collections.Generic;
using System.Linq;
using Input;
using MapMovement.Commands;
using MapMovement.Waypoints;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using VDFramework;

namespace MapMovement.NPCs
{
    public class VillagerController : BetterMonoBehaviour, IActorMover
    {
        public event Action OnMovementStart;
        public event Action OnMovementCancelled;
        public event Action OnEnterIdle;
        
        [SerializeField] private Intersection currentNode;

        private Queue<IMoveCommand> commandsQueue;  
        
        
        private NavMeshAgent agent;

        private void OnEnable()
        {
            agent = GetComponent<NavMeshAgent>();
            InputControllerManager.Instance.mainInput.Overworld.Movement.performed += MovementOnPerformed;
        }

        private void MovementOnPerformed(InputAction.CallbackContext obj)
        {
            agent.SetDestination(currentNode.Connections.First().transform.position);
            //agent.isStopped.
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }

        
    }
}