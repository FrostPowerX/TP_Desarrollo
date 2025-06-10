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
        animator.keepAnimatorStateOnDisable = true;
    }

    void PlayAnimFire()
    {
        animator.SetTrigger("Fire");

        if (particles && weapon.Type != WeaponType.Melee)
            particles.Play();
    }

    void PlayAnimReload()
    {
        animator.SetTrigger("Reload");
    }
}
