using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInput _playerInput;
    [SerializeField]
    private PlayerInputAnchor _playerInputAnchor;

    void OnEnable()
    {
        if (!_playerInput) {
            _playerInput = GetComponent<PlayerInput>();
        }
        _playerInputAnchor.Provide(_playerInput);
    }
}
