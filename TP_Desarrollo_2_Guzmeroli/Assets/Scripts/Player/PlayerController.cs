using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Character))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject head;

    [SerializeField] InputActionAsset inputActions;
    [SerializeField] Character character;

    [SerializeField] ForceRequest forceRequest;

    [SerializeField] Vector2 Sens;

    [SerializeField] float force;
    [SerializeField] float jumpForce;
    [SerializeField] float speed;

    Vector3 dir3;

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
        move.canceled += CancelMove;

        look.started += StartLook;
        look.performed += StartLook;
        look.canceled += CancelLook;

        forceRequest = new ForceRequest();
    }

    void Update()
    {
        ActualizeDirection();
    }

    [ContextMenu("LockMouse")]
    void LockMouse()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }


    void StartLook(InputAction.CallbackContext cont)
    {
        character.MoveHeadRequest(look.ReadValue<Vector2>(), Sens, head);
    }

    void Jump(InputAction.CallbackContext cont)
    {
        if (!character.OnFloor)
            return;

        ForceRequest request = new ForceRequest(Vector3.up, jumpForce, speed);
        forceRequest.speed = speed;
        character.InstantForceRequest(request);
    }

    void Move(InputAction.CallbackContext cont)
    {
        forceRequest.force = force;
        forceRequest.speed = speed;
        character.ConstantForceRequest(forceRequest);
    }

    void ActualizeDirection()
    {
        Vector2 dir = move.ReadValue<Vector2>();

        dir3 = transform.right * dir.x + transform.forward * dir.y;

        forceRequest.direction.x = dir3.x;
        forceRequest.direction.z = dir3.z;
    }

    void CancelMove(InputAction.CallbackContext cont)
    {
        character.CancelForce();
    }

    void CancelLook(InputAction.CallbackContext cont)
    {
        character.CancelMoveHead();
    }
}
