using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStoneTrap : MonoBehaviour
{
    [SerializeField] Rigidbody stoneRigidbody;
    //todo согласовать с толканием или сделать падающий но не толкаемый
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stoneRigidbody.isKinematic = false;
        }
    }
}
