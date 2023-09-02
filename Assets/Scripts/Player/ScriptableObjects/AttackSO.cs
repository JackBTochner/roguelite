using UnityEngine;

/// <summary>
/// An instance of Attack.
/// </summary>
[CreateAssetMenu(fileName = "Attack", menuName = "EntityConfig/Attack")]
public class AttackSO : ScriptableObject
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
}