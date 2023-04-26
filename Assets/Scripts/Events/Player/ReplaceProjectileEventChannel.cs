using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Buttons or Events when the player replaces a projectile ability
/// </summary>

[CreateAssetMenu(menuName = "Events/Player/ReplaceProjectileEventChannel")]
public class ReplaceProjectileEventChannel : DescriptionBaseSO
{
	public UnityAction<ProjectileEffectSO, int> OnEventRaised;

	public void RaiseEvent(ProjectileEffectSO newEffect, int index)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(newEffect, index);
	}
}


