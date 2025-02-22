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
    public static event Action OnBearDeath;
    private AudioSource audioSource;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        bearBehaviour = GetComponentInChildren<BearBehaviour>();
        audioSource = GetComponent<AudioSource>();
    }
    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            health -= damage;
            Debug.Log($"Bear Hearted with !!! {damage}");
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
        audioSource.Play();
        StartCoroutine(WinCoroutine());
    }

    IEnumerator WinCoroutine()
    {
        yield return new WaitForSeconds(3);
        OnBearDeath?.Invoke();
    }

}
