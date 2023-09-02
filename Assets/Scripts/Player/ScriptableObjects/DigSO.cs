using UnityEngine;

/// <summary>
/// An instance of Dig.
/// </summary>
[CreateAssetMenu(fileName = "Dig", menuName = "EntityConfig/Dig")]
public class DigSO : DescriptionBaseSO
{
    [Tooltip("The Dig")]
    [SerializeField] private bool _isDigging;

    public bool IsDigging => _isDigging;
    
    public void Reset()
    {
        _isDigging = true;
    }
    
    public bool GetIsDigging()
    {
        return _isDigging;
    }
    
    public void SetDig(bool value)
    {
        _isDigging = value;
    }
}