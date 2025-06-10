using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject target;

    [SerializeField] List<TMP_Text> inventoryInfoSlots;

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

            weaponAmmoInfo.text = $"{weapon.Ammo} / {weapon.MaxAmmo}";

            if (weapon.Type == WeaponType.Melee)
            {
                playerAmmoInfo.text = "";
                weaponAmmoInfo.text = $"{weapon.Type.ToString()}";
            }
        }
        else
        {
            playerAmmoInfo.text = "";
            weaponAmmoInfo.text = "Hand";
        }

    }

    void UpdateInventory()
    {
        if (inventoryInfoSlots.Count <= 0)
            return;

        inventoryInfoSlots[0].text = inventory.FindItemByName("Carrot").Count.ToString();
        inventoryInfoSlots[1].text = inventory.FindItemByName("CarrotSoup").Count.ToString();
        inventoryInfoSlots[2].text = inventory.FindItemByName("Parsnip").Count.ToString();
        inventoryInfoSlots[3].text = inventory.FindItemByName("PeaSoup").Count.ToString();
        inventoryInfoSlots[4].text = inventory.FindItemByName("Steak").Count.ToString();
        inventoryInfoSlots[5].text = inventory.FindItemByName("SteakCooked").Count.ToString();
    }

    public void UpdateTime(float time)
    {
        if (!timeLeftInfo)
            return;

        if (time < 60f)
            timeLeftInfo.text = $"00:{time:00.0}";
        else
        {
            int mins = Mathf.FloorToInt(time / 60f);
            float secondsRest = time % 60f;

            timeLeftInfo.text = $"{mins:00}:{secondsRest:00.0}";
        }
    }

    public void SetTarget(GameObject target) => this.target = target;
}
