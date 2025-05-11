using System;
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

    void Awake()
    {
        inputActions.Enable();

        move = inputActions.FindAction("Move");
        jump = inputActions.FindAction("Jump");
        look = inputActions.FindAction("Look");

        jump.started += Jump;

        move.started += Move;
        move.started += ActualizeDirectionMovement;
        move.canceled += CancelMove;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        look.started += StartLook;
        look.performed += StartLook;
        look.canceled += CancelLook;

        forceRequest = new ForceRequest();
    }

    void Update()
    {
        ActualizeDirection();
=======
        look.performed += ActualizeDirectionMovement;
        look.performed += Look;

        Cursor.lockState = CursorLockMode.Locked;
>>>>>>> Stashed changes
=======
        look.performed += ActualizeDirectionMovement;
        look.performed += Look;

        Cursor.lockState = CursorLockMode.Locked;
>>>>>>> Stashed changes
    }

    [ContextMenu("LockMouse")]
    void LockMouse()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

<<<<<<< Updated upstream
<<<<<<< Updated upstream

    void StartLook(InputAction.CallbackContext cont)
    {
        character.MoveHeadRequest(look.ReadValue<Vector2>(), Sens, head);
    }

=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    void Jump(InputAction.CallbackContext cont)
    {
        if (!OnFloor)
            return;

        InstantForceRequest();
    }

    void Move(InputAction.CallbackContext cont)
    {
        ConstantForceRequest();
<<<<<<< Updated upstream
    }

<<<<<<< Updated upstream
    void ActualizeDirection()
=======
    void CancelMove(InputAction.CallbackContext cont)
    {
        CancelForce();
    }

    void ActualizeDirectionMovement(InputAction.CallbackContext cont)
>>>>>>> Stashed changes
    {
        Vector2 moveDir = move.ReadValue<Vector2>();

        direction = transform.right * moveDir.x + transform.forward * moveDir.y;

    }

    void Look(InputAction.CallbackContext cont)
    {
        lookProcces.Look(look.ReadValue<Vector2>(), sensivility, gameObject);
=======
>>>>>>> Stashed changes
    }

    void CancelMove(InputAction.CallbackContext cont)
    {
        CancelForce();
    }

<<<<<<< Updated upstream
    void CancelLook(InputAction.CallbackContext cont)
    {
        character.CancelMoveHead();
=======
    void ActualizeDirectionMovement(InputAction.CallbackContext cont)
    {
        Vector2 moveDir = move.ReadValue<Vector2>();

        direction = transform.right * moveDir.x + transform.forward * moveDir.y;

    }

    void Look(InputAction.CallbackContext cont)
    {
        lookProcces.Look(look.ReadValue<Vector2>(), sensivility, gameObject);
>>>>>>> Stashed changes
    }
}
