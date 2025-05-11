using System.Collections.Generic;
using UnityEngine;

enum WeaponType
{
    None,
    Ray,
    Spawn
}

enum AmmoType
{
    None,
    primary,
    secondary
}

public class Weapon : MonoBehaviour, Interacteable<Weapon>
{
    [SerializeField] float damage;
    [SerializeField] float fireRate;
    [SerializeField] float reloadTime;

    [SerializeField] float maxDistance;

    [SerializeField] WeaponType type;
    [SerializeField] AmmoType ammoType;

    [SerializeField] int ammo;
    [SerializeField] int maxAmmo;

    [SerializeField] int defaultBulletsCount;

    [SerializeField] GameObject bullet;
    [SerializeField] List<GameObject> bulletPool;

    [SerializeField] Transform spawnPoint;

    private void Awake()
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
            switch (type)
            {
                case WeaponType.Ray:
                    ShootRay();
                    break;

                case WeaponType.Spawn:
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
