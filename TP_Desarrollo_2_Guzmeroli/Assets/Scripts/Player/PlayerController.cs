using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Character
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] LookProcces lookProcces;

    [SerializeField] Vector2 sensivility;

    InputAction move;
    InputAction jump;
    InputAction look;

    private void Awake()
    {
        inputActions.Enable();

        move = inputActions.FindAction("Move");
        jump = inputActions.FindAction("Jump");
        look = inputActions.FindAction("Look");

        jump.started += Jump;

        move.started += Move;
        move.started += ActualizeDirectionMovement;
        move.canceled += CancelMove;

        look.performed += ActualizeDirectionMovement;
        look.performed += Look;

        Cursor.lockState = CursorLockMode.Locked;
    }

    [ContextMenu("LockMouse")]
    void LockMouse()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    void Jump(InputAction.CallbackContext cont)
    {
        if (!OnFloor)
            return;

        InstantForceRequest();
    }

    void Move(InputAction.CallbackContext cont)
    {
        ConstantForceRequest();
    }

    void CancelMove(InputAction.CallbackContext cont)
    {
        CancelForce();
    }

    void ActualizeDirectionMovement(InputAction.CallbackContext cont)
    {
        Vector2 moveDir = move.ReadValue<Vector2>();

        direction = transform.right * moveDir.x + transform.forward * moveDir.y;

    }

    void Look(InputAction.CallbackContext cont)
    {
        lookProcces.Look(look.ReadValue<Vector2>(), sensivility, gameObject);
    }
}
