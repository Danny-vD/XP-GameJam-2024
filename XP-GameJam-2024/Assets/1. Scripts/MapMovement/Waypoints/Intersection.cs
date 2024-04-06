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
        [SerializeField] public List<Intersection> Connections;

        private void Update()
        {
            foreach (var intersection in Connections)
            {   
                Debug.DrawLine(transform.position, intersection.transform.position, Color.red);
            }
        }
    }
}