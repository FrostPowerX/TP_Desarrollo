using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] GameObject inventoryInfo;

    [SerializeField] TMP_Text healthInfo;

    [SerializeField] TMP_Text weaponAmmoInfo;
    [SerializeField] TMP_Text playerAmmoInfo;

    [SerializeField] TMP_Text timeLeftInfo;

    HealthSystem hs;
    WeaponController wp;
    Inventory inventory;

    private void Start()
    {
        if (target)
        {
            hs = target.GetComponent<HealthSystem>();
            wp = target.GetComponent<WeaponController>();
            inventory = target.GetComponent<Inventory>();
        }
    }

    private void Update()
    {
        UpdateHealth();
        UpdateInventory();
        UpdateTime();
        UpdateWeapons();
    }

    void UpdateHealth()
    {
        if (!healthInfo || !hs)
            return;

        healthInfo.text = $"{hs.Health} / {hs.MaxHealth}";
    }

    void UpdateWeapons()
    {
        if (!weaponAmmoInfo || !wp || !playerAmmoInfo)
            return;

        Weapon weapon = wp.ActiveWeapon;

        if (weapon)
        {
            if (weapon.Type == WeaponType.Primary)
                playerAmmoInfo.text = $"{wp.AmmoPri}";
            else if (weapon.Type == WeaponType.Secondary)
                playerAmmoInfo.text = $"{wp.AmmoSec}";
            else
                playerAmmoInfo.text = "";

            weaponAmmoInfo.text = $"{weapon.Ammo} / {weapon.MaxAmmo}";
        }
        else
            weaponAmmoInfo.text = "Hand";

    }

    void UpdateInventory()
    {
        if (!inventoryInfo || !inventory)
            return;


    }

    void UpdateTime()
    {
        if (!timeLeftInfo)
            return;


    }

    public void SetTarget(GameObject target) => this.target = target;
}
