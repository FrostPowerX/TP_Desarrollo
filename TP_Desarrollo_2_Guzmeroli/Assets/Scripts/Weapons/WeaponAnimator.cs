using UnityEngine;

[RequireComponent(typeof(Weapon))]
[RequireComponent(typeof(Animator))]
public class WeaponAnimator : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem particles;

    private void Awake()
    {
        weapon.Fired += PlayAnimFire;
        weapon.Reloaded += PlayAnimReload;
    }

    void PlayAnimFire()
    {
        animator.SetTrigger("Fire");
        if (particles)
            particles.Play();
    }

    void PlayAnimReload()
    {
        animator.SetTrigger("Reload");
    }
}
