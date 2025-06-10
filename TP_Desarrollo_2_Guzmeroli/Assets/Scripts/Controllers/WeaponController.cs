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
            return GetWeaponByType(activeWeapon);
        }

        private set
        {
            switch (activeWeapon)
            {
                case WeaponType.Primary:
                    primary = value;
                    break;

                case WeaponType.Secondary:
                    secondary = value;
                    break;

                case WeaponType.Melee:
                    melee = value;
                    break;
            }
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

    Weapon GetWeaponByType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Primary:
                return primary;

            case WeaponType.Secondary:
                return secondary;

            case WeaponType.Melee:
                return melee;

            default:
                return null;
        }
    }

    void SelectWeapon(WeaponType weapon)
    {
        if (CrossController.Instance)
            CrossController.Instance.ActiveWeaponCross(true);

        Weapon current = GetWeaponByType(activeWeapon);

        if(current)
            current.gameObject.SetActive(false);

        activeWeapon = weapon;

        Weapon next = GetWeaponByType(weapon);

        if (next)
            next.gameObject.SetActive(true);
        else
        {
            activeWeapon = WeaponType.None;

            if (CrossController.Instance)
                CrossController.Instance.ActiveDefaultCross(true);
        }
    }

    bool IsValidWeapon(WeaponType weapon)
    {
        switch (weapon)
        {
            case WeaponType.Primary:
                return primary;

            case WeaponType.Secondary:
                return secondary;

            case WeaponType.Melee:
                return melee;

            case WeaponType.None:
                return true;

            default:
                return false;
        }
    }

    public void ChangeWeapon(int index, bool rest = false)
    {
        if (index > (int)WeaponType.Melee || index < (int)WeaponType.None)
        {
            Debug.LogError($"Objet: {this.gameObject.name}: Script {this.name}: Se trato de cambiar a un arma con indice {index}, el cual no existe.");
            return;
        }

        int repeats = 0;
        int maxRepeats = System.Enum.GetValues(typeof(WeaponType)).Length;

        while (repeats < maxRepeats)
        {
            WeaponType selectedWeapon = (WeaponType)LoopIndex(index, (int)WeaponType.None, (int)WeaponType.Melee);

            if (selectedWeapon != activeWeapon && IsValidWeapon(selectedWeapon))
            {
                SelectWeapon(selectedWeapon);
                return;
            }

            if (rest)
                index--;
            else
                index++;

            repeats++;
        }

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

        ChangeWeapon(newIndex, true);
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
        if (!ActiveWeapon)
            return;

        ActiveWeapon.Fire();
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
        if (!ActiveWeapon)
            return;

        Vector3 dropPos = weaponPos.position + weaponPos.forward * offSetDrop;

        ActiveWeapon.transform.position = dropPos;
        ActiveWeapon.transform.rotation = Quaternion.LookRotation(transform.forward);

        ActiveWeapon.gameObject.layer = 0;
        ActiveWeapon.transform.SetParent(null, true);
        ActiveWeapon.OnDrop();

        ActiveWeapon = null;

        SelectWeapon(WeaponType.None);
    }
}
