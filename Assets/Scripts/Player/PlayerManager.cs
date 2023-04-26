using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // [SerializeField]
    // PlayerAbilitiesSO
    // [Header("Listening On")]
    // [SerializeField] private PlayerGetAbilityEventChannel _playerGetAbility
    // [SerializeField] private PlayerRemoveAbilityEventChannel _playerRemoveAbility

    public HealthSO CurrentHealthSO = default;

    [Header("Broadcasting on")]
    [SerializeField]private PlayerManagerAnchor _playerManagerAnchor = default;

    [Header("Listening on")]
    [SerializeField] private RunManagerAnchor _runManagerAnchor = default;
	[SerializeField] private VoidEventChannelSO _onSceneReady = default; //Raised by SceneLoader when the scene is set to active
    [SerializeField] private VoidEventChannelSO _onReturnToHub = default;

    private void OnEnable()
    {
        _playerManagerAnchor.Provide(this);
        _onReturnToHub.OnEventRaised += SetInitialStats;
        
        // _playerGetAbility.OnEventRaised += GetAbilityEventRaised
        //_playerRemoveAbility.OnEventRaised += RemoveAbilityEventRaised
    }
    private void OnDisable()
    {
        _onReturnToHub.OnEventRaised -= SetInitialStats;
        // _playerGetAbility.OnEventRaised -= GetAbilityEventRaised
        // _playerRemoveAbility.OnEventRaised -= RemoveAbilityEventRaised
    }

    private void SetInitialStats()
    { 
        CurrentHealthSO.SetCurrentHealth(CurrentHealthSO.InitialHealth);
    }

    private void GetAbilityEventRaised()
    { 

    }

    private void RemoveAbilityEventRaised()
    { 

    }

}
