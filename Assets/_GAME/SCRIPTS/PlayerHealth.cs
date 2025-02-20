using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    [SerializeField] private float maxHealth = 200;
    private Player player;
    private float health = 200;
    private float updateTime = 0.5f;

    public delegate void HealthUpdater (float maxHealth, float currentHealth);
    public event HealthUpdater OnHealthChanged;


    private void Start()
    {
        health = maxHealth;
        player = GetComponent<Player>();
    }

    public void TakeDamage(float damage)
    {

        if (damage > 0 )
        {
            Debug.Log($"Player Hearted with !!! {damage}");
            health -= damage; 
            OnHealthChanged?.Invoke(maxHealth, health);
            if(health<=0)
            {
                Death();
            }
        }
    }

    //private IEnumerator Start()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(updateTime);
            
    //    }
    //}

    private void Death()
    {
        player.Death();
        Debug.Log($"Player is Dead  !!! {this}");
    }
}
