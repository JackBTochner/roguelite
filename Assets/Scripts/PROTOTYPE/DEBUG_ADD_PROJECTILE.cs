using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_ADD_PROJECTILE : MonoBehaviour
{
    [SerializeField] private AddProjectileEventChannel _playerGetProjectile = default;
    [SerializeField] private ReplaceProjectileEventChannel _playerReplaceProjectile = default;
    [SerializeField] private IntEventChannel _playerRemoveProjectile = default;

    [SerializeField] private ProjectileEffectSO effectToGive;

    public void AddProjectile()
    {
        _playerGetProjectile.RaiseEvent(effectToGive);
    }
}
