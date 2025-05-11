using UnityEngine;

enum BulletType
{
    None,
    Explosive
}

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] BulletType type;

    [SerializeField] float damage;
    [SerializeField] float force;

    bool isEnable;

    public bool IsEnable { get { return isEnable; } }

    private void OnCollisionEnter(Collision collision)
    {
        HealthSystem hs;

        if (hs = collision.transform.GetComponent<HealthSystem>())
        {
            hs.TakeDamage(damage);
        }

        DisableBullet();
    }

    public void Fire()
    {
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public void SetForce(float force) => this.force = force;
    public void SetDamage( float damage) => this.damage = damage;

    public void EnableBullet()
    {
        isEnable = true;
        gameObject.SetActive(true);
    }

    public void DisableBullet()
    {
        isEnable = false;
        gameObject.SetActive(false);
    }
}
