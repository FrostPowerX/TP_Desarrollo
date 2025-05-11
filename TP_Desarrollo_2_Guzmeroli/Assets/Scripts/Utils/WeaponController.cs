using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [SerializeField] int activeWeapon;

    [SerializeField] Weapon primary;
    [SerializeField] Weapon secondary;
    [SerializeField] Weapon melee;

    [SerializeField] int ammoPrimary;
    [SerializeField] int ammoSecondary;

    [SerializeField] InputActionAsset actionMap;

    Action drop;

}
