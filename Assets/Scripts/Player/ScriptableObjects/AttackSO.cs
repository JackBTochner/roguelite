using UnityEngine;

/// <summary>
/// An instance of Attack.
/// </summary>
[CreateAssetMenu(fileName = "Attack", menuName = "EntityConfig/Attack")]
public class AttackSO : DescriptionBaseSO
{
    [Tooltip("The Attack")]
    [SerializeField] private bool _isAttack;

    public bool IsAttack => _isAttack;
    
    public void Reset()
    {
        _isAttack = true;
    }
    
    public bool GetIsAttack()
    {
        return _isAttack;
    }
    
    public void SetAttack(bool value)
    {
        _isAttack = value;
    }
}