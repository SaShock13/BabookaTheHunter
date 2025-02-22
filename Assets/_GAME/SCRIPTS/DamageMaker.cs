using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DamageMaker : MonoBehaviour
{    
    private IHealth healthToDamage;
    private Rigidbody rigidBody;

    /// <summary>
    /// Импульс солкновения по оси Y
    /// </summary>
    private float collisionYImpulse;

    [SerializeField] private bool isDanger = true;
    [SerializeField] private float damage = 50;
    [SerializeField] private float minDamageYImpulse = 1f; // минимальное сила столкновения камня по Y для причинения дамага

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<IHealth>(out healthToDamage) && isDanger)
        {
            collisionYImpulse = collision.impulse.y;
            Debug.Log($"IHealth collision with Y impulse {collisionYImpulse}");
            if (collisionYImpulse > minDamageYImpulse)
            {
                healthToDamage.TakeDamage(damage);
            }
        }
    }
}
