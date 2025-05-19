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

public class Weapon : MonoBehaviour, IInteractable
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

    [SerializeField] Rigidbody rb;

    [SerializeField] GameObject bullet;
    [SerializeField] List<GameObject> bulletPool;

    [SerializeField] Transform spawnPoint;

    [SerializeField] bool interactActive;

    float lastFireTime = 0f;

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

        ammo--;
    }

    void ShootBullet()
    {
        ammo--;
    }

    public void Fire()
    {
        if (Time.time < lastFireTime + fireRate)
            return;

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

        lastFireTime = Time.time;
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

    public void OnDrop()
    {
        interactActive = true;
        rb.useGravity = true;
    }

    public void OnInteract(GameObject owner)
    {
        WeaponController wpc = owner.GetComponent<WeaponController>();

        if (wpc)
        {
            wpc.EquipWeapon(this);
            interactActive = false;
            rb.useGravity = false;
        }
    }

    public bool IsInteracteable()
    {
        return interactActive;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * maxDistance, Color.yellow);
    }
}
