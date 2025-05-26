using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string deadName;
    [SerializeField] string idleName;
    [SerializeField] string walkingName;

    HealthSystem health;



    private void Awake()
    {
        health = GetComponent<HealthSystem>();

        if (health)
            health.OnDeath += PlayDeath;
    }

    private void PlayDeath()
    {
        animator.SetTrigger(deadName);
    }
}
