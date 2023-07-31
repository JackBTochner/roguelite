using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/// <summary>
/// An instance of the list of Projectileffects
/// </summary>
[
    CreateAssetMenu(
        fileName = "ProjectileEffects",
        menuName = "Player/ProjectileEffectSlots")
]
public class ProjectileEffectSlots : DescriptionBaseSO
{
    [SerializeField]
    private List<ProjectileEffectSO> _effects = new List<ProjectileEffectSO>();
    public List<ProjectileEffectSO> Effects => _effects;

    public event UnityAction<List<ProjectileEffectSO>> OnSlotsUpdated;

    [SerializeField]
    private ProjectileCountSO _projectileCount = default;
    [SerializeField]
    private VoidEventChannelSO _updateProjectileUI = default;


    public void UpdateSlots(List<ProjectileEffectSO> value)
	{
        if (_updateProjectileUI != null)
            _updateProjectileUI.RaiseEvent();
		if (OnSlotsUpdated != null)
			OnSlotsUpdated.Invoke(value);
	}

    public void SetEffects(List<ProjectileEffectSO> newValue)
    {
        _effects = newValue;
        UpdateSlots(_effects);
    }

    public void AddEffect(ProjectileEffectSO effect)
    {
        if (_projectileCount.MaxProjectileCount >= 8)
        {
            return;
        }
        int nextProjectileIndex = _projectileCount.MaxProjectileCount;
        ReplaceEffect(effect, nextProjectileIndex);
        _projectileCount.AddNewProjectiles(1);
        UpdateSlots(_effects);
        //_effects.Add(effect);
        //UpdateSlots(_effects);
    }

    public void ReplaceEffect(ProjectileEffectSO effect, int index)
    {
        if (_effects.Count >= index + 1) _effects[index] = effect;
        UpdateSlots(_effects);
    }

    public void RemoveEffectAt(int index)
    {
        if (_effects.Count >= index + 1) _effects.RemoveAt(index);
        UpdateSlots(_effects);
    }

    public void RemoveEffect(ProjectileEffectSO effect)
    {
        if (_effects.Contains(effect)) _effects.Remove(effect);
        UpdateSlots(_effects);
    }

    public void ClearEffects()
    {
        _effects.Clear();
        _effects =
            new List<ProjectileEffectSO> {
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>(),
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>(),
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>(),
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>(),
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>(),
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>(),
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>(),
                ScriptableObject
                    .CreateInstance<ProjectileEffectDefaultSO>()
            };
        UpdateSlots(_effects);
    }
}
