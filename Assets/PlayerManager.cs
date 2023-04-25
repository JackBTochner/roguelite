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

    private void OnEnable()
    {
        // _playerGetAbility.OnEventRaised += GetAbilityEventRaised
        //_playerRemoveAbility.OnEventRaised += RemoveAbilityEventRaised
    }
    private void OnDisable()
    {
        // _playerGetAbility.OnEventRaised -= GetAbilityEventRaised
        // _playerRemoveAbility.OnEventRaised -= RemoveAbilityEventRaised
    }

    private void GetAbilityEventRaised()
    { 

    }

    private void RemoveAbilityEventRaised()
    { 

    }

}
