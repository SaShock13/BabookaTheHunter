using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneTrap : MonoBehaviour
{
    [SerializeField] Rigidbody stoneRigidbody;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stoneRigidbody.isKinematic = false;
        }
    }
}
