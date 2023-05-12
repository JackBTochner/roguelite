using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DEBUG_ADD_PROJECTILE : MonoBehaviour
{
    [SerializeField] private AddProjectileEventChannel _playerGetProjectile = default;
    [SerializeField] private ReplaceProjectileEventChannel _playerReplaceProjectile = default;
    [SerializeField] private IntEventChannel _playerRemoveProjectile = default;

    [SerializeField] private ProjectileEffectSO effectToGive;

    [SerializeField] private LootTableProjectile projectileLootTable;
    [SerializeField] private TextMeshProUGUI buttonText;

    private void OnEnable()
    { 
        effectToGive = projectileLootTable.GetRandomWeightedProjectileEffect();
        buttonText.text = effectToGive.name;
    }

    public void AddProjectile()
    {
        _playerGetProjectile.RaiseEvent(effectToGive);
    }
}
