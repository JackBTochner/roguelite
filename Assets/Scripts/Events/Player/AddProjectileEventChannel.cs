using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class is used for Buttons or Events when the player gains a new Projectile effect
/// </summary>

[CreateAssetMenu(menuName = "Events/Player/AddProjectileEventChannel")]
public class AddProjectileEventChannel : DescriptionBaseSO
{
	public UnityAction<ProjectileEffectSO> OnEventRaised;

	public void RaiseEvent(ProjectileEffectSO value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}


