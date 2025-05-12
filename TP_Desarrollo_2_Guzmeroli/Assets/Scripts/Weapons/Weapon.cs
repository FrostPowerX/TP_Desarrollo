using System.Collections.Generic;
using UnityEngine;

public enum WeaponFireType
{
    None,
    Ray,
    Spawn
}

public enum AmmoType
{
    None,
    primary,
    secondary
}

public enum WeaponType
{
    None,
    Primary,
    Secondary,
    Melee
}

public class Weapon : MonoBehaviour, IInteractable<Weapon>
{
    [SerializeField] float damage;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;

    [SerializeField] float maxDistance;

    [SerializeField] WeaponType type;
    [SerializeField] WeaponFireType fireType;
    [SerializeField] AmmoType ammoType;

    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;

    [SerializeField] int defaultBulletsCount;

    [SerializeField] GameObject bullet;
    [SerializeField] List<GameObject> bulletPool;

    [SerializeField] Transform spawnPoint;

    public WeaponType Type { get { return type; } }

    void Awake()
    {
        for (int i = 0; i < defaultBulletsCount; i++)
        {
            bulletPool.Add(Instantiate(bullet, transform));
        }
    }

    void ShootRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(spawnPoint.position, spawnPoint.forward, out hit, maxDistance))
        {
            HealthSystem objetive = hit.transform.GetComponent<HealthSystem>();

            if (hit.transform && objetive)
            {
                objetive.TakeDamage(damage);
            }
        }
    }

    void ShootBullet()
    {

    }

    public void Fire()
    {
        if (ammo > 0)
            switch (fireType)
            {
                case WeaponFireType.Ray:
                    ShootRay();
                    break;

                case WeaponFireType.Spawn:
                    ShootBullet();
                    break;

                default:
                    Debug.LogWarning($"{this.name} no tiene un WeaponType asignado.");
                    break;
            }
    }

    public void Reload(int ammo)
    {
        int aux = maxAmmo - ammo;

        if (ammo > aux)
        {
            ammo -= aux;
            this.ammo += aux;
        }
        else
        {
            this.ammo += ammo;
            ammo -= ammo;
        }

    }

    public Weapon OnInteract()
    {
        return this;
    }
}
