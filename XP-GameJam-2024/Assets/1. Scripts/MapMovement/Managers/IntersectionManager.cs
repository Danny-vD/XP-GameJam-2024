using System.Collections.Generic;
using System.Linq;
using MapMovement.Waypoints;
using VDFramework.Singleton;

namespace MapMovement.Managers
{
    public class IntersectionManager : Singleton<IntersectionManager>
    {
        public List<Intersection> IntersectionList { get; set; }

        protected override void Awake()
        {
            base.Awake();
            IntersectionList = new List<Intersection>();
            FindIntersections();
        }

        private void FindIntersections()
        {
            IntersectionList = GetComponents<Intersection>().ToList();
        }
    }
}