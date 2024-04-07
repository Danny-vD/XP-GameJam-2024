using UnityEngine;
using VDFramework.Singleton;

namespace GameManagement
{
    public class GameManager : Singleton<GameManager>
    {
        public int saved;
        protected override void Awake()
        {
            base.Awake();
            saved = 0;
        }
        
         
    }
}