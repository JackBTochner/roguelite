using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : DescriptionBaseSO, Controls.IPlayerActions
{
    public Vector2 Look; // Gamepad look

    public Vector2 MousePosition;

    public Vector2 MoveComposite;

    public Action OnJumpPerformed;

    public Action OnRestartPerformed;

    public Action OnDigPerformed;

    public Action OnAttack1Performed;

    public Action OnAttack2Performed;

    public Action OnDashPerformed;

    public Controls controls;

    public void OnEnable()
    {
        if (controls != null) return;

        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    public void OnDisable()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnJumpPerformed?.Invoke();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        OnRestartPerformed?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveComposite = context.ReadValue<Vector2>();
    }

    public void OnDig(InputAction.CallbackContext context)
    { 
        if (!context.performed) return;
        OnDigPerformed?.Invoke();
    }
    
    public void OnAttack1(InputAction.CallbackContext context)
    { 
        if (!context.performed) return;
        OnAttack1Performed?.Invoke();
    }
     public void OnAttack2(InputAction.CallbackContext context)
    { 
        if (!context.performed) return;
        OnAttack2Performed?.Invoke();
    }
    public void OnDash(InputAction.CallbackContext context)
    { 
        if (!context.performed) return;
        OnDashPerformed?.Invoke();
    }
}
