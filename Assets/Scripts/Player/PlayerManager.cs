using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // [SerializeField]
    //private PlayerPerkSlots _playerPerks = default;
    //public PlayerPerkSlots PlayerPerks => _playerPerks;

    [SerializeField]
    private ProjectileEffectSlots _projectileEffects = default;
    public ProjectileEffectSlots ProjectileEffects => _projectileEffects;

    public HealthSO CurrentHealthSO = default;
    public ProjectileCountSO projectileCountSO = default;

    [Header("Broadcasting on")]
    [SerializeField]private PlayerManagerAnchor _playerManagerAnchor = default;

    [Header("Listening on")]
    [SerializeField] private RunManagerAnchor _runManagerAnchor = default;
	[SerializeField] private VoidEventChannelSO _onSceneReady = default; //Raised by SceneLoader when the scene is set to active
    [SerializeField] private VoidEventChannelSO _onReturnToHub = default;
    [SerializeField] private VoidEventChannelSO _onStartRun = default;
    /*
    [SerializeField] private PlayerGetAbilityEventChannel _playerGetPerk = default;
    [SerializeField] private PlayerReplaceAbilityEventChannel _playerReplacePerk = default;
    [SerializeField] private PlayerRemoveAbilityEventChannel _playerRemovePerk = default;
    */
    [SerializeField] private AddProjectileEventChannel _playerGetProjectile = default;
    [SerializeField] private ReplaceProjectileEventChannel _playerReplaceProjectile = default;
    [SerializeField] private IntEventChannel _playerRemoveProjectile = default;
    
    private void OnEnable()
    {
        _playerManagerAnchor.Provide(this);
        _onReturnToHub.OnEventRaised += SetInitialStats;
        _onStartRun.OnEventRaised += SetInitialStats;
        _onSceneReady.OnEventRaised += OnSceneReady;
        /*
        _playerGetPerk.OnEventRaised += GetPerkEventRaised;
        _playerReplacePerk.OnEventRaised += ReplacePerkEventRaised;
        _playerRemovePerk.OnEventRaised += RemovePerkAtEventRaised;
        */
        _playerGetProjectile.OnEventRaised += GetProjectileEventRaised;
        _playerReplaceProjectile.OnEventRaised += ReplaceProjectileEventRaised;
        _playerRemoveProjectile.OnEventRaised += RemoveProjectileAtEventRaised;
    }
    private void OnDisable()
    {
        _onReturnToHub.OnEventRaised -= SetInitialStats;
        _onStartRun.OnEventRaised -= SetInitialStats;
        _onSceneReady.OnEventRaised -= OnSceneReady;
        /*
        _playerGetPerk.OnEventRaised -= GetPerkEventRaised;
        _playerReplacePerk.OnEventRaised -= ReplacePerkEventRaised;
        _playerRemovePerk.OnEventRaised -= RemovePerkAtEventRaised;
        */
        _playerGetProjectile.OnEventRaised -= GetProjectileEventRaised;
        _playerReplaceProjectile.OnEventRaised -= ReplaceProjectileEventRaised;
        _playerRemoveProjectile.OnEventRaised -= RemoveProjectileAtEventRaised;
    }

    private void SetInitialStats()
    {
        CurrentHealthSO.SetCurrentHealth(CurrentHealthSO.InitialHealth);
        projectileCountSO.ResetProjectileCount();
        _projectileEffects.ClearEffects();
    }

    private void OnSceneReady()
    { 
        
    }

    private void GetProjectileEventRaised(ProjectileEffectSO newEffect)
    {
        _projectileEffects.AddEffect(newEffect);
    }
    
    private void ReplaceProjectileEventRaised(ProjectileEffectSO newEffect, int index)
    {
        _projectileEffects.ReplaceEffect(newEffect, index);
    }

    private void RemoveProjectileAtEventRaised(int index)
    {
        _projectileEffects.RemoveEffectAt(index);
    }

    private void GetPerkEventRaised()
    {

    }

    private void ReplacePerkEventRaised(int index)
    {

    }

    private void RemovePerkAtEventRaised(int index)
    { 

    }

}
