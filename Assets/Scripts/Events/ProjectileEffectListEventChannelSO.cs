using UnityEngine.Events;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This class is used for Events with a List<ProjectileEffect> argument
/// Example: An event to toggle a UI interface
/// </summary>

[CreateAssetMenu(menuName = "Events/ProjectileEffectList Event Channel")]
public class ProjectileEffectListEventChannel : DescriptionBaseSO
{
	public event UnityAction<List<PlayerProjectileEffectSO>> OnEventRaised;

	public void RaiseEvent(List<PlayerProjectileEffectSO> value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
