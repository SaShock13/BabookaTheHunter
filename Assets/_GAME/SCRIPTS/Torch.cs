using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;    
    [SerializeField, Header("Скорость затухания огня,значение 10 это 10 секунд") ] 
    private float dyingSpeed = 10;
    [SerializeField] private GameObject lightedFXs;
    [SerializeField] private Light fireLight;

    private float health;
    private bool isLighted = false;
    private Vector3 initFxScale = Vector3.one;
    private BlinkingFire blinkingFire;
    private float fireLifeTime; // замеряет скорость затухания огня

    private void Start()
    {
        blinkingFire = GetComponent<BlinkingFire>();
        fireLight.enabled = false;
        health = maxHealth;
        SetUnLighted();
    }

    private void Update()
    {
        if (isLighted)
        {    
            FireDying(); 
        }
    }

    public void SetLighted()
    { 
        isLighted = true;
        fireLifeTime = 0;
        fireLight.enabled = true;
        lightedFXs.SetActive(true);
        health = maxHealth;
    }

    public void SetUnLighted()
    {
        fireLight.enabled = false;
        isLighted = false; 
        lightedFXs.SetActive(false);
        Debug.Log($"Life of fire was {fireLifeTime} seconds");
    }

    private void FireDying()
    {
        health -= Time.deltaTime * dyingSpeed; 
        fireLifeTime += Time.deltaTime ;
        var coef = health / maxHealth;
        blinkingFire.SetIntensityCoef(coef); // угасание интенсивности света
        lightedFXs.transform.localScale = initFxScale * coef; // уменьшает масштаб Particle , эффект угасания
        if (health < 0)
        {
            Death();
        }
    }

    private void Death()
    {        
        SetUnLighted();
    }
}
