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

[RequireComponent(typeof(Rigidbody))]
public class Weapon : MonoBehaviour, IInteractable
{
    public delegate void FireAction();
    public event FireAction Fired;

    public delegate void ReloadAction();
    public event ReloadAction Reloaded;

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

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] Transform defaultPoint;

    [SerializeField] Vector3 offSetPoint;

    [SerializeField] bool interactActive;


    float lastFireTime = 0f;
    public WeaponType Type { get { return type; } }
    public int Ammo { get { return ammo; } }
    public int MaxAmmo { get { return maxAmmo; } }

    void Awake()
    {
        for (int i = 0; i < defaultBulletsCount; i++)
        {
            CreateNewBullet();
        }
    }

    Bullet CreateNewBullet()
    {
        GameObject newBullet = Instantiate(bullet, transform);
        Bullet bulletScript = newBullet.GetComponent<Bullet>();

        bulletScript.SetDamage(damage);

        bulletPool.Add(newBullet);

        return bulletScript;
    }

    Bullet FindUnusedBullet()
    {
        Bullet fireBullet;

        for (int i = 0; i < bulletPool.Count; i++)
        {
            fireBullet = bulletPool[i].GetComponent<Bullet>();

            if (!fireBullet.IsEnable)
                return fireBullet;
        }

        fireBullet = CreateNewBullet().GetComponent<Bullet>();

        return fireBullet;
    }

    void ShootRay()
    {
        RaycastHit hit;

        if (Physics.Raycast(bulletSpawnPoint.position + offSetPoint, bulletSpawnPoint.forward, out hit, maxDistance))
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
        Bullet firingBullet = FindUnusedBullet();

        firingBullet.gameObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        firingBullet.EnableBullet();

        firingBullet.Fire();

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
                    Fired?.Invoke();
                    break;

                case WeaponFireType.Spawn:
                    ShootBullet();
                    Fired?.Invoke();
                    break;

                default:
                    Debug.LogWarning($"{this.name} no tiene un WeaponType asignado.");
                    break;
            }

        lastFireTime = Time.time;
    }

    public int Reload(int otherAmmo)
    {
        if (otherAmmo <= 0)
            return 0;

        Reloaded?.Invoke();

        int aux = maxAmmo - ammo;

        ammo += (aux <= otherAmmo) ? aux : otherAmmo;
        otherAmmo -= (aux <= otherAmmo) ? aux : otherAmmo;

        return otherAmmo;
    }

    public void OnDrop()
    {
        interactActive = true;
        rb.useGravity = true;
        rb.isKinematic = false;

        bulletSpawnPoint = defaultPoint;

        gameObject.GetComponent<Collider>().enabled = true;
    }

    public void OnInteract(GameObject owner)
    {
        WeaponController wpc = owner.GetComponent<WeaponController>();

        if (wpc)
        {
            wpc.EquipWeapon(this);
            interactActive = false;
            rb.useGravity = false;
            rb.isKinematic = true;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

    public bool IsInteracteable()
    {
        return interactActive;
    }

    public void SetSpawnPoint(Transform newPos) => bulletSpawnPoint = newPos;

#if DEBUG

    private void OnDrawGizmos()
    {
        Debug.DrawRay(bulletSpawnPoint.position + offSetPoint, bulletSpawnPoint.forward * maxDistance, Color.yellow);
    }

#endif
}
