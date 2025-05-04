using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] int activeWeapon;

    [SerializeField] Weapon primary;
    [SerializeField] Weapon secondary;
    [SerializeField] Weapon melee;

    [SerializeField] int ammoPrimary;
    [SerializeField] int ammoSecondary;
}
