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
    [SerializeField] float offSetDrop;

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

    void FireWeapon(InputAction.CallbackContext context)
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

    void DropWeapon(InputAction.CallbackContext context)
    {
        Vector3 dropPos = weaponPos.position + weaponPos.forward * offSetDrop;

        switch (activeWeapon)
        {
            case WeaponType.Primary:

                primary.transform.position = dropPos;
                primary.transform.rotation = Quaternion.LookRotation(transform.forward);

                primary.transform.SetParent(null, true);
                primary.OnDrop();

                primary = null;
                break;

            case WeaponType.Secondary:

                secondary.transform.position = dropPos;
                secondary.transform.rotation = Quaternion.LookRotation(transform.forward);

                secondary.transform.SetParent(null, true);
                secondary.OnDrop();

                secondary = null;
                break;

            case WeaponType.Melee:

                melee.transform.position = dropPos;
                melee.transform.rotation = Quaternion.LookRotation(transform.forward);

                melee.transform.SetParent(null, true);
                melee.OnDrop();

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

    void ScrollUp()
    {
        WeaponType actualWeapon = activeWeapon;

        if (actualWeapon == WeaponType.Melee)
            SelectWeapon(WeaponType.Primary);
        else
            SelectWeapon((WeaponType)((int)actualWeapon + 1));
    }

    void ScrollDown()
    {
        WeaponType actualWeapon = activeWeapon;

        if (actualWeapon == WeaponType.Primary)
            SelectWeapon(WeaponType.Melee);
        else
            SelectWeapon((WeaponType)((int)actualWeapon - 1));
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        bool equiped = false;

        switch (newWeapon.Type)
        {
            case WeaponType.Primary:
                if (primary)
                    return;

                primary = newWeapon;
                equiped = true;
                break;

            case WeaponType.Secondary:
                if (secondary)
                    return;

                secondary = newWeapon;
                equiped = true;
                break;

            case WeaponType.Melee:
                if (melee)
                    return;

                melee = newWeapon;
                equiped = true;
                break;
        }

        if(equiped)
        {
            newWeapon.transform.SetParent(weaponPos, false);
            newWeapon.transform.position = weaponPos.position;
            newWeapon.transform.rotation = weaponPos.rotation;
        }
    }
}
