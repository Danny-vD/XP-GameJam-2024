using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using VDFramework;


namespace MapMovement.Waypoints
{
    [ExecuteInEditMode]    
    public class Intersection : BetterMonoBehaviour
    {
        [SerializeField] private List<Intersection> connections;

        private void Update()
        {
            foreach (var intersection in connections)
            {   
                Debug.DrawLine(transform.position, intersection.transform.position, Color.red);
            }
        }
    }
}