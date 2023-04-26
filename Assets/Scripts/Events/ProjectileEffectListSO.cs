using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// An instance of the list of Projectileffects
/// </summary>
[CreateAssetMenu(fileName = "PlayersHealth", menuName = "EntityConfig/Player's Health")]
public class ProjectileEffectListSO : DescriptionBaseSO
{
	[SerializeField] private List<PlayerProjectileEffectSO> _effects = new List<PlayerProjectileEffectSO>();
    
	[Tooltip("The initial health")]
    [SerializeField] private int _initialHealth;
    [SerializeField] private int _maxHealth;
	[SerializeField] private int _currentHealth;

    public List<PlayerProjectileEffectSO> Effects => _effects;
    public int InitialHealth => _initialHealth;
	public int MaxHealth => _maxHealth;
	public int CurrentHealth => _currentHealth;

    public void SetEffects(List<PlayerProjectileEffectSO> newValue)
    {
        _effects = newValue;
    }
    public void AddEffect(PlayerProjectileEffectSO effect)
    {
        _effects.Add(effect);
    }
    public void RemoveEffectAt(int index)
    {
		if(_effects.Count >= index + 1)
        	_effects.RemoveAt(index);
    }
    public void RemoveEffect(PlayerProjectileEffectSO effect)
    { 
		if(_effects.Contains(effect))
            _effects.Remove(effect);
    }
    public void ClearEffects()
    {
        _effects.Clear();
    }
}
