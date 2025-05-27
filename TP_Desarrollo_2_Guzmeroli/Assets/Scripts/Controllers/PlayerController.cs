using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponController))]
public class PlayerController : Character
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] LookProcces lookProcces;
    [SerializeField] WeaponController wpController;

    [SerializeField] Vector2 sensivility;

    InputAction move;
    InputAction jump;
    InputAction look;
    InputAction fire;
    InputAction drop;
    InputAction reload;
    InputAction scroll;

    private void Awake()
    {
        inputActions.Enable();

        move = inputActions.FindAction("Move");
        jump = inputActions.FindAction("Jump");
        look = inputActions.FindAction("Look");
        fire = inputActions.FindAction("Fire");
        drop = inputActions.FindAction("Drop");
        reload = inputActions.FindAction("Reload");
        scroll = inputActions.FindAction("Scroll");

        jump.started += Jump;

        move.started += Move;
        move.canceled += CancelMove;

        look.performed += Look;

        fire.started += FireWeapon;
        drop.started += DropWeapon;
        reload.started += ReloadWeapon;

        scroll.performed += ScrollWeapon;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        inputActions.Disable();

        jump.started -= Jump;

        move.started -= Move;
        move.canceled -= CancelMove;

        look.performed -= Look;

        fire.started -= FireWeapon;
        drop.started -= DropWeapon;
        reload.started -= ReloadWeapon;

        scroll.performed -= ScrollWeapon;

        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        ActualizeDirectionMovement();
    }

    void Jump(InputAction.CallbackContext cont)
    {
        if (!OnFloor)
            return;

        InstantForceRequest(Vector3.up);
    }

    void Move(InputAction.CallbackContext cont)
    {
        ConstantForceRequest();
    }

    void CancelMove(InputAction.CallbackContext cont)
    {
        CancelForce();
    }

    void FireWeapon(InputAction.CallbackContext cont)
    {
        if (wpController)
            wpController.FireWeapon();
    }

    void DropWeapon(InputAction.CallbackContext cont)
    {
        if (wpController)
            wpController.DropWeapon();
    }

    void ReloadWeapon(InputAction.CallbackContext cont)
    {
        if (wpController)
            wpController.ReloadWeapon();
    }

    void ScrollWeapon(InputAction.CallbackContext context)
    {
        if (wpController)
        {
            if (scroll.ReadValue<Vector2>().y == 1)
                wpController.ScrollUp();
            else if (scroll.ReadValue<Vector2>().y == -1)
                wpController.ScrollDown();
        }
    }

    void ActualizeDirectionMovement()
    {
        Vector2 moveDir = move.ReadValue<Vector2>();

        direction = transform.right * moveDir.x + transform.forward * moveDir.y;
    }

    void Look(InputAction.CallbackContext cont)
    {
        lookProcces.Look(look.ReadValue<Vector2>(), sensivility, gameObject);
    }
}
