using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Transform weaponTrace;
    [SerializeField] Transform weaponPos;

    [SerializeField] WeaponType activeWeapon;

    [SerializeField] Weapon primary;
    [SerializeField] Weapon secondary;
    [SerializeField] Weapon melee;

    [SerializeField] int ammoPrimary;
    [SerializeField] int ammoSecondary;
    [SerializeField] float offSetDrop;

    public int AmmoPri { get { return ammoPrimary; } }
    public int AmmoSec { get { return ammoSecondary; } }
    public Weapon ActiveWeapon
    {
        get
        {
            switch (activeWeapon)
            {
                case WeaponType.Primary:
                    return primary;

                case WeaponType.Secondary:
                    return secondary;

                case WeaponType.Melee:
                    return melee;
            }

            return null;
        }
    }


    int LoopIndex(int index, int min, int max)
    {
        int result = index;

        if (result > max)
            result = min;
        else if (result < min)
            result = max;

        return result;
    }

    void SelectWeapon(WeaponType weapon)
    {
        if (CrossController.Instance)
            CrossController.Instance.ActiveWeaponCross(true);

        switch (activeWeapon)
        {
            case WeaponType.Primary:
                if (!primary)
                    break;

                primary.gameObject.SetActive(false);
                break;

            case WeaponType.Secondary:
                if (!secondary)
                    break;

                secondary.gameObject.SetActive(false);
                break;

            case WeaponType.Melee:
                if (!melee)
                    break;

                melee.gameObject.SetActive(false);
                break;
        }

        activeWeapon = weapon;

        switch (weapon)
        {
            case WeaponType.Primary:
                if (!primary)
                    break;

                primary.gameObject.SetActive(true);
                break;

            case WeaponType.Secondary:
                if (!secondary)
                    break;

                secondary.gameObject.SetActive(true);
                break;

            case WeaponType.Melee:
                if (!melee)
                    break;

                melee.gameObject.SetActive(true);
                break;

            default:

                activeWeapon = WeaponType.None;

                if (CrossController.Instance)
                    CrossController.Instance.ActiveDefaultCross(true);
                break;
        }
    }

    public void ChangeWeapon(int index)
    {
        if (index <= (int)WeaponType.Melee && index >= (int)WeaponType.None)
        {
            WeaponType selectedWeapon = (WeaponType)index;

            bool pass = false;

            int repeats = 0;

            while (!pass)
            {
                if (selectedWeapon != activeWeapon)
                    switch (selectedWeapon)
                    {
                        case WeaponType.Primary:
                            if (primary)
                                pass = true;
                            break;

                        case WeaponType.Secondary:
                            if (secondary)
                                pass = true;
                            break;

                        case WeaponType.Melee:
                            if (melee)
                                pass = true;
                            break;

                        case WeaponType.None:
                            pass = true;
                            break;
                    }

                if (!pass)
                {
                    index++;

                    if ((int)activeWeapon == index)
                        index++;

                    index = LoopIndex(index, (int)WeaponType.None, (int)WeaponType.Melee);
                    selectedWeapon = (WeaponType)index;
                    break;
                }

                repeats++;

                if (repeats > 6)
                    return;
            }

            SelectWeapon(selectedWeapon);
        }
        else
            Debug.LogError($"Objet: {this.gameObject.name}: Script {this.name}: Se trato de cambiar a un arma con indice {index}, el cual no existe.");
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

        if (equiped)
        {
            newWeapon.gameObject.layer = 3;
            newWeapon.SetSpawnPoint(weaponTrace);
            newWeapon.transform.SetParent(weaponPos, false);
            newWeapon.transform.position = weaponPos.position;
            newWeapon.transform.rotation = weaponPos.rotation;

            SelectWeapon(newWeapon.Type);
        }
    }

    [ContextMenu("Change Weapon Up")]
    public void ScrollUp()
    {
        int newIndex = (int)activeWeapon - 1;

        newIndex = LoopIndex(newIndex, (int)WeaponType.None, (int)WeaponType.Melee);

        ChangeWeapon(newIndex);
    }

    [ContextMenu("Change Weapon Down")]
    public void ScrollDown()
    {
        int newIndex = (int)activeWeapon + 1;

        newIndex = LoopIndex(newIndex, (int)WeaponType.None, (int)WeaponType.Melee);

        ChangeWeapon(newIndex);
    }

    public void FireWeapon()
    {
        switch (activeWeapon)
        {
            case WeaponType.Primary:
                if (primary)
                    primary.Fire();
                break;

            case WeaponType.Secondary:
                if (secondary)
                    secondary.Fire();
                break;

            case WeaponType.Melee:
                if (melee)
                    melee.Fire();
                break;

            default:
                break;
        }
    }

    public void ReloadWeapon()
    {
        switch (activeWeapon)
        {
            case WeaponType.Primary:
                if (primary)
                    ammoPrimary = primary.Reload(ammoPrimary);
                break;

            case WeaponType.Secondary:
                if (secondary)
                    ammoSecondary = secondary.Reload(ammoSecondary);
                break;
        }
    }

    public void DropWeapon()
    {
        Vector3 dropPos = weaponPos.position + weaponPos.forward * offSetDrop;

        switch (activeWeapon)
        {
            case WeaponType.Primary:

                if (!primary)
                    return;

                primary.transform.position = dropPos;
                primary.transform.rotation = Quaternion.LookRotation(transform.forward);

                primary.gameObject.layer = 0;
                primary.transform.SetParent(null, true);
                primary.OnDrop();

                primary = null;
                break;

            case WeaponType.Secondary:

                if (!secondary)
                    return;

                secondary.transform.position = dropPos;
                secondary.transform.rotation = Quaternion.LookRotation(transform.forward);

                secondary.gameObject.layer = 0;
                secondary.transform.SetParent(null, true);
                secondary.OnDrop();

                secondary = null;
                break;

            case WeaponType.Melee:

                if (!melee)
                    return;

                melee.transform.position = dropPos;
                melee.transform.rotation = Quaternion.LookRotation(transform.forward);

                melee.gameObject.layer = 0;
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
}
