using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerInputController : MonoBehaviour
{
    /*public InputMaster controls;
 
    void Awake() {
        controls = new InputMaster();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    } --- Consider changing into this*/


    public delegate void Input_OnMoveDelegate(Vector2 movementDirection);
    public static Input_OnMoveDelegate input_OnMoveDelegate;

    public delegate void Input_OnMoveTowardsMouseDelegate(bool isMovingTowardsMouse);
    public static Input_OnMoveTowardsMouseDelegate input_OnMoveTowardsMouseDelegate;

    public delegate void Input_OnSprintDelegate(bool isSprinting);
    public static Input_OnSprintDelegate input_OnSprintDelegate;

    public delegate void Input_OnDrawWeaponDelegate();
    public static Input_OnDrawWeaponDelegate input_OnDrawWeaponDelegate;

    public delegate void Input_OnSpinCameraDelegate(int direction);
    public static Input_OnSpinCameraDelegate input_OnSpinCameraDelegate;

    public delegate void Input_OnFireDelegate();
    public static Input_OnFireDelegate input_OnFireDelegate;

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 movementDirection = context.ReadValue<Vector2>();
        input_OnMoveDelegate(movementDirection);
    }

    public void OnMoveTowardsMouse(InputAction.CallbackContext context)
    {
        if (context.interaction is HoldInteraction && context.canceled)
        {
            input_OnMoveTowardsMouseDelegate(false);
            return;
        }
        if (!context.performed)
            return;
        if (context.interaction is MultiTapInteraction)
            input_OnMoveTowardsMouseDelegate(true);
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        input_OnSprintDelegate(context.started || context.performed);
    }

    public void OnDrawWeapon(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            input_OnDrawWeaponDelegate();
        }
    }

    public void OnTurnCamera(InputAction.CallbackContext context)
    {
        if (!context.performed || context.canceled)
            return;
        int direction = Mathf.RoundToInt(context.ReadValue<float>());
        input_OnSpinCameraDelegate(direction);
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (InputActionPhase.Started == context.phase)
        {
            input_OnFireDelegate();
        }
    }

}
