using UnityEngine;
using UnityEngine.SceneManagement;
using VDFramework;

namespace Menu
{
	public class MenuFunctionality : BetterMonoBehaviour
	{
		public void Quit()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
			return;
#pragma warning disable CS0162 // Unreachable code detected
#endif
			
			Application.Quit();
		}

		public void LoadNextScene()
		{
			int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
			SceneManager.LoadScene(nextIndex);
		}

		public void LoadScene(int buildIndex)
		{
			SceneManager.LoadScene(buildIndex);
		}

		public void LoadScene(int buildIndex, LoadSceneMode loadSceneMode)
		{
			SceneManager.LoadScene(buildIndex, loadSceneMode);
		}
	}
}
