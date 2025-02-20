using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HUD : MonoBehaviour
{
    private PlayerHealth _playerHealth;
    [SerializeField] private Image healthImage;

    [Inject]
    public void Construct(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }


    private void OnEnable()
    {
        _playerHealth.OnHealthChanged += HealthChanged;
    }

    private void HealthChanged(float maxHealth, float currentHealth)
    {
        healthImage.fillAmount = currentHealth / maxHealth;
        Debug.Log($"Player health {currentHealth} of {maxHealth}");
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= HealthChanged;
    }
}
