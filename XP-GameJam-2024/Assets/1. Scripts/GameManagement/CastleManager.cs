using UnityEngine;
using VDFramework;

namespace GameManagement
{
	public class CastleManager : BetterMonoBehaviour
	{
		[SerializeField]
		private int layer;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.gameObject.layer.Equals(layer)) return;

			Villager villager = other.GetComponent<Villager>();
			villager.Save();
		}
	}
}