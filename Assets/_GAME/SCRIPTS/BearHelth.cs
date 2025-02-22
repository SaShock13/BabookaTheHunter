using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BearHelth : MonoBehaviour,IHealth
{
    private Animator animator;
    private BearBehaviour bearBehaviour;
    public float health = 100;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bearBehaviour = GetComponentInChildren<BearBehaviour>();
    }
    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            health -= damage;
            Debug.Log($"Bear Hearted with !!! {damage}");
            //sounds.PlayHearted();
            //OnHealthChanged?.Invoke(maxHealth, health);
            if (health <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        animator.SetBool("Death", true);
        Debug.Log($"Bear is dead !!! {this}");
        bearBehaviour.enabled = false;
    }

}
