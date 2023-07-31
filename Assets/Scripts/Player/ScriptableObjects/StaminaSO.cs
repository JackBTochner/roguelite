using UnityEngine;
/// <summary>
/// An instance of the stamina.
/// </summary>
[CreateAssetMenu(fileName = "Stamina", menuName = "EntityConfig/Stamina")]
public class StaminaSO : ScriptableObject
{
    [Tooltip("The initial stamina")]
    [SerializeField] private float _initialStamina;
    [SerializeField] private float _maxStamina;
	[SerializeField] private float _currentStamina;
    [SerializeField] private float _regenRate;

    public float InitialStamina => _initialStamina;
	public float MaxStamina => _maxStamina;
	public float CurrentStamina => _currentStamina;
    public float RegenRate => _regenRate;

    public void SetMaxStamina(float newValue)
	{
		_maxStamina = newValue;
	}

	public void SetCurrentStamina(float newValue)
	{
		_currentStamina = newValue;
	}
	
	public void InflictDamage(float DamageValue)
	{
		_currentStamina -= DamageValue;
	}

	public void RestoreStamina(float HealthValue)
	{
		_currentStamina += HealthValue;
		if(_currentStamina > _maxStamina)
			_currentStamina = _maxStamina;
	}
}