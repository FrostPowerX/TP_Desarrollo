using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public delegate void Dead();
    public event Dead OnDeath;

    [SerializeField] float health;
    [SerializeField] float maxHealth;

    public float Health {  get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

    public void TakeDamage(float damage)
    {
        if (damage < 0) damage *= -1;

        health -= (health > 0) ? damage : health;

        if (health <= 0)
            OnDeath?.Invoke();
    }

    public void Heal(float healing)
    {
        if(healing < 0) healing *= -1;

        health += (health + healing <= maxHealth) ? healing : maxHealth - health;
    }
}
