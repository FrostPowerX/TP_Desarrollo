using UnityEngine;
using UnityEngine.InputSystem;


public class WeaponController : MonoBehaviour
{
    [SerializeField] Transform weaponPos;

    [SerializeField] WeaponType activeWeapon;

    [SerializeField] Weapon primary;
    [SerializeField] Weapon secondary;
    [SerializeField] Weapon melee;

    [SerializeField] int ammoPrimary;
    [SerializeField] int ammoSecondary;

    [SerializeField] InputActionAsset actionMap;

    InputAction fire;
    InputAction drop;

    void Awake()
    {
        actionMap.Enable();

        fire = actionMap.FindAction("Attack");
        drop = actionMap.FindAction("Drop");

        fire.started += FireWeapon;
        drop.started += DropWeapon;
    }

    private void FireWeapon(InputAction.CallbackContext context)
    {
        switch (activeWeapon)
        {
            case WeaponType.Primary:
                primary.Fire();
                break;

            case WeaponType.Secondary:
                secondary.Fire();
                break;

            case WeaponType.Melee:
                melee.Fire();
                break;

            default:
                break;
        }
    }

    void SelectWeapon(WeaponType weapon)
    {
        switch (activeWeapon)
        {
            case WeaponType.Primary:
                if (!primary)
                    return;

                primary.gameObject.SetActive(false);
                break;

            case WeaponType.Secondary:
                if (!secondary)
                    return;

                secondary.gameObject.SetActive(false);
                break;

            case WeaponType.Melee:
                if (!melee)
                    return;

                melee.gameObject.SetActive(false);
                break;
        }

        activeWeapon = weapon;

        switch (weapon)
        {
            case WeaponType.Primary:
                if (!primary)
                    return;

                primary.gameObject.SetActive(true);
                break;

            case WeaponType.Secondary:
                if (!secondary)
                    return;

                secondary.gameObject.SetActive(true);
                break;

            case WeaponType.Melee:
                if (!melee)
                    return;

                melee.gameObject.SetActive(true);
                break;
        }
    }

    void DropWeapon(InputAction.CallbackContext context)
    {
        switch (activeWeapon)
        {
            case WeaponType.Primary:

                primary.transform.SetParent(null, false);
                primary = null;
                break;

            case WeaponType.Secondary:

                secondary.transform.SetParent(null, false);
                secondary = null;
                break;

            case WeaponType.Melee:

                melee.transform.SetParent(null, false);
                melee = null;
                break;

            default:
                break;
        }

        if (primary)
            SelectWeapon(WeaponType.Primary);
        else if (secondary)
            SelectWeapon(WeaponType.Secondary);
        else if (melee)
            SelectWeapon(WeaponType.Melee);
        else
            SelectWeapon(WeaponType.None);
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        switch (newWeapon.Type)
        {
            case WeaponType.Primary:
                if (primary)
                    return;

                primary = newWeapon;
                primary.transform.SetParent(weaponPos, true);
                primary.transform.position = Vector3.zero;
                break;

            case WeaponType.Secondary:
                if (secondary)
                    return;

                secondary = newWeapon;
                secondary.transform.SetParent(weaponPos, true);
                secondary.transform.position = Vector3.zero;
                break;

            case WeaponType.Melee:
                if (melee)
                    return;

                melee = newWeapon;
                melee.transform.SetParent(weaponPos, true);
                melee.transform.position = Vector3.zero;
                break;
        }
    }
}
