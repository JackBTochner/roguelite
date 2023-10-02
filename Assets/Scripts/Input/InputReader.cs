using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : DescriptionBaseSO, Controls.IPlayerActions
{
    // PLAYER CONTROLS
    public Vector2 Look; // Gamepad look

    public Vector2 MousePosition;

    public Vector2 MoveComposite;

    public Action OnJumpPerformed;
    public Action OnJumpCancelled;

    public Action OnRestartPerformed;

    public Action OnDigPerformed;
    public Action OnDigCancelled;

    public Action OnAttack1Performed;
    public Action OnAttack1Cancelled;

    public Action OnAttack2Performed;
    public Action OnAttack2Cancelled;

    public Action OnDashPerformed;
    public Action OnDashCancelled;

    public Action OnInteractPerformed;
    public Action OnInteractCancelled;

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
        if (context.performed)
            OnJumpPerformed?.Invoke();
        if (context.canceled)
            OnJumpCancelled?.Invoke();
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (context.performed)
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
        if (context.performed)
            OnDigPerformed?.Invoke();
        if (context.canceled)
            OnDigCancelled?.Invoke();
    }
    
    public void OnAttack1(InputAction.CallbackContext context)
    { 
        if (context.performed)
            OnAttack1Performed?.Invoke();
        if (context.canceled)
            OnAttack1Cancelled?.Invoke();
    }
     public void OnAttack2(InputAction.CallbackContext context)
    { 
        if (context.performed)
            OnAttack2Performed?.Invoke();
        if (context.canceled)
            OnAttack2Cancelled?.Invoke();
    }
    public void OnDash(InputAction.CallbackContext context)
    { 
        if (context.performed)
            OnDashPerformed?.Invoke();
        if (context.canceled)
            OnDashCancelled?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
            OnInteractPerformed?.Invoke();
        if (context.canceled)
            OnInteractCancelled?.Invoke();
    }
}
