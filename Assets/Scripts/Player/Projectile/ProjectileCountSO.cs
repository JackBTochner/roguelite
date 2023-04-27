using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An instance of the Proejctile count of the PlayerWeaponController
/// </summary>
[CreateAssetMenu(fileName = "ProjectileCount", menuName = "EntityConfig/ProjectileCount")]
public class ProjectileCountSO : DescriptionBaseSO
{
    [Tooltip("The initial health")]
    [SerializeField] private int _initialProjectileCount = 8;
    [SerializeField] private int _maxProjectileCount = 8;
	[SerializeField] private int _currentProjectileCount = 8;

    public int InitialProjectileCount => _initialProjectileCount;
    public int MaxProjectileCount => _maxProjectileCount;
    public int CurrentProjectileCount => _currentProjectileCount;

    public void SetMaxProjectiles(int newValue)
	{
		_maxProjectileCount = newValue;
	}

	public void SetCurrentProjectiles(int newValue)
	{
        if(newValue < _maxProjectileCount)
		    _currentProjectileCount = newValue;
        else
            RestoreProjectileCount();
    }
	
	public void RemoveCurrentProjectiles(int value)
	{
        if(_currentProjectileCount - value <= 0)
            _currentProjectileCount = 0;
        else
            _currentProjectileCount -= value;
	}

    public void AddCurrentProjectiles(int value)
	{
        if(_currentProjectileCount < _maxProjectileCount)
		    _currentProjectileCount += value;
        else
            RestoreProjectileCount();
	}

    public void AddNewProjectiles(int value)
    {
        SetMaxProjectiles(value);
        SetCurrentProjectiles(value);
    }

    public void RestoreProjectileCount()
    {
        _currentProjectileCount = _maxProjectileCount;
    }

    public void ResetProjectileCount()
    {
        SetMaxProjectiles(_initialProjectileCount);
        SetCurrentProjectiles(_initialProjectileCount);
    }
}
