using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string deadName;
    [SerializeField] string idleName;
    [SerializeField] string walkingName;

    [SerializeField] HealthSystem health;
    [SerializeField] Character character;

    private void Awake()
    {
        if (health)
            health.OnDeath += PlayDeath;
    }

    private void Update()
    {
        if (character)
            PlayWalking();
    }

    private void PlayDeath()
    {
        animator.SetTrigger(deadName);
    }

    void PlayWalking()
    {
        animator.SetBool(walkingName, character.IsWalking);
    }
}
