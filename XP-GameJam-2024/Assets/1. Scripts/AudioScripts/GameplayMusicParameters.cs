using FMODUtilityPackage.Core;
using Timer;
using UnityEngine;
using VDFramework;

namespace AudioScripts
{
	public class GameplayMusicParameters : BetterMonoBehaviour
	{
		[SerializeField]
		private GameTimer timer;

		private void LateUpdate()
		{
			double totalSecondsRemainingNormalized = timer.TotalSecondsRemainingNormalized;
			
			if (totalSecondsRemainingNormalized <= 0.33f)
			{
				AudioParameterManager.SetGlobalParameter("Phase", 1);
			}
			else
			{
				AudioParameterManager.SetGlobalParameter("Phase", 0);
			}
		}
	}
}