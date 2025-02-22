using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DamageMaker : MonoBehaviour
{    
    private IHealth healthToDamage;
    private Rigidbody rigidBody;
    private bool isDanger = true;

    [SerializeField] private float damage = 50;
    [SerializeField] private float minDamageMagnitude = 1f; // минимальна скорость камня для причинения дамага

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IHealth>(out healthToDamage) && isDanger)
        {

            Debug.Log($"IHealth collision with Y velocity {rigidBody.velocity.y}");


            if (Mathf.Abs( rigidBody.velocity.y) > minDamageMagnitude)
            {
                healthToDamage.TakeDamage(damage);
            }
            isDanger = false;
        }
    }
}
