using UnityEngine;

/// <summary>
/// An instance of health.
/// </summary>
[CreateAssetMenu(fileName = "Health", menuName = "EntityConfig/Health")]
public class HealthSO : ScriptableObject
{
    [Tooltip("The initial health")]
    [SerializeField] private int _initialHealth;
    [SerializeField] private int _maxHealth;
	[SerializeField] private int _currentHealth;

	public int InitialHealth => _initialHealth;
	public int MaxHealth => _maxHealth;
	public int CurrentHealth => _currentHealth;

    public void SetMaxHealth(int newValue)
	{
		Debug.LogError("SetMaxHealth" + newValue);
		_maxHealth = newValue;
	}

    public void PrintCurrentHealth()
    {
        Debug.LogError("Current Health: " + _currentHealth);
    }

    public int GetCurrentestHealth()
    {
        return _currentHealth;
    }

    public void SetCurrentHealth(int newValue)
	{
        Debug.LogError("SetCurrentHealth" + newValue);
        _currentHealth = newValue;
	}
	
	public void InflictDamage(int DamageValue)
	{
		Debug.LogError("InflictDamage" + DamageValue);
		_currentHealth -= DamageValue;
	}

	public void RestoreHealth(int HealthValue)
	{
		_currentHealth += HealthValue;
		if(_currentHealth > _maxHealth)
			_currentHealth = _maxHealth;
	}
}
