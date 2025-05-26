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
    [SerializeField] float lifeTime;

    float currentLifeTime;

    bool isEnable;

    public bool IsEnable { get { return isEnable; } }

    private void Awake()
    {
        currentLifeTime = lifeTime;
    }

    private void Update()
    {
        currentLifeTime -= (currentLifeTime > 0) ? Time.deltaTime: currentLifeTime;

        if (currentLifeTime <= 0)
            DisableBullet();
    }

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
        currentLifeTime = lifeTime;
    }

    public void DisableBullet()
    {
        isEnable = false;
        gameObject.SetActive(false);
    }
}
