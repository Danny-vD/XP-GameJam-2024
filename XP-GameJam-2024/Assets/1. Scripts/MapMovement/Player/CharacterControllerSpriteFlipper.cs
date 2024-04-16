using UnityEngine;
using VDFramework;

namespace MapMovement.Player
{
	[DisallowMultipleComponent, RequireComponent(typeof(SpriteRenderer))]
	public class CharacterControllerSpriteFlipper : BetterMonoBehaviour
	{
		[SerializeField]
		private bool flipXOnLeft = false;
		
		private CharacterController characterController;
		private SpriteRenderer spriteRenderer;

		private void Awake()
		{
			characterController = GetComponentInParent<CharacterController>();
			spriteRenderer      = GetComponent<SpriteRenderer>();
		}

		private void LateUpdate()
		{
			Vector3 velocity = characterController.velocity;

			if (velocity.x > 0)
			{
				spriteRenderer.flipX = !flipXOnLeft;
			}
			else if (velocity.x < 0)
			{
				spriteRenderer.flipX = flipXOnLeft;
			}
		}
	}
}