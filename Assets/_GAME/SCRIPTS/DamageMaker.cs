using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DamageMaker : MonoBehaviour
{    
    private IHealth healthToDamage;
    private Rigidbody rigidbody;
    private DamageMaker damageMaker;
    private bool isDanger = true;

    [SerializeField] private float damage = 50;
    [SerializeField] private float minDamageMagnitude = 2f; // минимальна скорость камня для причинения дамага игроку

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        damageMaker = GetComponent<DamageMaker>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IHealth>(out healthToDamage) && isDanger)
        {
            if (rigidbody.velocity.magnitude > minDamageMagnitude)
            {
                healthToDamage.TakeDamage(damage);
            }
            isDanger = false;
        }
    }
}
